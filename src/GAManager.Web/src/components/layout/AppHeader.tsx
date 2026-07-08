import {
  Button,
  Divider,
  Menu,
  MenuTrigger,
  MenuPopover,
  MenuList,
  MenuItem,
  MenuDivider,
} from '@fluentui/react-components'
import { ArrowUndoRegular, ArrowRedoRegular, SaveRegular, ChevronDownRegular } from '@fluentui/react-icons'
import type { RecentFile } from '../../host/wpfBridge'
import logo from '../../assets/logo.svg'
import { useStyles } from './AppHeader.styles'

export interface AppHeaderProps {
  canUndo: boolean
  canRedo: boolean
  dirty: boolean
  onUndo: () => void
  onRedo: () => void
  onSave: () => void
  onNew: () => void
  onOpenFile: () => void
  onOpenSample: () => void
  recentFiles: RecentFile[]
  onOpenRecent: (path: string) => void
  onExport: () => void
  onImport: () => void
  onOpenTemplateManager: () => void
}

export function AppHeader({
  canUndo,
  canRedo,
  dirty,
  onUndo,
  onRedo,
  onSave,
  onNew,
  onOpenFile,
  onOpenSample,
  recentFiles,
  onOpenRecent,
  onExport,
  onImport,
  onOpenTemplateManager,
}: AppHeaderProps) {
  const styles = useStyles()

  return (
    <div className={styles.header}>
      <div className={styles.menuBar}>
        <img src={logo} alt="Gruppenadressen Manager" className={styles.logo} />

        <Divider vertical style={{ height: '28px', flexGrow: 0 }} />

        <Menu>
          <MenuTrigger disableButtonEnhancement>
            <Button appearance="outline" icon={<ChevronDownRegular />} iconPosition="after">
              Datei
            </Button>
          </MenuTrigger>
          <MenuPopover>
            <MenuList>
              <MenuItem onClick={onNew}>Neu</MenuItem>
              <Menu>
                <MenuTrigger disableButtonEnhancement>
                  <MenuItem>Öffnen</MenuItem>
                </MenuTrigger>
                <MenuPopover>
                  <MenuList>
                    <MenuItem onClick={onOpenFile}>Datei…</MenuItem>
                    <MenuDivider />
                    <MenuItem onClick={onOpenSample}>Beispiel</MenuItem>
                    {recentFiles.length > 0 && <MenuDivider />}
                    {recentFiles.map((file) => (
                      <MenuItem key={file.path} onClick={() => onOpenRecent(file.path)} title={file.path}>
                        {file.name}
                      </MenuItem>
                    ))}
                  </MenuList>
                </MenuPopover>
              </Menu>
              <MenuItem onClick={onSave}>Speichern</MenuItem>
              <MenuDivider />
              <MenuItem onClick={onExport}>Export</MenuItem>
              <MenuItem onClick={onImport}>Import</MenuItem>
            </MenuList>
          </MenuPopover>
        </Menu>

        <Button appearance="outline" onClick={onOpenTemplateManager}>
          Template Manager
        </Button>

        <Button size="small" appearance="subtle" icon={<ArrowUndoRegular />} title="Rückgängig (Strg+Z)" disabled={!canUndo} onClick={onUndo} />
        <Button size="small" appearance="subtle" icon={<ArrowRedoRegular />} title="Wiederholen (Strg+Y)" disabled={!canRedo} onClick={onRedo} />
        <Button
          size="small"
          appearance="subtle"
          icon={<SaveRegular />}
          title="Speichern (Strg+S)"
          style={dirty ? undefined : { opacity: 0.5 }}
          onClick={onSave}
        />
      </div>
    </div>
  )
}
