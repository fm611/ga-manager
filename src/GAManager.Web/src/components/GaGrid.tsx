import { forwardRef, memo, useEffect, useImperativeHandle, useMemo, useRef, useState } from 'react'
import { mergeClasses, MenuList, MenuItem, MenuDivider } from '@fluentui/react-components'
import type { Address, GA, Group } from '../domain/schema'
import { addressToString, gaAddressName } from '../domain/operations'
import { TextPromptDialog } from './dialogs/TextPromptDialog'
import { ConfirmDialog } from './dialogs/ConfirmDialog'
import { useStyles } from './GaGrid.styles'
import { useStableCallback } from '../hooks/useStableCallback'

const TOTAL_ROWS = 256
const COLUMNS = [0, 1, 2, 3, 4, 5, 6, 7]
const MIN_DRAG_WIDTH = 4

function gaMatchesSearch(g: GA, searchLower: string): boolean {
  return g.name.toLowerCase().includes(searchLower) || addressToString(g.address).toLowerCase().includes(searchLower)
}

function cellKey(r: number, c: number): string {
  return `${r}:${c}`
}

function parseCellKey(key: string): { r: number; c: number } {
  const [rStr, cStr] = key.split(':')
  return { r: Number(rStr), c: Number(cStr) }
}

interface GaGridRowHeaderProps {
  row: number
  onMouseDown: (row: number, e: React.MouseEvent) => void
  onMouseEnter: (row: number) => void
}

const GaGridRowHeader = memo(function GaGridRowHeader({ row, onMouseDown, onMouseEnter }: GaGridRowHeaderProps) {
  const styles = useStyles()
  return (
    <th className={styles.rowHeader} onMouseDown={(e) => onMouseDown(row, e)} onMouseEnter={() => onMouseEnter(row)}>
      {row}
    </th>
  )
})

interface GaGridCellProps {
  row: number
  col: number
  ga: GA | undefined
  isSelected: boolean
  isEditing: boolean
  isHighlighted: boolean
  isSearchMatch: boolean
  editValue: string
  hasCustomWidth: boolean
  onMouseDown: (row: number, col: number, e: React.MouseEvent) => void
  onMouseEnter: (row: number, col: number) => void
  onDoubleClick: (row: number, col: number) => void
  onContextMenu: (e: React.MouseEvent, row: number, col: number) => void
  onEditChange: (value: string) => void
  onEditCommit: () => void
  onEditCancel: () => void
}

const GaGridCell = memo(function GaGridCell({
  row,
  col,
  ga,
  isSelected,
  isEditing,
  isHighlighted,
  isSearchMatch,
  editValue,
  hasCustomWidth,
  onMouseDown,
  onMouseEnter,
  onDoubleClick,
  onContextMenu,
  onEditChange,
  onEditCommit,
  onEditCancel,
}: GaGridCellProps) {
  const styles = useStyles()
  return (
    <td
      className={mergeClasses(
        styles.cell,
        isHighlighted && styles.cellHighlighted,
        isSearchMatch && styles.cellSearchMatch,
        isSelected && styles.cellSelected,
      )}
      style={hasCustomWidth ? { minWidth: 0 } : undefined}
      onMouseDown={(e) => onMouseDown(row, col, e)}
      onMouseEnter={() => onMouseEnter(row, col)}
      onDoubleClick={() => onDoubleClick(row, col)}
      onContextMenu={(e) => onContextMenu(e, row, col)}
    >
      {isEditing ? (
        <input
          autoFocus
          className={styles.cellInput}
          value={editValue}
          onChange={(e) => onEditChange(e.target.value)}
          onBlur={onEditCommit}
          onKeyDown={(e) => {
            if (e.key === 'Enter') {
              e.preventDefault()
              onEditCommit()
            } else if (e.key === 'Escape') {
              e.preventDefault()
              onEditCancel()
            }
            e.stopPropagation()
          }}
        />
      ) : ga ? (
        gaAddressName(ga)
      ) : (
        ''
      )}
    </td>
  )
})

export interface GaGridHandle {
  scrollToRow: (row: number) => void
}

interface GaGridProps {
  gas: GA[]
  subGroupNames: string[]
  mainGroupSubAddress: number
  readOnly: boolean
  filterGroupIds: (string | null)[] | null
  searchQuery?: string
  enableGroupContextMenu?: boolean
  groups?: Group[]
  onRenameColumn: (columnIndex: number, name: string) => void
  onCommitCell: (middleGroup: number, ga: number, name: string) => void
  onDeleteGAs: (gaIds: string[]) => void
  onAssignToGroup?: (gaIds: string[], groupId: string) => void
  onSelectionChange?: (gas: GA[], addresses: Address[]) => void
}

export const GaGrid = forwardRef<GaGridHandle, GaGridProps>(function GaGrid(
  {
    gas,
    subGroupNames,
    mainGroupSubAddress,
    readOnly,
    filterGroupIds,
    searchQuery,
    enableGroupContextMenu,
    groups,
    onRenameColumn,
    onCommitCell,
    onDeleteGAs,
    onAssignToGroup,
    onSelectionChange,
  },
  ref,
) {
  const styles = useStyles()
  const rowRefs = useRef<Map<number, HTMLTableRowElement>>(new Map())
  const containerRef = useRef<HTMLDivElement>(null)

  const thRefs = useRef<Map<number, HTMLTableCellElement>>(new Map())

  const [selected, setSelected] = useState<Set<string>>(new Set())
  const [anchor, setAnchor] = useState<{ r: number; c: number } | null>(null)
  const [isDragging, setIsDragging] = useState(false)
  const [columnWidths, setColumnWidths] = useState<Record<number, number>>({})
  const [resizingCol, setResizingCol] = useState<{ col: number; startX: number; startWidth: number } | null>(null)
  const [editingCell, setEditingCell] = useState<{ r: number; c: number } | null>(null)
  const [editValue, setEditValue] = useState('')
  const [renameColumn, setRenameColumn] = useState<number | null>(null)
  const [confirmDelete, setConfirmDelete] = useState(false)
  const [contextMenu, setContextMenu] = useState<{ x: number; y: number; gaIds: string[] } | null>(null)

  useImperativeHandle(ref, () => ({
    scrollToRow: (row: number) => {
      rowRefs.current.get(row)?.scrollIntoView({ block: 'center' })
    },
  }))

  // The confirm dialog isn't opened via a Fluent DialogTrigger (it's driven by state), so
  // Fluent has no trigger element to return focus to when it closes and focus falls back to
  // the document body — reclaim it once the dialog has finished unmounting/animating out.
  function focusContainerAfterDialogClose() {
    requestAnimationFrame(() => containerRef.current?.focus())
  }

  function handleResizeMouseDown(c: number, e: React.MouseEvent) {
    e.preventDefault()
    e.stopPropagation()
    const startWidth = thRefs.current.get(c)?.getBoundingClientRect().width ?? MIN_DRAG_WIDTH
    setResizingCol({ col: c, startX: e.clientX, startWidth })
  }

  useEffect(() => {
    if (!resizingCol) return
    function onMove(e: MouseEvent) {
      const delta = e.clientX - resizingCol!.startX
      const newWidth = Math.max(MIN_DRAG_WIDTH, resizingCol!.startWidth + delta)
      setColumnWidths((prev) => ({ ...prev, [resizingCol!.col]: newWidth }))
    }
    function onUp() {
      setResizingCol(null)
    }
    window.addEventListener('mousemove', onMove)
    window.addEventListener('mouseup', onUp)
    return () => {
      window.removeEventListener('mousemove', onMove)
      window.removeEventListener('mouseup', onUp)
    }
  }, [resizingCol])

  const gaByCell = useMemo(() => {
    const map = new Map<string, GA>()
    for (const g of gas) map.set(`${g.address.middleGroup}:${g.address.ga}`, g)
    return map
  }, [gas])

  const filterActive = filterGroupIds !== null && filterGroupIds.length > 0

  const searchLower = searchQuery?.trim().toLowerCase() ?? ''
  const searchActive = searchLower.length > 0

  const rows = useMemo(() => {
    const result: { row: number; gas: (GA | undefined)[] }[] = []
    for (let r = 0; r < TOTAL_ROWS; r++) {
      const rowGAs = COLUMNS.map((c) => gaByCell.get(`${c}:${r}`))
      if (filterActive) {
        const matches = rowGAs.filter((g): g is GA => !!g && filterGroupIds!.includes(g.groupId))
        if (matches.length === 0) continue
      }
      if (searchActive) {
        const matches = rowGAs.some((g): g is GA => !!g && gaMatchesSearch(g, searchLower))
        if (!matches) continue
      }
      result.push({ row: r, gas: rowGAs })
    }
    return result
  }, [gaByCell, filterActive, filterGroupIds, searchActive, searchLower])

  function rangeSelection(from: { r: number; c: number }, to: { r: number; c: number }) {
    const rMin = Math.min(from.r, to.r)
    const rMax = Math.max(from.r, to.r)
    const cMin = Math.min(from.c, to.c)
    const cMax = Math.max(from.c, to.c)
    const next = new Set<string>()
    for (let rr = rMin; rr <= rMax; rr++) for (let cc = cMin; cc <= cMax; cc++) next.add(cellKey(rr, cc))
    return next
  }

  const selectedRef = useRef(selected)
  useEffect(() => {
    selectedRef.current = selected
  }, [selected])

  useEffect(() => {
    if (!isDragging) return
    const stopDragging = () => {
      setIsDragging(false)
      computeSelectionResult(selectedRef.current)
    }
    window.addEventListener('mouseup', stopDragging)
    return () => window.removeEventListener('mouseup', stopDragging)
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isDragging])

  function computeSelectionResult(sel: Set<string>) {
    const selectedGAs: GA[] = []
    const selectedAddresses: Address[] = []
    for (const key of sel) {
      const { r, c } = parseCellKey(key)
      selectedAddresses.push({ mainGroup: mainGroupSubAddress, middleGroup: c, ga: r })
      const g = gaByCell.get(`${c}:${r}`)
      if (g) selectedGAs.push(g)
    }
    onSelectionChange?.(selectedGAs, selectedAddresses)
  }

  function setSelection(next: Set<string>) {
    setSelected(next)
    computeSelectionResult(next)
  }

  function handleCellMouseDown(r: number, c: number, e: React.MouseEvent) {
    if (editingCell) commitEdit()
    if (e.shiftKey && anchor) {
      setSelection(rangeSelection(anchor, { r, c }))
    } else if (e.ctrlKey || e.metaKey) {
      const next = new Set(selected)
      const k = cellKey(r, c)
      if (next.has(k)) next.delete(k)
      else next.add(k)
      setSelection(next)
      setAnchor({ r, c })
    } else {
      setSelection(new Set([cellKey(r, c)]))
      setAnchor({ r, c })
      setIsDragging(true)
    }
  }

  function handleCellMouseEnter(r: number, c: number) {
    if (!isDragging || !anchor) return
    setSelected(rangeSelection(anchor, { r, c }))
  }

  function rowCells(row: number): Set<string> {
    return new Set(COLUMNS.map((c) => cellKey(row, c)))
  }

  function handleRowHeaderMouseDown(row: number, e: React.MouseEvent) {
    if (editingCell) commitEdit()
    if (e.shiftKey && anchor) {
      setSelection(rangeSelection({ r: anchor.r, c: 0 }, { r: row, c: COLUMNS.length - 1 }))
    } else if (e.ctrlKey || e.metaKey) {
      const cells = rowCells(row)
      const allSelected = [...cells].every((k) => selected.has(k))
      const next = new Set(selected)
      for (const k of cells) {
        if (allSelected) next.delete(k)
        else next.add(k)
      }
      setSelection(next)
      setAnchor({ r: row, c: 0 })
    } else {
      setSelection(rowCells(row))
      setAnchor({ r: row, c: 0 })
      setIsDragging(true)
    }
  }

  function handleRowHeaderMouseEnter(row: number) {
    if (!isDragging || !anchor) return
    setSelected(rangeSelection({ r: anchor.r, c: 0 }, { r: row, c: COLUMNS.length - 1 }))
  }

  function beginEdit(r: number, c: number, initialValue?: string) {
    if (readOnly) return
    setSelection(new Set([cellKey(r, c)]))
    setAnchor({ r, c })
    const ga = gaByCell.get(`${c}:${r}`)
    setEditValue(initialValue ?? ga?.name ?? '')
    setEditingCell({ r, c })
  }

  function commitEdit() {
    if (!editingCell) return
    const trimmed = editValue.trim()
    if (trimmed.length > 0) {
      onCommitCell(editingCell.c, editingCell.r, trimmed)
    }
    setEditingCell(null)
  }

  function cancelEdit() {
    setEditingCell(null)
  }

  function selectedGAIds(): string[] {
    const ids: string[] = []
    for (const key of selected) {
      const { r, c } = parseCellKey(key)
      const g = gaByCell.get(`${c}:${r}`)
      if (g) ids.push(g.id)
    }
    return ids
  }

  function selectionBounds(): { rMin: number; rMax: number; cMin: number; cMax: number } | null {
    if (selected.size === 0) return null
    let rMin = Infinity
    let rMax = -Infinity
    let cMin = Infinity
    let cMax = -Infinity
    for (const key of selected) {
      const { r, c } = parseCellKey(key)
      if (r < rMin) rMin = r
      if (r > rMax) rMax = r
      if (c < cMin) cMin = c
      if (c > cMax) cMax = c
    }
    return { rMin, rMax, cMin, cMax }
  }

  function buildClipboardText(): string | null {
    const bounds = selectionBounds()
    if (!bounds) return null
    const lines: string[] = []
    for (let r = bounds.rMin; r <= bounds.rMax; r++) {
      const cells: string[] = []
      for (let c = bounds.cMin; c <= bounds.cMax; c++) {
        cells.push(gaByCell.get(`${c}:${r}`)?.name ?? '')
      }
      lines.push(cells.join('\t'))
    }
    return lines.join('\n')
  }

  function pasteClipboardText(text: string) {
    const bounds = selectionBounds()
    if (!bounds) return

    const rows = text.replace(/\r/g, '').split('\n')
    if (rows.length > 1 && rows[rows.length - 1] === '') rows.pop()
    const grid = rows.map((line) => line.split('\t'))

    // A single copied value fills every selected cell (like a spreadsheet fill-paste);
    // a multi-cell block pastes starting at the selection's top-left corner.
    if (grid.length === 1 && grid[0].length === 1) {
      const value = grid[0][0].trim()
      if (value.length === 0) return
      for (const key of selected) {
        const { r, c } = parseCellKey(key)
        onCommitCell(c, r, value)
      }
      return
    }

    for (let r = 0; r < grid.length; r++) {
      const targetRow = bounds.rMin + r
      if (targetRow < 0 || targetRow >= TOTAL_ROWS) continue
      for (let c = 0; c < grid[r].length; c++) {
        const targetCol = bounds.cMin + c
        if (targetCol < 0 || targetCol >= COLUMNS.length) continue
        const value = grid[r][c].trim()
        if (value.length === 0) continue
        onCommitCell(targetCol, targetRow, value)
      }
    }
  }

  async function handleKeyDown(e: React.KeyboardEvent) {
    if (editingCell) return

    if (e.key === 'Delete' && !readOnly) {
      if (selectedGAIds().length > 0) setConfirmDelete(true)
      return
    }

    if ((e.ctrlKey || e.metaKey) && e.key.toLowerCase() === 'c') {
      const text = buildClipboardText()
      if (text !== null) {
        try {
          await navigator.clipboard.writeText(text)
        } catch {
          /* clipboard unavailable — ignore */
        }
      }
      return
    }

    if ((e.ctrlKey || e.metaKey) && e.key.toLowerCase() === 'v' && selected.size > 0 && !readOnly) {
      try {
        const text = await navigator.clipboard.readText()
        if (text.length === 0) return
        pasteClipboardText(text)
      } catch {
        /* clipboard unavailable — ignore */
      }
      return
    }

    // Typing over a single selected cell starts editing it with the typed character,
    // replacing its content — same as double-clicking, but without the extra click.
    if (!readOnly && selected.size === 1 && e.key.length === 1 && !e.ctrlKey && !e.metaKey && !e.altKey) {
      const [key] = selected
      const { r, c } = parseCellKey(key)
      e.preventDefault()
      beginEdit(r, c, e.key)
    }
  }

  function handleContextMenu(e: React.MouseEvent, r: number, c: number) {
    if (!enableGroupContextMenu) return
    e.preventDefault()
    let ids = selectedGAIds()
    if (ids.length === 0) {
      const ga = gaByCell.get(`${c}:${r}`)
      if (ga) {
        setSelection(new Set([cellKey(r, c)]))
        ids = [ga.id]
      }
    }
    if (ids.length === 0) return
    setContextMenu({ x: e.clientX, y: e.clientY, gaIds: ids })
  }

  // Stable handler references so memoized GaGridCell/GaGridRowHeader instances only
  // re-render when their own visual props change, not on every selection tick.
  const stableCellMouseDown = useStableCallback(handleCellMouseDown)
  const stableCellMouseEnter = useStableCallback(handleCellMouseEnter)
  const stableCellDoubleClick = useStableCallback(beginEdit)
  const stableCellContextMenu = useStableCallback(handleContextMenu)
  const stableEditCommit = useStableCallback(commitEdit)
  const stableEditCancel = useStableCallback(cancelEdit)

  const stableRowHeaderMouseDown = useStableCallback(handleRowHeaderMouseDown)
  const stableRowHeaderMouseEnter = useStableCallback(handleRowHeaderMouseEnter)

  return (
    <div
      ref={containerRef}
      className={styles.container}
      tabIndex={0}
      onKeyDown={(e) => void handleKeyDown(e)}
    >
      <table className={styles.table}>
        <colgroup>
          <col />
          {COLUMNS.map((c) => (
            <col key={c} style={columnWidths[c] !== undefined ? { width: columnWidths[c] } : undefined} />
          ))}
        </colgroup>
        <thead>
          <tr>
            <th className={styles.th} style={{ left: 0, zIndex: 2 }} />
            {COLUMNS.map((c) => (
              <th
                key={c}
                ref={(el) => {
                  if (el) thRefs.current.set(c, el)
                  else thRefs.current.delete(c)
                }}
                className={styles.th}
                style={columnWidths[c] !== undefined ? { minWidth: 0 } : undefined}
                onDoubleClick={() => !readOnly && setRenameColumn(c)}
                title={readOnly ? undefined : 'Doppelklick zum Umbenennen'}
              >
                {c} - {subGroupNames[c] || ''}
                <div className={styles.colResizer} onMouseDown={(e) => handleResizeMouseDown(c, e)} onDoubleClick={(e) => e.stopPropagation()} />
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {rows.map(({ row, gas: rowGAs }) => (
            <tr
              key={row}
              ref={(el) => {
                if (el) rowRefs.current.set(row, el)
                else rowRefs.current.delete(row)
              }}
            >
              <GaGridRowHeader row={row} onMouseDown={stableRowHeaderMouseDown} onMouseEnter={stableRowHeaderMouseEnter} />
              {COLUMNS.map((c) => {
                const ga = rowGAs[c]
                const key = cellKey(row, c)
                const isSelected = selected.has(key)
                const isEditing = editingCell?.r === row && editingCell?.c === c
                const isHighlighted = !!ga && filterActive && filterGroupIds!.includes(ga.groupId)
                const isSearchMatch = !!ga && searchActive && gaMatchesSearch(ga, searchLower)
                return (
                  <GaGridCell
                    key={c}
                    row={row}
                    col={c}
                    ga={ga}
                    isSelected={isSelected}
                    isEditing={isEditing}
                    isHighlighted={isHighlighted}
                    isSearchMatch={isSearchMatch}
                    editValue={isEditing ? editValue : ''}
                    hasCustomWidth={columnWidths[c] !== undefined}
                    onMouseDown={stableCellMouseDown}
                    onMouseEnter={stableCellMouseEnter}
                    onDoubleClick={stableCellDoubleClick}
                    onContextMenu={stableCellContextMenu}
                    onEditChange={setEditValue}
                    onEditCommit={stableEditCommit}
                    onEditCancel={stableEditCancel}
                  />
                )
              })}
            </tr>
          ))}
        </tbody>
      </table>

      {renameColumn !== null && (
        <TextPromptDialog
          open
          title="Mittelgruppe"
          initialValue={subGroupNames[renameColumn] || 'Neue Mittelgruppe'}
          onSubmit={(value) => {
            onRenameColumn(renameColumn, value)
            setRenameColumn(null)
          }}
          onCancel={() => setRenameColumn(null)}
        />
      )}

      <ConfirmDialog
        open={confirmDelete}
        title="Gruppenadressen löschen"
        message="Gruppenadressen löschen?"
        onConfirm={() => {
          onDeleteGAs(selectedGAIds())
          setConfirmDelete(false)
          focusContainerAfterDialogClose()
        }}
        onCancel={() => {
          setConfirmDelete(false)
          focusContainerAfterDialogClose()
        }}
      />

      {contextMenu && groups && onAssignToGroup && (
        <>
          <div className={styles.contextOverlay} onClick={() => setContextMenu(null)} onContextMenu={(e) => e.preventDefault()} />
          <div className={styles.contextMenu} style={{ left: contextMenu.x, top: contextMenu.y }}>
            <MenuList>
              <MenuItem disabled>Zu Gruppe hinzufügen:</MenuItem>
              <MenuDivider />
              {groups.length === 0 && <MenuItem disabled>(keine Gruppen vorhanden)</MenuItem>}
              {groups.map((g) => (
                <MenuItem
                  key={g.id}
                  onClick={() => {
                    onAssignToGroup(contextMenu.gaIds, g.id)
                    setContextMenu(null)
                  }}
                >
                  {g.name}
                </MenuItem>
              ))}
            </MenuList>
          </div>
        </>
      )}
    </div>
  )
})
