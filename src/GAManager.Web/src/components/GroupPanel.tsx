import { memo, useState } from 'react'
import { Button, ToggleButton, Listbox, Option, Text } from '@fluentui/react-components'
import { AddRegular, EditRegular, DeleteRegular, LayerRegular, FilterRegular, FilterDismissRegular } from '@fluentui/react-icons'
import type { Group } from '../domain/schema'
import { useProject } from '../state/ProjectContext'
import { createGroup, getGroupGAs } from '../domain/operations'
import { ContextMenu, type ContextMenuState } from './ContextMenu'
import { TextPromptDialog } from './dialogs/TextPromptDialog'
import { DeleteGroupDialog } from './dialogs/DeleteGroupDialog'
import { ConfirmDialog } from './dialogs/ConfirmDialog'
import { useStyles } from './GroupPanel.styles'

interface GroupPanelProps {
  selectedIds: string[]
  onSelectionChange: (ids: string[]) => void
  onOpenTemplateManager: () => void
  noGroupFilterActive: boolean
  onToggleNoGroupFilter: () => void
  onClearFilter: () => void
  filterDisabled?: boolean
}

export const GroupPanel = memo(function GroupPanel({
  selectedIds,
  onSelectionChange,
  onOpenTemplateManager,
  noGroupFilterActive,
  onToggleNoGroupFilter,
  onClearFilter,
  filterDisabled,
}: GroupPanelProps) {
  const styles = useStyles()
  const { project, addGroup, renameGroup, removeGroups } = useProject()
  const sorted = [...project.groups].sort((a, b) => a.name.localeCompare(b.name))

  const [contextMenu, setContextMenu] = useState<ContextMenuState | null>(null)
  const [newGroupDialogOpen, setNewGroupDialogOpen] = useState(false)
  const [editingGroup, setEditingGroup] = useState<Group | null>(null)
  const [deleteCandidate, setDeleteCandidate] = useState<Group | null>(null)
  const [confirmSimpleDelete, setConfirmSimpleDelete] = useState<Group | null>(null)

  const singleSelected = selectedIds.length === 1 ? project.groups.find((g) => g.id === selectedIds[0]) ?? null : null

  function openContextMenu(e: React.MouseEvent, group: Group | null) {
    e.preventDefault()
    if (group && !selectedIds.includes(group.id)) onSelectionChange([group.id])
    setContextMenu({
      x: e.clientX,
      y: e.clientY,
      items: [
        { key: 'new-empty', label: 'Neu (leer)', onClick: () => setNewGroupDialogOpen(true) },
        { key: 'new-template', label: 'Neu (Template)', onClick: onOpenTemplateManager },
        { key: 'edit', label: 'Bearbeiten', onClick: () => group && setEditingGroup(group), disabled: !group },
        { key: 'delete', label: 'Löschen', onClick: () => group && startDelete(group), disabled: !group },
      ],
    })
  }

  function startDelete(group: Group) {
    const gas = getGroupGAs(project, group.id)
    if (gas.length === 0) {
      setConfirmSimpleDelete(group)
    } else {
      setDeleteCandidate(group)
    }
  }

  function handleOptionSelect(ids: string[]) {
    onSelectionChange(ids)
  }

  return (
    <div className={styles.root}>
      <div className={styles.header}>
        <Text weight="semibold">Gruppen</Text>
        <div className={styles.buttonRow}>
          <Button size="small" appearance="subtle" icon={<AddRegular />} title="Neu (leer)" onClick={() => setNewGroupDialogOpen(true)} />
          <Button size="small" appearance="subtle" icon={<LayerRegular />} title="Neu (Template)" onClick={onOpenTemplateManager} />
          <Button
            size="small"
            appearance="subtle"
            icon={<EditRegular />}
            title="Bearbeiten"
            disabled={!singleSelected}
            onClick={() => singleSelected && setEditingGroup(singleSelected)}
          />
          <Button
            size="small"
            appearance="subtle"
            icon={<DeleteRegular />}
            title="Löschen"
            disabled={selectedIds.length === 0}
            onClick={() => {
              const first = project.groups.find((g) => g.id === selectedIds[0])
              if (first) startDelete(first)
            }}
          />
          <div className={styles.filterButtons}>
            <ToggleButton
              size="small"
              appearance="subtle"
              icon={<FilterRegular />}
              title="Ohne Gruppe"
              checked={noGroupFilterActive}
              onClick={onToggleNoGroupFilter}
              disabled={filterDisabled}
            />
            <Button
              size="small"
              appearance="subtle"
              icon={<FilterDismissRegular />}
              title="Filter löschen"
              onClick={onClearFilter}
              disabled={selectedIds.length === 0 && !noGroupFilterActive}
            />
          </div>
        </div>
      </div>
      <div className={styles.list} onContextMenu={(e) => openContextMenu(e, singleSelected)}>
        <Listbox
          multiselect
          selectedOptions={selectedIds}
          onOptionSelect={(_, data) => handleOptionSelect(data.selectedOptions)}
        >
          {sorted.map((g) => (
            <Option key={g.id} value={g.id} text={g.name} onContextMenu={(e) => openContextMenu(e, g)}>
              {g.name}
            </Option>
          ))}
        </Listbox>
      </div>

      <ContextMenu state={contextMenu} onClose={() => setContextMenu(null)} />

      <TextPromptDialog
        open={newGroupDialogOpen}
        title="Gruppe"
        initialValue="Neue Gruppe"
        onSubmit={(value) => {
          addGroup(createGroup(value))
          setNewGroupDialogOpen(false)
        }}
        onCancel={() => setNewGroupDialogOpen(false)}
      />

      <TextPromptDialog
        open={editingGroup !== null}
        title="Gruppe"
        initialValue={editingGroup?.name ?? ''}
        onSubmit={(value) => {
          if (editingGroup) renameGroup(editingGroup.id, value)
          setEditingGroup(null)
        }}
        onCancel={() => setEditingGroup(null)}
      />

      <ConfirmDialog
        open={confirmSimpleDelete !== null}
        title="Gruppe löschen"
        message="Möchten Sie die Gruppe wirklich löschen?"
        onConfirm={() => {
          if (confirmSimpleDelete) {
            removeGroups([confirmSimpleDelete.id], false)
            onSelectionChange(selectedIds.filter((id) => id !== confirmSimpleDelete.id))
          }
          setConfirmSimpleDelete(null)
        }}
        onCancel={() => setConfirmSimpleDelete(null)}
      />

      <DeleteGroupDialog
        open={deleteCandidate !== null}
        gas={deleteCandidate ? getGroupGAs(project, deleteCandidate.id) : []}
        onDeleteWithGAs={() => {
          if (deleteCandidate) {
            removeGroups([deleteCandidate.id], true)
            onSelectionChange(selectedIds.filter((id) => id !== deleteCandidate.id))
          }
          setDeleteCandidate(null)
        }}
        onDeleteGroupOnly={() => {
          if (deleteCandidate) {
            removeGroups([deleteCandidate.id], false)
            onSelectionChange(selectedIds.filter((id) => id !== deleteCandidate.id))
          }
          setDeleteCandidate(null)
        }}
        onCancel={() => setDeleteCandidate(null)}
      />
    </div>
  )
})
