import { forwardRef } from 'react'
import { Button, Input, Text } from '@fluentui/react-components'
import { TableInsertRowRegular, TableDeleteRowRegular, SearchRegular, DismissRegular } from '@fluentui/react-icons'
import type { Address, GA, Group, MainGroup } from '../../domain/schema'
import { GaGrid, type GaGridHandle } from '../GaGrid'
import { useStyles } from './MainGridPanel.styles'

export interface MainGridPanelProps {
  selectedMainGroup: MainGroup | null
  groups: Group[]
  filterGroupIds: (string | null)[] | null
  searchQuery: string
  onSearchQueryChange: (value: string) => void
  addCellsCount: string
  onAddCellsCountChange: (value: string) => void
  gridSelection: { gas: GA[]; addresses: Address[] }
  onAddCells: () => void
  onDeleteCellsClick: () => void
  onRenameSubGroupColumn: (mainGroupId: string, columnIndex: number, name: string) => void
  onCommitCell: (mainGroupId: string, mainGroupSubAddress: number, middleGroup: number, ga: number, name: string) => void
  onDeleteGAs: (mainGroupId: string, gaIds: string[]) => void
  onAssignToGroup: (mainGroupId: string, gaIds: string[], groupId: string) => void
  onGridSelectionChange: (gas: GA[], addresses: Address[]) => void
}

export const MainGridPanel = forwardRef<GaGridHandle, MainGridPanelProps>(function MainGridPanel(
  {
    selectedMainGroup,
    groups,
    filterGroupIds,
    searchQuery,
    onSearchQueryChange,
    addCellsCount,
    onAddCellsCountChange,
    gridSelection,
    onAddCells,
    onDeleteCellsClick,
    onRenameSubGroupColumn,
    onCommitCell,
    onDeleteGAs,
    onAssignToGroup,
    onGridSelectionChange,
  },
  ref,
) {
  const styles = useStyles()

  return (
    <div className={styles.rightColumn}>
      <div className={styles.gridToolbar}>
        <Text weight="semibold">{selectedMainGroup ? `Hauptgruppe: ${selectedMainGroup.subAddress} - ${selectedMainGroup.name}` : 'Gruppenadressen'}</Text>

        <div style={{ display: 'flex', alignItems: 'center', gap: '6px' }}>
          <Input
            size="small"
            style={{ width: '50px', margin: '2px' }}
            input={{ style: { textAlign: 'right' } }}
            value={addCellsCount}
            onChange={(_, data) => {
              if (/^\d*$/.test(data.value)) onAddCellsCountChange(data.value)
            }}
          />
          <Button
            size="small"
            appearance="subtle"
            icon={<TableInsertRowRegular />}
            title="Zellen einfügen"
            disabled={!selectedMainGroup || gridSelection.addresses.length === 0}
            onClick={onAddCells}
          />
          <Button
            size="small"
            appearance="subtle"
            icon={<TableDeleteRowRegular />}
            title="Zellen löschen (Strg+Entf)"
            disabled={!selectedMainGroup || gridSelection.addresses.length === 0}
            onClick={onDeleteCellsClick}
          />

          <Input
            size="small"
            contentBefore={<SearchRegular />}
            contentAfter={
              searchQuery ? (
                <Button
                  appearance="transparent"
                  size="small"
                  icon={<DismissRegular />}
                  title="Filter löschen"
                  onClick={() => onSearchQueryChange('')}
                />
              ) : undefined
            }
            placeholder="Suche nach Name oder Adresse"
            value={searchQuery}
            onChange={(_, data) => onSearchQueryChange(data.value)}
            style={{ width: '220px' }}
          />
        </div>
      </div>

      <div className={styles.gridWrapper + (filterGroupIds ? ' ' + styles.gridWrapperFiltered : '')}>
        {selectedMainGroup ? (
          <GaGrid
            ref={ref}
            key={selectedMainGroup.id}
            gas={selectedMainGroup.gas}
            subGroupNames={selectedMainGroup.subGroupNames}
            mainGroupSubAddress={selectedMainGroup.subAddress}
            readOnly={false}
            filterGroupIds={filterGroupIds}
            searchQuery={searchQuery}
            enableGroupContextMenu
            groups={groups}
            onRenameColumn={(columnIndex, name) => onRenameSubGroupColumn(selectedMainGroup.id, columnIndex, name)}
            onCommitCell={(middleGroup, ga, name) => onCommitCell(selectedMainGroup.id, selectedMainGroup.subAddress, middleGroup, ga, name)}
            onDeleteGAs={(gaIds) => onDeleteGAs(selectedMainGroup.id, gaIds)}
            onAssignToGroup={(gaIds, groupId) => onAssignToGroup(selectedMainGroup.id, gaIds, groupId)}
            onSelectionChange={onGridSelectionChange}
          />
        ) : (
          <div style={{ padding: '24px' }}>
            <Text>Bitte eine Hauptgruppe erstellen oder auswählen.</Text>
          </div>
        )}
      </div>
    </div>
  )
})
