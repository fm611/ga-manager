import { useCallback, useRef, useState } from 'react'
import { FluentProvider } from '@fluentui/react-components'
import type { MainGroup, Project } from './domain/schema'
import { ProjectProvider, useProject } from './state/ProjectContext'
import { createEmptyProject, buildSampleProject, createMainGroup } from './domain/operations'
import { type GaGridHandle } from './components/GaGrid'
import { AddEditMainGroupDialog } from './components/dialogs/AddEditMainGroupDialog'
import { ConfirmDialog } from './components/dialogs/ConfirmDialog'
import { GroupTemplateManagerDialog } from './components/GroupTemplateManagerDialog'
import { AppHeader } from './components/layout/AppHeader'
import { Sidebar } from './components/layout/Sidebar'
import { MainGridPanel } from './components/layout/MainGridPanel'
import { AppFooter } from './components/layout/AppFooter'
import { useProjectFileOperations } from './hooks/useProjectFileOperations'
import { useGridCellActions } from './hooks/useGridCellActions'
import { useKeyboardShortcuts } from './hooks/useKeyboardShortcuts'
import { I18nProvider, useI18n } from './i18n/I18nContext'
import { knxDarkTheme } from './theme'
import { useStyles } from './App.styles'

function MainContent() {
  const styles = useStyles()
  const { t, locale } = useI18n()
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
      alert(t('app.selectMainGroupFirst'))
      return
    }
    setTemplateManagerOpen(true)
  }, [selectedMainGroup, t])

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

  const fileOps = useProjectFileOperations({ project, dirty, markClean, applyReset: applyProjectReset })
  const gridActions = useGridCellActions({ selectedMainGroup, insertCells, deleteCellsAndShift })

  useKeyboardShortcuts({
    undo,
    redo,
    onSave: fileOps.handleSave,
    onSaveAs: fileOps.handleSaveAs,
    onOpen: fileOps.handleOpenFile,
    onDeleteCells: gridActions.handleDeleteCellsClick,
  })

  return (
    <div className={styles.root}>
      <AppHeader
        canUndo={canUndo}
        canRedo={canRedo}
        dirty={dirty}
        onUndo={undo}
        onRedo={redo}
        onSave={fileOps.handleSave}
        onSaveAs={fileOps.handleSaveAs}
        onNew={() => fileOps.handleResetRequest(createEmptyProject(), { clearHostFile: true })}
        onOpenFile={fileOps.handleOpenFile}
        onOpenSample={() => fileOps.handleResetRequest(buildSampleProject(locale), { clearHostFile: true })}
        recentFiles={fileOps.recentFiles}
        onOpenRecent={fileOps.handleOpenRecent}
        onExport={fileOps.handleExport}
        onImport={fileOps.handleImport}
        onOpenTemplateManager={handleOpenTemplateManager}
      />

      <div className={styles.body}>
        <Sidebar
          mainGroups={project.mainGroups}
          selectedMainGroupId={selectedMainGroupId}
          onSelectMainGroup={setSelectedMainGroupId}
          onAddMainGroup={handleAddMainGroup}
          onEditMainGroup={handleEditMainGroup}
          onDeleteMainGroup={setPendingDeleteMainGroup}
          selectedGroupIds={selectedGroupIds}
          onGroupSelectionChange={handleGroupSelectionChange}
          onOpenTemplateManager={handleOpenTemplateManager}
          noGroupFilterActive={noGroupFilterActive}
          onToggleNoGroupFilter={handleToggleNoGroupFilter}
          onClearFilter={handleClearFilter}
          filterDisabled={!selectedMainGroup}
        />

        <MainGridPanel
          ref={gridRef}
          selectedMainGroup={selectedMainGroup}
          groups={project.groups}
          filterGroupIds={filterGroupIds}
          searchQuery={searchQuery}
          onSearchQueryChange={setSearchQuery}
          addCellsCount={gridActions.addCellsCount}
          onAddCellsCountChange={gridActions.setAddCellsCount}
          gridSelection={gridActions.gridSelection}
          onAddCells={gridActions.handleAddCells}
          onDeleteCellsClick={gridActions.handleDeleteCellsClick}
          onRenameSubGroupColumn={(mainGroupId, columnIndex, name) => renameSubGroupColumn('mainGroup', mainGroupId, columnIndex, name)}
          onCommitCell={(mainGroupId, mainGroupSubAddress, middleGroup, ga, name) =>
            setCellGA('mainGroup', mainGroupId, mainGroupSubAddress, middleGroup, ga, name)
          }
          onDeleteGAs={(mainGroupId, gaIds) => removeGAsFromCollection('mainGroup', mainGroupId, gaIds)}
          onAssignToGroup={(mainGroupId, gaIds, groupId) => assignGAsToGroup(mainGroupId, gaIds, groupId)}
          onGridSelectionChange={(gas, addresses) => gridActions.setGridSelection({ gas, addresses })}
        />
      </div>

      <AppFooter />

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
        title={t('mainGroupPanel.confirmDeleteTitle')}
        message={t('mainGroupPanel.confirmDeleteMessage')}
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
        open={gridActions.confirmDeleteCellsOpen}
        title={t('grid.confirmDeleteTitle')}
        message={t('grid.confirmDeleteMessage')}
        onConfirm={gridActions.confirmDeleteCells}
        onCancel={gridActions.cancelDeleteCells}
      />

      <ConfirmDialog
        open={fileOps.pendingReset !== null}
        title={t('fileOps.confirmDiscardTitle')}
        message={t('fileOps.confirmDiscardMessage')}
        confirmText={t('fileOps.confirmDiscardButton')}
        danger
        onConfirm={fileOps.confirmPendingReset}
        onCancel={fileOps.cancelPendingReset}
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
      <I18nProvider>
        <ProjectProvider project={createEmptyProject()}>
          <MainContent />
        </ProjectProvider>
      </I18nProvider>
    </FluentProvider>
  )
}

export default App
