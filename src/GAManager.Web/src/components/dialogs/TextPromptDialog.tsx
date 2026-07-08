import { useEffect, useState } from 'react'
import { Button, Field, Input } from '@fluentui/react-components'
import { useI18n } from '../../i18n/I18nContext'
import { DialogShell } from './DialogShell'

export interface TextPromptDialogProps {
  open: boolean
  title: string
  label?: string
  initialValue: string
  onSubmit: (value: string) => void
  onCancel: () => void
}

export function TextPromptDialog({ open, title, label, initialValue, onSubmit, onCancel }: TextPromptDialogProps) {
  const { t } = useI18n()
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
    <DialogShell
      open={open}
      title={title}
      onCancel={onCancel}
      actions={
        <>
          <Button appearance="secondary" onClick={onCancel}>
            {t('common.cancel')}
          </Button>
          <Button appearance="primary" disabled={!canSave} onClick={submit}>
            {t('common.save')}
          </Button>
        </>
      }
    >
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
    </DialogShell>
  )
}
