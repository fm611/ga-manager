import { Button } from '@fluentui/react-components'
import type { GA } from '../../domain/schema'
import { gaAddressName } from '../../domain/operations'
import { DialogShell } from './DialogShell'

export interface DeleteGroupDialogProps {
  open: boolean
  gas: GA[]
  onDeleteWithGAs: () => void
  onDeleteGroupOnly: () => void
  onCancel: () => void
}

export function DeleteGroupDialog({ open, gas, onDeleteWithGAs, onDeleteGroupOnly, onCancel }: DeleteGroupDialogProps) {
  const sorted = [...gas].sort((a, b) => {
    if (a.address.middleGroup !== b.address.middleGroup) return a.address.middleGroup - b.address.middleGroup
    return a.address.ga - b.address.ga
  })

  return (
    <DialogShell
      open={open}
      title="Gruppe löschen"
      onCancel={onCancel}
      actions={
        <>
          <Button appearance="secondary" onClick={onCancel}>
            Abbrechen
          </Button>
          <Button appearance="secondary" onClick={onDeleteGroupOnly}>
            Nur Gruppe löschen
          </Button>
          <Button appearance="primary" onClick={onDeleteWithGAs}>
            Gruppe + GA's löschen
          </Button>
        </>
      }
    >
      <p>Die Gruppe enthält folgende Gruppenadressen:</p>
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
