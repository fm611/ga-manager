import type { ReactNode } from 'react'
import { Dialog, DialogSurface, DialogBody, DialogTitle, DialogContent, DialogActions } from '@fluentui/react-components'

export interface DialogShellProps {
  open: boolean
  title: string
  onCancel: () => void
  actions: ReactNode
  children: ReactNode
  surfaceStyle?: React.CSSProperties
  contentStyle?: React.CSSProperties
}

export function DialogShell({ open, title, onCancel, actions, children, surfaceStyle, contentStyle }: DialogShellProps) {
  return (
    <Dialog open={open} onOpenChange={(_, data) => !data.open && onCancel()}>
      <DialogSurface style={surfaceStyle}>
        <DialogBody>
          <DialogTitle>{title}</DialogTitle>
          <DialogContent style={contentStyle}>{children}</DialogContent>
          <DialogActions>{actions}</DialogActions>
        </DialogBody>
      </DialogSurface>
    </Dialog>
  )
}
