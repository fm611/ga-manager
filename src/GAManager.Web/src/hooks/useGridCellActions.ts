import { useCallback, useState } from 'react'
import type { Address, GA, MainGroup } from '../domain/schema'

interface UseGridCellActionsArgs {
  selectedMainGroup: MainGroup | null
  insertCells: (kind: 'mainGroup', collectionId: string, middleGroups: number[], atRow: number, count: number) => void
  deleteCellsAndShift: (kind: 'mainGroup', collectionId: string, addresses: Address[]) => void
}

export function useGridCellActions({ selectedMainGroup, insertCells, deleteCellsAndShift }: UseGridCellActionsArgs) {
  const [gridSelection, setGridSelection] = useState<{ gas: GA[]; addresses: Address[] }>({ gas: [], addresses: [] })
  const [addCellsCount, setAddCellsCount] = useState('1')
  const [confirmDeleteCellsOpen, setConfirmDeleteCellsOpen] = useState(false)

  const handleAddCells = useCallback(() => {
    if (!selectedMainGroup) return
    const numRows = Number(addCellsCount)
    if (!Number.isFinite(numRows) || numRows <= 0) return
    const minRowByColumn = new Map<number, number>()
    for (const addr of gridSelection.addresses) {
      const current = minRowByColumn.get(addr.middleGroup)
      if (current === undefined || addr.ga < current) minRowByColumn.set(addr.middleGroup, addr.ga)
    }
    for (const [col, minRow] of minRowByColumn) {
      insertCells('mainGroup', selectedMainGroup.id, [col], minRow, numRows)
    }
  }, [selectedMainGroup, addCellsCount, gridSelection, insertCells])

  const handleDeleteCellsClick = useCallback(() => {
    if (!selectedMainGroup || gridSelection.addresses.length === 0) return
    if (gridSelection.gas.length > 0) {
      setConfirmDeleteCellsOpen(true)
    } else {
      deleteCellsAndShift('mainGroup', selectedMainGroup.id, gridSelection.addresses)
    }
  }, [selectedMainGroup, gridSelection, deleteCellsAndShift])

  const confirmDeleteCells = useCallback(() => {
    if (selectedMainGroup) deleteCellsAndShift('mainGroup', selectedMainGroup.id, gridSelection.addresses)
    setConfirmDeleteCellsOpen(false)
  }, [selectedMainGroup, gridSelection, deleteCellsAndShift])

  const cancelDeleteCells = useCallback(() => setConfirmDeleteCellsOpen(false), [])

  return {
    gridSelection,
    setGridSelection,
    addCellsCount,
    setAddCellsCount,
    confirmDeleteCellsOpen,
    handleAddCells,
    handleDeleteCellsClick,
    confirmDeleteCells,
    cancelDeleteCells,
  }
}
