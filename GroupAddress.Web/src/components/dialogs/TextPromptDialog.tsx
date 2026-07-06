import { useEffect, useState } from 'react'
import {
  Dialog,
  DialogSurface,
  DialogBody,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Field,
  Input,
} from '@fluentui/react-components'

export interface TextPromptDialogProps {
  open: boolean
  title: string
  label?: string
  initialValue: string
  onSubmit: (value: string) => void
  onCancel: () => void
}

export function TextPromptDialog({ open, title, label, initialValue, onSubmit, onCancel }: TextPromptDialogProps) {
  const [value, setValue] = useState(initialValue)

  useEffect(() => {
    if (open) setValue(initialValue)
  }, [open, initialValue])

  const trimmed = value.trim()
  const canSave = trimmed.length > 0

  const submit = () => {
    if (!canSave) return
    onSubmit(trimmed)
  }

  return (
    <Dialog open={open} onOpenChange={(_, data) => !data.open && onCancel()}>
      <DialogSurface>
        <DialogBody>
          <DialogTitle>{title}</DialogTitle>
          <DialogContent>
            <Field label={label}>
              <Input
                autoFocus
                value={value}
                onChange={(_, data) => setValue(data.value)}
                onKeyDown={(e) => {
                  if (e.key === 'Enter') submit()
                }}
                onFocus={(e) => e.currentTarget.select()}
              />
            </Field>
          </DialogContent>
          <DialogActions>
            <Button appearance="secondary" onClick={onCancel}>
              Abbrechen
            </Button>
            <Button appearance="primary" disabled={!canSave} onClick={submit}>
              Speichern
            </Button>
          </DialogActions>
        </DialogBody>
      </DialogSurface>
    </Dialog>
  )
}
