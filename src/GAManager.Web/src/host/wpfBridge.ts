// Bridge between the React app and the WPF host that embeds it via WebView2.
//
// Protocol (all messages are plain JSON objects):
//
// React -> Host (window.chrome.webview.postMessage):
//   { type: 'ready' }
//   { type: 'openFile', id }
//   { type: 'openRecent', id, path }
//   { type: 'saveFile', id, content }
//   { type: 'saveFileAs', id, content }
//   { type: 'exportFile', id, content, fileName }    // save-as for a one-off export (e.g. CSV); doesn't touch the
//                                                    // host's notion of the currently open/saved project file
//   { type: 'importFile', id }                       // open-file-picker for a one-off import (e.g. CSV); doesn't touch the
//                                                    // host's notion of the currently open/saved project file
//   { type: 'setDirty', dirty }
//   { type: 'contentResponse', id, content }        // reply to host's requestContent
//   { type: 'newProject' }                          // React replaced the project with one that has no file (Neu / Beispiel) -
//                                                    // tells the host to forget the previously open/saved file
//
// Host -> React (CoreWebView2.PostWebMessageAsJson):
//   { type: 'openResult', id, ok, path?, content?, error? }
//   { type: 'saveResult', id, ok, path? }
//   { type: 'exportResult', id, ok, path? }
//   { type: 'importResult', id, ok, path?, content?, error? }
//   { type: 'fileOpened', path, content }            // pushed e.g. when launched with a .gaproj file
//   { type: 'requestContent', id }                   // host asks for the current project JSON (e.g. before closing)
//   { type: 'recentFiles', files }                   // pushed whenever the host's recent-files list (recent.json) changes
//
// When not running inside the WPF WebView2 host (e.g. `npm run dev` in a normal browser),
// Open/Save fall back to a native file picker / download so the app stays usable standalone.

declare global {
  interface Window {
    chrome?: {
      webview?: {
        postMessage: (message: unknown) => void
        addEventListener: (type: 'message', listener: (event: MessageEvent) => void) => void
      }
    }
  }
}

export interface OpenFileResult {
  path: string
  content: string
}

export interface SaveFileResult {
  path: string
}

export interface RecentFile {
  path: string
  name: string
}

interface OpenResultMessage {
  type: 'openResult'
  id: string
  ok: boolean
  path?: string
  content?: string
  error?: string
}

interface SaveResultMessage {
  type: 'saveResult'
  id: string
  ok: boolean
  path?: string
}

interface ExportResultMessage {
  type: 'exportResult'
  id: string
  ok: boolean
  path?: string
}

interface ImportResultMessage {
  type: 'importResult'
  id: string
  ok: boolean
  path?: string
  content?: string
  error?: string
}

interface FileOpenedMessage {
  type: 'fileOpened'
  path: string
  content: string
}

interface RequestContentMessage {
  type: 'requestContent'
  id: string
}

interface RecentFilesMessage {
  type: 'recentFiles'
  files: RecentFile[]
}

type HostMessage =
  | OpenResultMessage
  | SaveResultMessage
  | ExportResultMessage
  | ImportResultMessage
  | FileOpenedMessage
  | RequestContentMessage
  | RecentFilesMessage

type PendingResolver = (message: OpenResultMessage | SaveResultMessage | ExportResultMessage | ImportResultMessage) => void

const pendingRequests = new Map<string, PendingResolver>()

let getContent: (() => string) | null = null
let fileOpenedListener: ((result: OpenFileResult) => void) | null = null
let recentFilesListener: ((files: RecentFile[]) => void) | null = null

function isHosted(): boolean {
  return typeof window !== 'undefined' && !!window.chrome?.webview
}

function createId(): string {
  return crypto.randomUUID()
}

function postToHost(message: unknown): void {
  window.chrome!.webview!.postMessage(message)
}

function handleHostMessage(message: HostMessage): void {
  switch (message.type) {
    case 'openResult':
    case 'saveResult':
    case 'exportResult':
    case 'importResult': {
      const resolve = pendingRequests.get(message.id)
      if (resolve) {
        pendingRequests.delete(message.id)
        resolve(message)
      }
      break
    }
    case 'fileOpened':
      fileOpenedListener?.({ path: message.path, content: message.content })
      break
    case 'requestContent':
      postToHost({ type: 'contentResponse', id: message.id, content: getContent ? getContent() : '' })
      break
    case 'recentFiles':
      recentFilesListener?.(message.files)
      break
  }
}

if (isHosted()) {
  window.chrome!.webview!.addEventListener('message', (event) => handleHostMessage(event.data as HostMessage))
}

/**
 * Wires the bridge up to the app: `getProjectJson` is called whenever the host needs the
 * current project content (e.g. before closing the window), `onFileOpened` is invoked
 * when the host pushes a project it opened on its own (e.g. a .gaproj file passed on the
 * command line at startup), and `onRecentFilesChanged` is invoked whenever the host's
 * recent-files list (recent.json) changes.
 */
export function initHostBridge(handlers: {
  getProjectJson: () => string
  onFileOpened: (result: OpenFileResult) => void
  onRecentFilesChanged: (files: RecentFile[]) => void
}): void {
  getContent = handlers.getProjectJson
  fileOpenedListener = handlers.onFileOpened
  recentFilesListener = handlers.onRecentFilesChanged
  if (isHosted()) postToHost({ type: 'ready' })
}

export function reportDirty(dirty: boolean): void {
  if (isHosted()) postToHost({ type: 'setDirty', dirty })
}

/** Tells the host that the project was replaced by one with no associated file (Neu / Beispiel),
 *  so it stops treating the previously open/saved file as the current one. */
export function notifyNewProject(): void {
  if (isHosted()) postToHost({ type: 'newProject' })
}

export function requestOpenFile(): Promise<OpenFileResult | null> {
  if (!isHosted()) return pickFileFallback('.gaproj')
  return sendOpenRequest('openFile')
}

export function requestOpenRecentFile(path: string): Promise<OpenFileResult | null> {
  if (!isHosted()) return Promise.resolve(null)
  return sendOpenRequest('openRecent', { path })
}

export function requestSaveFile(content: string): Promise<SaveFileResult | null> {
  return sendSaveRequest('saveFile', content)
}

export function requestSaveFileAs(content: string): Promise<SaveFileResult | null> {
  return sendSaveRequest('saveFileAs', content)
}

/** Exports arbitrary content (e.g. a CSV) to a file the user picks, without affecting the
 *  host's notion of the currently open/saved project file. */
export function requestExportFile(content: string, suggestedFileName: string): Promise<SaveFileResult | null> {
  if (!isHosted()) return exportFallback(content, suggestedFileName)

  return new Promise((resolve) => {
    const id = createId()
    pendingRequests.set(id, (message) => {
      const result = message as ExportResultMessage
      resolve(result.ok && result.path ? { path: result.path } : null)
    })
    postToHost({ type: 'exportFile', id, content, fileName: suggestedFileName })
  })
}

/** Imports arbitrary content (e.g. a CSV) from a file the user picks, without affecting the
 *  host's notion of the currently open/saved project file. */
export function requestImportFile(): Promise<OpenFileResult | null> {
  if (!isHosted()) return pickFileFallback('.csv')
  return sendOpenRequest('importFile')
}

function sendOpenRequest(type: 'openFile' | 'openRecent' | 'importFile', extra?: Record<string, unknown>): Promise<OpenFileResult | null> {
  return new Promise((resolve) => {
    const id = createId()
    pendingRequests.set(id, (message) => {
      const result = message as OpenResultMessage | ImportResultMessage
      resolve(result.ok && result.path && result.content !== undefined ? { path: result.path, content: result.content } : null)
    })
    postToHost({ type, id, ...extra })
  })
}

function sendSaveRequest(type: 'saveFile' | 'saveFileAs', content: string): Promise<SaveFileResult | null> {
  if (!isHosted()) return saveFallback(content)

  return new Promise((resolve) => {
    const id = createId()
    pendingRequests.set(id, (message) => {
      const result = message as SaveResultMessage
      resolve(result.ok && result.path ? { path: result.path } : null)
    })
    postToHost({ type, id, content })
  })
}

// ---- Fallbacks for running the app outside of the WPF host (`npm run dev`) ----------------

function pickFileFallback(accept: string): Promise<OpenFileResult | null> {
  return new Promise((resolve) => {
    const input = document.createElement('input')
    input.type = 'file'
    input.accept = accept
    input.onchange = () => {
      const file = input.files?.[0]
      if (!file) {
        resolve(null)
        return
      }
      const reader = new FileReader()
      reader.onload = () => resolve({ path: file.name, content: String(reader.result ?? '') })
      reader.onerror = () => resolve(null)
      reader.readAsText(file)
    }
    input.click()
  })
}

function saveFallback(content: string): Promise<SaveFileResult | null> {
  const fileName = 'Projekt.gaproj'
  const blob = new Blob([content], { type: 'application/json' })
  const url = URL.createObjectURL(blob)
  const anchor = document.createElement('a')
  anchor.href = url
  anchor.download = fileName
  anchor.click()
  URL.revokeObjectURL(url)
  return Promise.resolve({ path: fileName })
}

function exportFallback(content: string, fileName: string): Promise<SaveFileResult | null> {
  const blob = new Blob([content], { type: 'text/csv;charset=utf-8' })
  const url = URL.createObjectURL(blob)
  const anchor = document.createElement('a')
  anchor.href = url
  anchor.download = fileName
  anchor.click()
  URL.revokeObjectURL(url)
  return Promise.resolve({ path: fileName })
}
