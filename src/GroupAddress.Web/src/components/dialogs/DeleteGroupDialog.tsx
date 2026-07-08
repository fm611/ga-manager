import {
  Dialog,
  DialogSurface,
  DialogBody,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
} from '@fluentui/react-components'
import type { GA } from '../../domain/schema'
import { gaAddressName } from '../../domain/operations'

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
    <Dialog open={open} onOpenChange={(_, data) => !data.open && onCancel()}>
      <DialogSurface>
        <DialogBody>
          <DialogTitle>Gruppe löschen</DialogTitle>
          <DialogContent>
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
          </DialogContent>
          <DialogActions>
            <Button appearance="secondary" onClick={onCancel}>
              Abbrechen
            </Button>
            <Button appearance="secondary" onClick={onDeleteGroupOnly}>
              Nur Gruppe löschen
            </Button>
            <Button appearance="primary" onClick={onDeleteWithGAs}>
              Gruppe + GA's löschen
            </Button>
          </DialogActions>
        </DialogBody>
      </DialogSurface>
    </Dialog>
  )
}
