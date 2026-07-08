import { Button } from '@fluentui/react-components'
import { useI18n } from '../../i18n/I18nContext'
import { DialogShell } from './DialogShell'

export interface ConfirmDialogProps {
  open: boolean
  title: string
  message: string
  confirmText?: string
  cancelText?: string
  danger?: boolean
  onConfirm: () => void
  onCancel: () => void
}

export function ConfirmDialog({ open, title, message, confirmText, cancelText, danger, onConfirm, onCancel }: ConfirmDialogProps) {
  const { t } = useI18n()

  return (
    <DialogShell
      open={open}
      title={title}
      onCancel={onCancel}
      actions={
        <>
          <Button appearance="secondary" onClick={onCancel}>
            {cancelText ?? t('common.no')}
          </Button>
          <Button appearance="primary" onClick={onConfirm} style={danger ? { backgroundColor: '#c4314b' } : undefined}>
            {confirmText ?? t('common.yes')}
          </Button>
        </>
      }
    >
      {message}
    </DialogShell>
  )
}
