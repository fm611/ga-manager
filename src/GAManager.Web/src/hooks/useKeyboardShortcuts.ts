import { useEffect } from 'react'

interface UseKeyboardShortcutsArgs {
  undo: () => void
  redo: () => void
  onSave: () => void
  onSaveAs: () => void
  onOpen: () => void
  onDeleteCells: () => void
}

function isTextEntryTarget(el: EventTarget | null): boolean {
  if (!(el instanceof HTMLElement)) return false
  return el.tagName === 'INPUT' || el.tagName === 'TEXTAREA' || el.isContentEditable
}

export function useKeyboardShortcuts({ undo, redo, onSave, onSaveAs, onOpen, onDeleteCells }: UseKeyboardShortcutsArgs): void {
  useEffect(() => {
    function handleKeyDown(e: KeyboardEvent) {
      if (!(e.ctrlKey || e.metaKey) || isTextEntryTarget(document.activeElement)) return
      const key = e.key.toLowerCase()
      if (key === 'z' && !e.shiftKey) {
        e.preventDefault()
        undo()
      } else if (key === 'y' || (key === 'z' && e.shiftKey)) {
        e.preventDefault()
        redo()
      } else if (key === 's' && e.shiftKey) {
        e.preventDefault()
        onSaveAs()
      } else if (key === 's') {
        e.preventDefault()
        onSave()
      } else if (key === 'o') {
        e.preventDefault()
        onOpen()
      } else if (key === 'delete') {
        e.preventDefault()
        onDeleteCells()
      }
    }
    window.addEventListener('keydown', handleKeyDown)
    return () => window.removeEventListener('keydown', handleKeyDown)
  }, [undo, redo, onSave, onSaveAs, onOpen, onDeleteCells])
}
