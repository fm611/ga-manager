import { Button } from '@fluentui/react-components'
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

export function ConfirmDialog({
  open,
  title,
  message,
  confirmText = 'Ja',
  cancelText = 'Nein',
  danger,
  onConfirm,
  onCancel,
}: ConfirmDialogProps) {
  return (
    <DialogShell
      open={open}
      title={title}
      onCancel={onCancel}
      actions={
        <>
          <Button appearance="secondary" onClick={onCancel}>
            {cancelText}
          </Button>
          <Button appearance="primary" onClick={onConfirm} style={danger ? { backgroundColor: '#c4314b' } : undefined}>
            {confirmText}
          </Button>
        </>
      }
    >
      {message}
    </DialogShell>
  )
}
