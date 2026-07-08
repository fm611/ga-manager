import { memo, useState } from 'react'
import { Button, Listbox, Option, Text } from '@fluentui/react-components'
import { AddRegular, EditRegular, DeleteRegular } from '@fluentui/react-icons'
import type { MainGroup } from '../domain/schema'
import { MIN_MAIN_GROUP, MAX_MAIN_GROUP } from '../domain/schema'
import { mainGroupListLabel } from '../domain/operations'
import { useI18n } from '../i18n/I18nContext'
import { ContextMenu, type ContextMenuState } from './ContextMenu'
import { ConfirmDialog } from './dialogs/ConfirmDialog'
import { useStyles } from './MainGroupPanel.styles'

const TOTAL_MAIN_GROUPS = MAX_MAIN_GROUP - MIN_MAIN_GROUP + 1

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
  const { t } = useI18n()
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
        { key: 'add', label: t('mainGroupPanel.add'), onClick: onAdd },
        { key: 'edit', label: t('mainGroupPanel.edit'), onClick: () => mg && onEdit(mg), disabled: !mg },
        { key: 'delete', label: t('mainGroupPanel.delete'), onClick: () => mg && setPendingDelete(mg), disabled: !mg },
      ],
    })
  }

  return (
    <div className={styles.root}>
      <div className={styles.header}>
        <div className={styles.titleGroup}>
          <Text weight="semibold">{t('mainGroupPanel.title')}</Text>
          <Text size={200}>
            {mainGroups.length}/{TOTAL_MAIN_GROUPS}
          </Text>
        </div>
        <div>
          <Button size="small" appearance="subtle" icon={<AddRegular />} title={t('mainGroupPanel.add')} onClick={onAdd} />
          <Button
            size="small"
            appearance="subtle"
            icon={<EditRegular />}
            title={t('mainGroupPanel.edit')}
            disabled={!selected}
            onClick={() => selected && onEdit(selected)}
          />
          <Button
            size="small"
            appearance="subtle"
            icon={<DeleteRegular />}
            title={t('mainGroupPanel.delete')}
            disabled={!selected}
            onClick={() => selected && setPendingDelete(selected)}
          />
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
        title={t('mainGroupPanel.confirmDeleteTitle')}
        message={t('mainGroupPanel.confirmDeleteMessage')}
        onConfirm={() => {
          if (pendingDelete) onDelete(pendingDelete)
          setPendingDelete(null)
        }}
        onCancel={() => setPendingDelete(null)}
      />
    </div>
  )
})
