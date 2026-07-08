import { MenuList, MenuItem, MenuDivider } from '@fluentui/react-components'
import { useStyles } from './ContextMenu.styles'

export interface ContextMenuItemDef {
  key: string
  label: string
  onClick: () => void
  disabled?: boolean
  separatorBefore?: boolean
}

export interface ContextMenuState {
  x: number
  y: number
  items: ContextMenuItemDef[]
}

export function ContextMenu({ state, onClose }: { state: ContextMenuState | null; onClose: () => void }) {
  const styles = useStyles()
  if (!state) return null

  return (
    <>
      <div className={styles.overlay} onClick={onClose} onContextMenu={(e) => e.preventDefault()} />
      <div className={styles.menu} style={{ left: state.x, top: state.y }}>
        <MenuList>
          {state.items.map((item) => (
            <span key={item.key}>
              {item.separatorBefore && <MenuDivider />}
              <MenuItem
                disabled={item.disabled}
                onClick={() => {
                  item.onClick()
                  onClose()
                }}
              >
                {item.label}
              </MenuItem>
            </span>
          ))}
        </MenuList>
      </div>
    </>
  )
}
