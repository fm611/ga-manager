import { useCallback, useEffect, useRef, useState } from 'react'
import {
  FluentProvider,
  Button,
  Divider,
  Input,
  Menu,
  MenuTrigger,
  MenuPopover,
  MenuList,
  MenuItem,
  MenuDivider,
  Text,
  tokens,
  makeStyles,
} from '@fluentui/react-components'
import {
  TableInsertRowRegular,
  TableDeleteRowRegular,
  SearchRegular,
  DismissRegular,
  ArrowUndoRegular,
  ArrowRedoRegular,
  SaveRegular,
  ChevronDownRegular,
} from '@fluentui/react-icons'
import type { Address, GA, MainGroup, Project } from './domain/schema'
import { ProjectSchema } from './domain/schema'
import { ProjectProvider, useProject } from './state/ProjectContext'
import { createEmptyProject, buildSampleProject, createMainGroup } from './domain/operations'
import { buildGaExportCsv } from './domain/csvExport'
import { parseGaImportCsv } from './domain/csvImport'
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
} from './host/wpfBridge'
import { MainGroupPanel } from './components/MainGroupPanel'
import { GroupPanel } from './components/GroupPanel'
import { GaGrid, type GaGridHandle } from './components/GaGrid'
import { AddEditMainGroupDialog } from './components/dialogs/AddEditMainGroupDialog'
import { ConfirmDialog } from './components/dialogs/ConfirmDialog'
import { GroupTemplateManagerDialog } from './components/GroupTemplateManagerDialog'
import { knxDarkTheme } from './theme'
import logo from './assets/logo.svg'

const useStyles = makeStyles({
  root: { display: 'flex', flexDirection: 'column', height: '100vh', width: '100vw', overflow: 'hidden' },
  header: {
    display: 'flex',
    flexDirection: 'column',
    borderBottom: `1px solid ${tokens.colorNeutralStroke2}`,
    flexShrink: 0,
  },
  menuBar: {
    display: 'flex',
    alignItems: 'center',
    gap: '12px',
    padding: '6px 10px',
  },
  logo: { height: '48px', width: 'auto', padding: "2px"},
  body: { display: 'flex', flex: 1, minHeight: 0, gap: '10px', padding: '10px' },
  leftColumn: { width: '300px', flexShrink: 0, display: 'flex', flexDirection: 'column', gap: '10px', minHeight: 0 },
  leftTop: { flex: '1 1 55%', minHeight: 0 },
  leftBottom: { flex: '1 1 45%', minHeight: 0 },
  rightColumn: { flex: 1, minWidth: 0, display: 'flex', flexDirection: 'column', gap: '8px' },
  gridToolbar: { display: 'flex', alignItems: 'center', gap: '8px', justifyContent: 'space-between' },
  gridWrapper: { flex: 1, minHeight: 0, borderRadius: tokens.borderRadiusMedium },
  gridWrapperFiltered: { boxShadow: `0 0 0 2px ${tokens.colorPaletteRedBorder2}` },
})

function parseProjectJson(json: string): Project | null {
  try {
    const parsed = ProjectSchema.safeParse(JSON.parse(json))
    return parsed.success ? parsed.data : null
  } catch {
    return null
  }
}

function MainContent() {
  const styles = useStyles()
  const {
    project,
    canUndo,
    canRedo,
    dirty,
    undo,
    redo,
    resetProject,
    markClean,
    addMainGroup,
    updateMainGroup,
    removeMainGroup,
    assignGAsToGroup,
    setCellGA,
    removeGAsFromCollection,
    renameSubGroupColumn,
    insertCells,
    deleteCellsAndShift,
  } = useProject()

  const [selectedMainGroupId, setSelectedMainGroupId] = useState<string | null>(null)
  const [selectedGroupIds, setSelectedGroupIds] = useState<string[]>([])
  const [filterGroupIds, setFilterGroupIds] = useState<(string | null)[] | null>(null)
  const [searchQuery, setSearchQuery] = useState('')

  const [mainGroupDialog, setMainGroupDialog] = useState<{ open: boolean; editing: MainGroup | null }>({ open: false, editing: null })
  const [pendingDeleteMainGroup, setPendingDeleteMainGroup] = useState<MainGroup | null>(null)
  const [templateManagerOpen, setTemplateManagerOpen] = useState(false)
  const [pendingReset, setPendingReset] = useState<{ project: Project; clearHostFile: boolean } | null>(null)

  const [gridSelection, setGridSelection] = useState<{ gas: GA[]; addresses: Address[] }>({ gas: [], addresses: [] })
  const [addCellsCount, setAddCellsCount] = useState('1')
  const [confirmDeleteCellsOpen, setConfirmDeleteCellsOpen] = useState(false)
  const [recentFiles, setRecentFiles] = useState<RecentFile[]>([])

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

  const applyProjectReset = useCallback(
    (newProject: Project) => {
      resetProject(newProject)
      const firstMainGroup = [...newProject.mainGroups].sort((a, b) => a.subAddress - b.subAddress)[0]
      setSelectedMainGroupId(firstMainGroup?.id ?? null)
    },
    [resetProject],
  )

  const handleResetRequest = useCallback(
    (newProject: Project, options?: { clearHostFile?: boolean }) => {
      const clearHostFile = options?.clearHostFile ?? false
      if (dirty) {
        setPendingReset({ project: newProject, clearHostFile })
      } else {
        if (clearHostFile) notifyNewProject()
        applyProjectReset(newProject)
      }
    },
    [dirty, applyProjectReset],
  )

  const projectRef = useRef(project)
  useEffect(() => {
    projectRef.current = project
  }, [project])

  useEffect(() => {
    initHostBridge({
      getProjectJson: () => JSON.stringify(projectRef.current),
      onFileOpened: (result) => {
        const parsed = parseProjectJson(result.content)
        if (!parsed) {
          alert(`Datei ist kein gültiges Projekt:\n${result.path}`)
          return
        }
        handleResetRequest(parsed)
      },
      onRecentFilesChanged: setRecentFiles,
    })
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])

  useEffect(() => {
    reportDirty(dirty)
  }, [dirty])

  const handleOpenFile = useCallback(async () => {
    const result = await requestOpenFile()
    if (!result) return
    const parsed = parseProjectJson(result.content)
    if (!parsed) {
      alert(`Datei ist kein gültiges Projekt:\n${result.path}`)
      return
    }
    handleResetRequest(parsed)
  }, [handleResetRequest])

  const handleOpenRecent = useCallback(
    async (path: string) => {
      const result = await requestOpenRecentFile(path)
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

  const handleDeleteCellsClick = useCallback(() => {
    if (!selectedMainGroup || gridSelection.addresses.length === 0) return
    if (gridSelection.gas.length > 0) {
      setConfirmDeleteCellsOpen(true)
    } else {
      deleteCellsAndShift('mainGroup', selectedMainGroup.id, gridSelection.addresses)
    }
  }, [selectedMainGroup, gridSelection, deleteCellsAndShift])

  useEffect(() => {
    function isTextEntryTarget(el: EventTarget | null): boolean {
      if (!(el instanceof HTMLElement)) return false
      return el.tagName === 'INPUT' || el.tagName === 'TEXTAREA' || el.isContentEditable
    }
    function handleKeyDown(e: KeyboardEvent) {
      if (!(e.ctrlKey || e.metaKey) || isTextEntryTarget(document.activeElement)) return
      const key = e.key.toLowerCase()
      if (key === 'z' && !e.shiftKey) {
        e.preventDefault()
        undo()
      } else if (key === 'y' || (key === 'z' && e.shiftKey)) {
        e.preventDefault()
        redo()
      } else if (key === 's') {
        e.preventDefault()
        void handleSave()
      } else if (key === 'o') {
        e.preventDefault()
        void handleOpenFile()
      } else if (key === 'delete') {
        e.preventDefault()
        handleDeleteCellsClick()
      }
    }
    window.addEventListener('keydown', handleKeyDown)
    return () => window.removeEventListener('keydown', handleKeyDown)
  }, [undo, redo, handleSave, handleOpenFile, handleDeleteCellsClick])

  return (
    <div className={styles.root}>
      <div className={styles.header}>
        <div className={styles.menuBar}>
          <img src={logo} alt="Gruppenadressen Manager" className={styles.logo} />

          <Divider vertical style={{ height: '28px', flexGrow: 0 }} />

          <Menu>
            <MenuTrigger disableButtonEnhancement>
              <Button appearance="outline" icon={<ChevronDownRegular />} iconPosition="after">
                Datei
              </Button>
            </MenuTrigger>
            <MenuPopover>
              <MenuList>
                <MenuItem onClick={() => handleResetRequest(createEmptyProject(), { clearHostFile: true })}>Neu</MenuItem>
                <Menu>
                  <MenuTrigger disableButtonEnhancement>
                    <MenuItem>Öffnen</MenuItem>
                  </MenuTrigger>
                  <MenuPopover>
                    <MenuList>
                      <MenuItem onClick={handleOpenFile}>Datei…</MenuItem>
                      <MenuDivider />
                      <MenuItem onClick={() => handleResetRequest(buildSampleProject(), { clearHostFile: true })}>Beispiel</MenuItem>
                      {recentFiles.length > 0 && <MenuDivider />}
                      {recentFiles.map((file) => (
                        <MenuItem key={file.path} onClick={() => handleOpenRecent(file.path)} title={file.path}>
                          {file.name}
                        </MenuItem>
                      ))}
                    </MenuList>
                  </MenuPopover>
                </Menu>
                <MenuItem onClick={handleSave}>Speichern</MenuItem>
                <MenuDivider />
                <MenuItem onClick={handleExport}>Export</MenuItem>
                <MenuItem onClick={handleImport}>Import</MenuItem>
              </MenuList>
            </MenuPopover>
          </Menu>

          <Button appearance="outline" onClick={handleOpenTemplateManager}>
            Template Manager
          </Button>

          {/* <Divider vertical style={{ height: '28px', flexGrow: 0 }} /> */}

          <Button size="small" appearance="subtle" icon={<ArrowUndoRegular />} title="Rückgängig (Strg+Z)" disabled={!canUndo} onClick={undo} />
          <Button size="small" appearance="subtle" icon={<ArrowRedoRegular />} title="Wiederholen (Strg+Y)" disabled={!canRedo} onClick={redo} />
          <Button
            size="small"
            appearance="subtle"
            icon={<SaveRegular />}
            title="Speichern (Strg+S)"
            style={dirty ? undefined : { opacity: 0.5 }}
            onClick={handleSave}
          />
        </div>
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
            <Text weight="semibold">{selectedMainGroup ? `Hauptgruppe: ${selectedMainGroup.subAddress} - ${selectedMainGroup.name}` : 'Gruppenadressen'}</Text>

            <div style={{ display: 'flex', alignItems: 'center', gap: '6px' }}>
              <Input
                size="small"
                style={{ width: '50px', margin: '2px' }}
                input={{ style: { textAlign: 'right' } }}
                value={addCellsCount}
                onChange={(_, data) => {
                  if (/^\d*$/.test(data.value)) setAddCellsCount(data.value)
                }}
              />
              <Button
                size="small"
                appearance="subtle"
                icon={<TableInsertRowRegular />}
                title="Zellen einfügen"
                disabled={!selectedMainGroup || gridSelection.addresses.length === 0}
                onClick={handleAddCells}
              />
              <Button
                size="small"
                appearance="subtle"
                icon={<TableDeleteRowRegular />}
                title="Zellen löschen (Strg+Entf)"
                disabled={!selectedMainGroup || gridSelection.addresses.length === 0}
                onClick={handleDeleteCellsClick}
              />

              {/* <Divider vertical style={{ height: '16px', flexGrow: 0 }} /> */}

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

      <ConfirmDialog
        open={pendingReset !== null}
        title="Änderungen verwerfen?"
        message="Es gibt nicht gespeicherte Änderungen. Möchten Sie diese verwerfen?"
        confirmText="Verwerfen"
        danger
        onConfirm={() => {
          if (pendingReset) {
            if (pendingReset.clearHostFile) notifyNewProject()
            applyProjectReset(pendingReset.project)
          }
          setPendingReset(null)
        }}
        onCancel={() => setPendingReset(null)}
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
