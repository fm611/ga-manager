import { useCallback, useRef, useState } from 'react'
import {
  FluentProvider,
  Button,
  Input,
  Menu,
  MenuTrigger,
  MenuPopover,
  MenuList,
  MenuItem,
  Text,
  Title3,
  tokens,
  makeStyles,
} from '@fluentui/react-components'
import { TableInsertRowRegular, TableDeleteRowRegular, SearchRegular, DismissRegular } from '@fluentui/react-icons'
import type { Address, GA, MainGroup } from './domain/schema'
import { ProjectProvider, useProject } from './state/ProjectContext'
import { createEmptyProject, buildSampleProject, createMainGroup } from './domain/operations'
import { MainGroupPanel } from './components/MainGroupPanel'
import { GroupPanel } from './components/GroupPanel'
import { GaGrid, type GaGridHandle } from './components/GaGrid'
import { AddEditMainGroupDialog } from './components/dialogs/AddEditMainGroupDialog'
import { ConfirmDialog } from './components/dialogs/ConfirmDialog'
import { GroupTemplateManagerDialog } from './components/GroupTemplateManagerDialog'
import { knxDarkTheme } from './theme'

const useStyles = makeStyles({
  root: { display: 'flex', flexDirection: 'column', height: '100vh', width: '100vw', overflow: 'hidden' },
  menuBar: {
    display: 'flex',
    alignItems: 'center',
    gap: '8px',
    padding: '6px 10px',
    borderBottom: `1px solid ${tokens.colorNeutralStroke2}`,
    flexShrink: 0,
  },
  body: { display: 'flex', flex: 1, minHeight: 0, gap: '10px', padding: '10px' },
  leftColumn: { width: '300px', flexShrink: 0, display: 'flex', flexDirection: 'column', gap: '10px', minHeight: 0 },
  leftTop: { flex: '1 1 55%', minHeight: 0 },
  leftBottom: { flex: '1 1 45%', minHeight: 0 },
  rightColumn: { flex: 1, minWidth: 0, display: 'flex', flexDirection: 'column', gap: '8px' },
  gridToolbar: { display: 'flex', alignItems: 'center', gap: '8px', justifyContent: 'space-between' },
  gridWrapper: { flex: 1, minHeight: 0, borderRadius: tokens.borderRadiusMedium },
  gridWrapperFiltered: { boxShadow: `0 0 0 2px ${tokens.colorPaletteRedBorder2}` },
})

function MainContent() {
  const styles = useStyles()
  const { project, resetProject, addMainGroup, updateMainGroup, removeMainGroup, assignGAsToGroup, setCellGA, removeGAsFromCollection, renameSubGroupColumn, insertCells, deleteCellsAndShift } =
    useProject()

  const [selectedMainGroupId, setSelectedMainGroupId] = useState<string | null>(null)
  const [selectedGroupIds, setSelectedGroupIds] = useState<string[]>([])
  const [filterGroupIds, setFilterGroupIds] = useState<(string | null)[] | null>(null)
  const [searchQuery, setSearchQuery] = useState('')

  const [mainGroupDialog, setMainGroupDialog] = useState<{ open: boolean; editing: MainGroup | null }>({ open: false, editing: null })
  const [pendingDeleteMainGroup, setPendingDeleteMainGroup] = useState<MainGroup | null>(null)
  const [templateManagerOpen, setTemplateManagerOpen] = useState(false)

  const [gridSelection, setGridSelection] = useState<{ gas: GA[]; addresses: Address[] }>({ gas: [], addresses: [] })
  const [addCellsCount, setAddCellsCount] = useState('1')
  const [confirmDeleteCellsOpen, setConfirmDeleteCellsOpen] = useState(false)

  const gridRef = useRef<GaGridHandle>(null)

  const selectedMainGroup = project.mainGroups.find((m) => m.id === selectedMainGroupId) ?? null

  const noGroupFilterActive = filterGroupIds !== null && filterGroupIds.length === 1 && filterGroupIds[0] === null

  const handleGroupSelectionChange = useCallback((ids: string[]) => {
    setSelectedGroupIds(ids)
    setFilterGroupIds(ids.length > 0 ? ids : null)
  }, [])

  const handleToggleNoGroupFilter = useCallback(() => {
    if (noGroupFilterActive) {
      setFilterGroupIds(null)
    } else {
      setSelectedGroupIds([])
      setFilterGroupIds([null])
    }
  }, [noGroupFilterActive])

  const handleClearFilter = useCallback(() => {
    setSelectedGroupIds([])
    setFilterGroupIds(null)
  }, [])

  const handleOpenTemplateManager = useCallback(() => {
    if (!selectedMainGroup) {
      alert('Bitte erst eine Hauptgruppe erstellen/auswählen.')
      return
    }
    setTemplateManagerOpen(true)
  }, [selectedMainGroup])

  const handleAddMainGroup = useCallback(() => setMainGroupDialog({ open: true, editing: null }), [])
  const handleEditMainGroup = useCallback((mg: MainGroup) => setMainGroupDialog({ open: true, editing: mg }), [])

  function handleAddCells() {
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
  }

  function handleDeleteCellsClick() {
    if (!selectedMainGroup || gridSelection.addresses.length === 0) return
    if (gridSelection.gas.length > 0) {
      setConfirmDeleteCellsOpen(true)
    } else {
      deleteCellsAndShift('mainGroup', selectedMainGroup.id, gridSelection.addresses)
    }
  }

  return (
    <div className={styles.root}>
      <div className={styles.menuBar}>
        <Menu>
          <MenuTrigger disableButtonEnhancement>
            <Button appearance="subtle">Datei</Button>
          </MenuTrigger>
          <MenuPopover>
            <MenuList>
              <MenuItem onClick={() => resetProject(createEmptyProject())}>Neu</MenuItem>
              <MenuItem onClick={() => resetProject(buildSampleProject())}>Beispiel laden</MenuItem>
            </MenuList>
          </MenuPopover>
        </Menu>

        <Button appearance="subtle" onClick={handleOpenTemplateManager}>
          Gruppen Manager
        </Button>

        <div style={{ flex: 1 }} />

        <Input
          size="small"
          style={{ width: '60px' }}
          value={addCellsCount}
          onChange={(_, data) => {
            if (/^\d*$/.test(data.value)) setAddCellsCount(data.value)
          }}
        />
        <Button
          appearance="subtle"
          icon={<TableInsertRowRegular />}
          title="Zellen einfügen"
          disabled={!selectedMainGroup || gridSelection.addresses.length === 0}
          onClick={handleAddCells}
        />
        <Button
          appearance="subtle"
          icon={<TableDeleteRowRegular />}
          title="Zellen löschen"
          disabled={!selectedMainGroup || gridSelection.addresses.length === 0}
          onClick={handleDeleteCellsClick}
        />

        <Title3 style={{ marginLeft: '16px' }}>Gruppenadressen Manager</Title3>
      </div>

      <div className={styles.body}>
        <div className={styles.leftColumn}>
          <div className={styles.leftTop}>
            <MainGroupPanel
              mainGroups={project.mainGroups}
              selectedId={selectedMainGroupId}
              onSelect={setSelectedMainGroupId}
              onAdd={handleAddMainGroup}
              onEdit={handleEditMainGroup}
              onDelete={setPendingDeleteMainGroup}
            />
          </div>
          <div className={styles.leftBottom}>
            <GroupPanel
              selectedIds={selectedGroupIds}
              onSelectionChange={handleGroupSelectionChange}
              onOpenTemplateManager={handleOpenTemplateManager}
              noGroupFilterActive={noGroupFilterActive}
              onToggleNoGroupFilter={handleToggleNoGroupFilter}
              onClearFilter={handleClearFilter}
              filterDisabled={!selectedMainGroup}
            />
          </div>
        </div>

        <div className={styles.rightColumn}>
          <div className={styles.gridToolbar}>
            <Text weight="semibold">{selectedMainGroup ? `Gruppenadressen – ${selectedMainGroup.subAddress} - ${selectedMainGroup.name}` : 'Gruppenadressen'}</Text>
            <div style={{ display: 'flex', gap: '6px', alignItems: 'center' }}>
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
                      onClick={() => setSearchQuery('')}
                    />
                  ) : undefined
                }
                placeholder="Suche nach Name oder Adresse"
                value={searchQuery}
                onChange={(_, data) => setSearchQuery(data.value)}
                style={{ width: '220px' }}
              />
            </div>
          </div>

          <div className={styles.gridWrapper + (filterGroupIds ? ' ' + styles.gridWrapperFiltered : '')}>
            {selectedMainGroup ? (
              <GaGrid
                ref={gridRef}
                key={selectedMainGroup.id}
                gas={selectedMainGroup.gas}
                subGroupNames={selectedMainGroup.subGroupNames}
                mainGroupSubAddress={selectedMainGroup.subAddress}
                readOnly={false}
                filterGroupIds={filterGroupIds}
                searchQuery={searchQuery}
                enableGroupContextMenu
                groups={project.groups}
                onRenameColumn={(columnIndex, name) => renameSubGroupColumn('mainGroup', selectedMainGroup.id, columnIndex, name)}
                onCommitCell={(middleGroup, ga, name) => setCellGA('mainGroup', selectedMainGroup.id, selectedMainGroup.subAddress, middleGroup, ga, name)}
                onDeleteGAs={(gaIds) => removeGAsFromCollection('mainGroup', selectedMainGroup.id, gaIds)}
                onAssignToGroup={(gaIds, groupId) => assignGAsToGroup(selectedMainGroup.id, gaIds, groupId)}
                onSelectionChange={(gas, addresses) => setGridSelection({ gas, addresses })}
              />
            ) : (
              <div style={{ padding: '24px' }}>
                <Text>Bitte eine Hauptgruppe erstellen oder auswählen.</Text>
              </div>
            )}
          </div>
        </div>
      </div>

      <AddEditMainGroupDialog
        open={mainGroupDialog.open}
        mainGroups={project.mainGroups}
        editing={mainGroupDialog.editing}
        onSave={(values) => {
          if (mainGroupDialog.editing) {
            updateMainGroup(mainGroupDialog.editing.id, values)
          } else {
            const mg = createMainGroup(values.subAddress, values.name, values.defaultBlockLength)
            addMainGroup(mg)
            setSelectedMainGroupId(mg.id)
          }
          setMainGroupDialog({ open: false, editing: null })
        }}
        onCancel={() => setMainGroupDialog({ open: false, editing: null })}
      />

      <ConfirmDialog
        open={pendingDeleteMainGroup !== null}
        title="Hauptgruppe löschen"
        message="Haupgruppe löschen?"
        onConfirm={() => {
          if (pendingDeleteMainGroup) {
            removeMainGroup(pendingDeleteMainGroup.id)
            if (selectedMainGroupId === pendingDeleteMainGroup.id) setSelectedMainGroupId(null)
          }
          setPendingDeleteMainGroup(null)
        }}
        onCancel={() => setPendingDeleteMainGroup(null)}
      />

      <ConfirmDialog
        open={confirmDeleteCellsOpen}
        title="Gruppenadressen löschen"
        message="Gruppenadressen löschen?"
        onConfirm={() => {
          if (selectedMainGroup) deleteCellsAndShift('mainGroup', selectedMainGroup.id, gridSelection.addresses)
          setConfirmDeleteCellsOpen(false)
        }}
        onCancel={() => setConfirmDeleteCellsOpen(false)}
      />

      <GroupTemplateManagerDialog
        open={templateManagerOpen}
        initialMainGroupId={selectedMainGroupId}
        onClose={() => setTemplateManagerOpen(false)}
        onGroupInserted={(mainGroupId, _group, firstRow) => {
          setSelectedMainGroupId(mainGroupId)
          requestAnimationFrame(() => gridRef.current?.scrollToRow(firstRow))
        }}
      />
    </div>
  )
}

function App() {
  return (
    <FluentProvider theme={knxDarkTheme} style={{ height: '100vh', width: '100vw' }}>
      <ProjectProvider project={createEmptyProject()}>
        <MainContent />
      </ProjectProvider>
    </FluentProvider>
  )
}

export default App
