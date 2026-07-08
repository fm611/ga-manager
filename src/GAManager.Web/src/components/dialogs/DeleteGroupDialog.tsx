import { Button } from '@fluentui/react-components'
import type { GA } from '../../domain/schema'
import { gaAddressName } from '../../domain/operations'
import { useI18n } from '../../i18n/I18nContext'
import { DialogShell } from './DialogShell'

export interface DeleteGroupDialogProps {
  open: boolean
  gas: GA[]
  onDeleteWithGAs: () => void
  onDeleteGroupOnly: () => void
  onCancel: () => void
}

export function DeleteGroupDialog({ open, gas, onDeleteWithGAs, onDeleteGroupOnly, onCancel }: DeleteGroupDialogProps) {
  const { t } = useI18n()
  const sorted = [...gas].sort((a, b) => {
    if (a.address.middleGroup !== b.address.middleGroup) return a.address.middleGroup - b.address.middleGroup
    return a.address.ga - b.address.ga
  })

  return (
    <DialogShell
      open={open}
      title={t('deleteGroupDialog.title')}
      onCancel={onCancel}
      actions={
        <>
          <Button appearance="secondary" onClick={onCancel}>
            {t('common.cancel')}
          </Button>
          <Button appearance="secondary" onClick={onDeleteGroupOnly}>
            {t('deleteGroupDialog.deleteGroupOnly')}
          </Button>
          <Button appearance="primary" onClick={onDeleteWithGAs}>
            {t('deleteGroupDialog.deleteWithGAs')}
          </Button>
        </>
      }
    >
      <p>{t('deleteGroupDialog.intro')}</p>
      <div
        style={{
          maxHeight: 200,
          overflowY: 'auto',
          border: '1px solid var(--colorNeutralStroke2)',
          borderRadius: 4,
          padding: 4,
        }}
      >
        {sorted.map((g) => (
          <div key={g.id}>{gaAddressName(g)}</div>
        ))}
      </div>
    </DialogShell>
  )
}
