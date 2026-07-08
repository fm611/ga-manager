import { memo, useState } from 'react'
import { Button, Listbox, Option, Text, tokens, makeStyles } from '@fluentui/react-components'
import { AddRegular, EditRegular, DeleteRegular } from '@fluentui/react-icons'
import type { MainGroup } from '../domain/schema'
import { MIN_MAIN_GROUP, MAX_MAIN_GROUP } from '../domain/schema'
import { mainGroupListLabel } from '../domain/operations'
import { ContextMenu, type ContextMenuState } from './ContextMenu'
import { ConfirmDialog } from './dialogs/ConfirmDialog'

const TOTAL_MAIN_GROUPS = MAX_MAIN_GROUP - MIN_MAIN_GROUP + 1

const useStyles = makeStyles({
  root: { display: 'flex', flexDirection: 'column', height: '100%', gap: '4px', minHeight: 0 },
  header: { display: 'flex', alignItems: 'center', justifyContent: 'space-between' },
  titleGroup: { display: 'flex', alignItems: 'baseline', gap: '6px' },
  list: { flex: 1, overflowY: 'auto', border: `1px solid ${tokens.colorNeutralStroke2}`, borderRadius: tokens.borderRadiusMedium },
})

interface MainGroupPanelProps {
  mainGroups: MainGroup[]
  selectedId: string | null
  onSelect: (id: string | null) => void
  onAdd: () => void
  onEdit: (mainGroup: MainGroup) => void
  onDelete: (mainGroup: MainGroup) => void
}

export const MainGroupPanel = memo(function MainGroupPanel({ mainGroups, selectedId, onSelect, onAdd, onEdit, onDelete }: MainGroupPanelProps) {
  const styles = useStyles()
  const sorted = [...mainGroups].sort((a, b) => a.subAddress - b.subAddress)
  const [contextMenu, setContextMenu] = useState<ContextMenuState | null>(null)
  const [pendingDelete, setPendingDelete] = useState<MainGroup | null>(null)

  const selected = mainGroups.find((m) => m.id === selectedId) ?? null

  function openContextMenu(e: React.MouseEvent, mg: MainGroup | null) {
    e.preventDefault()
    if (mg) onSelect(mg.id)
    setContextMenu({
      x: e.clientX,
      y: e.clientY,
      items: [
        { key: 'add', label: 'Neu', onClick: onAdd },
        { key: 'edit', label: 'Bearbeiten', onClick: () => mg && onEdit(mg), disabled: !mg },
        { key: 'delete', label: 'Löschen', onClick: () => mg && setPendingDelete(mg), disabled: !mg },
      ],
    })
  }

  return (
    <div className={styles.root}>
      <div className={styles.header}>
        <div className={styles.titleGroup}>
          <Text weight="semibold">Hauptgruppen</Text>
          <Text size={200}>
            {mainGroups.length}/{TOTAL_MAIN_GROUPS}
          </Text>
        </div>
        <div>
          <Button size="small" appearance="subtle" icon={<AddRegular />} title="Neu" onClick={onAdd} />
          <Button size="small" appearance="subtle" icon={<EditRegular />} title="Bearbeiten" disabled={!selected} onClick={() => selected && onEdit(selected)} />
          <Button size="small" appearance="subtle" icon={<DeleteRegular />} title="Löschen" disabled={!selected} onClick={() => selected && setPendingDelete(selected)} />
        </div>
      </div>
      <div
        className={styles.list}
        onContextMenu={(e) => openContextMenu(e, selected)}
        onKeyDown={(e) => {
          if (e.key === 'Delete' && selected) setPendingDelete(selected)
        }}
        tabIndex={0}
      >
        <Listbox
          selectedOptions={selectedId ? [selectedId] : []}
          onOptionSelect={(_, data) => onSelect(data.optionValue ?? null)}
        >
          {sorted.map((mg) => (
            <Option key={mg.id} value={mg.id} text={mainGroupListLabel(mg)} onContextMenu={(e) => openContextMenu(e, mg)}>
              {mainGroupListLabel(mg)}
            </Option>
          ))}
        </Listbox>
      </div>

      <ContextMenu state={contextMenu} onClose={() => setContextMenu(null)} />

      <ConfirmDialog
        open={pendingDelete !== null}
        title="Hauptgruppe löschen"
        message="Hauptgruppe löschen?"
        onConfirm={() => {
          if (pendingDelete) onDelete(pendingDelete)
          setPendingDelete(null)
        }}
        onCancel={() => setPendingDelete(null)}
      />
    </div>
  )
})
