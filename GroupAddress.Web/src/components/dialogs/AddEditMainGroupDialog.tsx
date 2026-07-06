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
import { MainGroupFormSchema } from '../../domain/schema'
import type { MainGroup } from '../../domain/schema'
import { isValidMainGroupSubAddress, nextFreeMainGroupAddress } from '../../domain/operations'

export interface AddEditMainGroupDialogProps {
  open: boolean
  mainGroups: MainGroup[]
  editing: MainGroup | null
  onSave: (values: { subAddress: number; name: string; defaultBlockLength: number }) => void
  onCancel: () => void
}

export function AddEditMainGroupDialog({ open, mainGroups, editing, onSave, onCancel }: AddEditMainGroupDialogProps) {
  const [subAddress, setSubAddress] = useState('0')
  const [name, setName] = useState('')
  const [defaultBlockLength, setDefaultBlockLength] = useState('1')
  const [errors, setErrors] = useState<{ subAddress?: string; name?: string; defaultBlockLength?: string }>({})

  useEffect(() => {
    if (!open) return
    if (editing) {
      setSubAddress(String(editing.subAddress))
      setName(editing.name)
      setDefaultBlockLength(String(editing.defaultBlockLength))
    } else {
      setSubAddress(String(nextFreeMainGroupAddress(mainGroups)))
      setName('Neue Hauptgruppe')
      setDefaultBlockLength('1')
    }
    setErrors({})
  }, [open, editing, mainGroups])

  function validate(nextSubAddress: string, nextName: string, nextBlockLength: string) {
    const parsedSubAddress = Number(nextSubAddress)
    const parsedBlockLength = Number(nextBlockLength)

    const next: typeof errors = {}

    const result = MainGroupFormSchema.safeParse({
      subAddress: parsedSubAddress,
      name: nextName,
      defaultBlockLength: parsedBlockLength,
    })
    if (!result.success) {
      for (const issue of result.error.issues) {
        const key = issue.path[0] as keyof typeof errors
        if (!next[key]) next[key] = issue.message
      }
    }

    if (!next.subAddress && isValidMainGroupSubAddress(parsedSubAddress)) {
      const conflict = mainGroups.some((m) => m.subAddress === parsedSubAddress && m.id !== editing?.id)
      if (conflict) next.subAddress = 'Adresse bereits belegt.'
    }

    setErrors(next)
    return Object.keys(next).length === 0 ? { subAddress: parsedSubAddress, name: nextName, defaultBlockLength: parsedBlockLength } : null
  }

  const handleSave = () => {
    const values = validate(subAddress, name, defaultBlockLength)
    if (values) onSave(values)
  }

  return (
    <Dialog open={open} onOpenChange={(_, data) => !data.open && onCancel()}>
      <DialogSurface>
        <DialogBody>
          <DialogTitle>{editing ? 'Hauptgruppe bearbeiten' : 'Hauptgruppe hinzufügen'}</DialogTitle>
          <DialogContent style={{ display: 'flex', flexDirection: 'column', gap: '8px' }}>
            <Field label="Adresse" validationState={errors.subAddress ? 'error' : 'none'} validationMessage={errors.subAddress}>
              <Input
                type="number"
                min={0}
                max={31}
                value={subAddress}
                onChange={(_, data) => {
                  setSubAddress(data.value)
                  validate(data.value, name, defaultBlockLength)
                }}
              />
            </Field>
            <Field label="Name" validationState={errors.name ? 'error' : 'none'} validationMessage={errors.name}>
              <Input
                value={name}
                onChange={(_, data) => {
                  setName(data.value)
                  validate(subAddress, data.value, defaultBlockLength)
                }}
              />
            </Field>
            <Field
              label="Block Länge"
              validationState={errors.defaultBlockLength ? 'error' : 'none'}
              validationMessage={errors.defaultBlockLength}
            >
              <Input
                type="number"
                min={1}
                value={defaultBlockLength}
                onChange={(_, data) => {
                  setDefaultBlockLength(data.value)
                  validate(subAddress, name, data.value)
                }}
              />
            </Field>
          </DialogContent>
          <DialogActions>
            <Button appearance="secondary" onClick={onCancel}>
              Abbrechen
            </Button>
            <Button appearance="primary" disabled={Object.keys(errors).length > 0} onClick={handleSave}>
              Speichern
            </Button>
          </DialogActions>
        </DialogBody>
      </DialogSurface>
    </Dialog>
  )
}
