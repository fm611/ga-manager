import { makeStyles, tokens, MenuList, MenuItem, MenuDivider } from '@fluentui/react-components'

const useStyles = makeStyles({
  overlay: {
    position: 'fixed',
    inset: 0,
    zIndex: 1000,
  },
  menu: {
    position: 'fixed',
    zIndex: 1001,
    backgroundColor: tokens.colorNeutralBackground1,
    border: `1px solid ${tokens.colorNeutralStroke1}`,
    borderRadius: tokens.borderRadiusMedium,
    boxShadow: tokens.shadow16,
    minWidth: '180px',
  },
})

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
