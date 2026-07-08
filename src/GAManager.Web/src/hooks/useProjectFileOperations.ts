import { useCallback, useEffect, useRef, useState } from 'react'
import type { Project } from '../domain/schema'
import { ProjectSchema } from '../domain/schema'
import { buildGaExportCsv } from '../domain/csvExport'
import { parseGaImportCsv } from '../domain/csvImport'
import {
  initHostBridge,
  notifyNewProject,
  reportDirty,
  requestOpenFile,
  requestOpenRecentFile,
  requestSaveFile,
  requestExportFile,
  requestImportFile,
  type RecentFile,
  type OpenFileResult,
} from '../host/wpfBridge'

function parseProjectJson(json: string): Project | null {
  try {
    const parsed = ProjectSchema.safeParse(JSON.parse(json))
    return parsed.success ? parsed.data : null
  } catch {
    return null
  }
}

interface UseProjectFileOperationsArgs {
  project: Project
  dirty: boolean
  markClean: () => void
  applyReset: (project: Project) => void
}

export interface PendingReset {
  project: Project
  clearHostFile: boolean
}

export function useProjectFileOperations({ project, dirty, markClean, applyReset }: UseProjectFileOperationsArgs) {
  const [recentFiles, setRecentFiles] = useState<RecentFile[]>([])
  const [pendingReset, setPendingReset] = useState<PendingReset | null>(null)

  const projectRef = useRef(project)
  useEffect(() => {
    projectRef.current = project
  }, [project])

  const handleResetRequest = useCallback(
    (newProject: Project, options?: { clearHostFile?: boolean }) => {
      const clearHostFile = options?.clearHostFile ?? false
      if (dirty) {
        setPendingReset({ project: newProject, clearHostFile })
      } else {
        if (clearHostFile) notifyNewProject()
        applyReset(newProject)
      }
    },
    [dirty, applyReset],
  )

  const handleLoadedProjectFile = useCallback(
    (result: OpenFileResult | null) => {
      if (!result) return
      const parsed = parseProjectJson(result.content)
      if (!parsed) {
        alert(`Datei ist kein gültiges Projekt:\n${result.path}`)
        return
      }
      handleResetRequest(parsed)
    },
    [handleResetRequest],
  )

  useEffect(() => {
    initHostBridge({
      getProjectJson: () => JSON.stringify(projectRef.current),
      onFileOpened: handleLoadedProjectFile,
      onRecentFilesChanged: setRecentFiles,
    })
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])

  useEffect(() => {
    reportDirty(dirty)
  }, [dirty])

  const handleOpenFile = useCallback(async () => {
    handleLoadedProjectFile(await requestOpenFile())
  }, [handleLoadedProjectFile])

  const handleOpenRecent = useCallback(
    async (path: string) => {
      handleLoadedProjectFile(await requestOpenRecentFile(path))
    },
    [handleLoadedProjectFile],
  )

  const handleSave = useCallback(async () => {
    const result = await requestSaveFile(JSON.stringify(project, null, 2))
    if (result) markClean()
  }, [project, markClean])

  const handleExport = useCallback(async () => {
    await requestExportFile(buildGaExportCsv(project), 'Gruppenadressen.csv')
  }, [project])

  const handleImport = useCallback(async () => {
    const result = await requestImportFile()
    if (!result) return
    const parsed = parseGaImportCsv(result.content)
    if (!parsed) {
      alert(`Datei ist keine gültige Gruppenadressen-CSV:\n${result.path}`)
      return
    }
    handleResetRequest(parsed, { clearHostFile: true })
  }, [handleResetRequest])

  const confirmPendingReset = useCallback(() => {
    if (pendingReset) {
      if (pendingReset.clearHostFile) notifyNewProject()
      applyReset(pendingReset.project)
    }
    setPendingReset(null)
  }, [pendingReset, applyReset])

  const cancelPendingReset = useCallback(() => setPendingReset(null), [])

  return {
    recentFiles,
    handleResetRequest,
    handleOpenFile,
    handleOpenRecent,
    handleSave,
    handleExport,
    handleImport,
    pendingReset,
    confirmPendingReset,
    cancelPendingReset,
  }
}
