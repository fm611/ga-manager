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
import { useI18n } from '../../i18n/I18nContext'
import logoDe from '../../assets/logo.svg'
import logoEn from '../../assets/logo-en.svg'
import { LanguageSelector } from './LanguageSelector'
import { useStyles } from './AppHeader.styles'

export interface AppHeaderProps {
  canUndo: boolean
  canRedo: boolean
  dirty: boolean
  onUndo: () => void
  onRedo: () => void
  onSave: () => void
  onSaveAs: () => void
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
  onSaveAs,
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
  const { t, locale } = useI18n()
  const logo = locale === 'de' ? logoDe : logoEn

  return (
    <div className={styles.header}>
      <div className={styles.menuBar}>
        <img src={logo} alt="GA Manager" className={styles.logo} />

        <Divider vertical style={{ height: '28px', flexGrow: 0 }} />

        <Menu>
          <MenuTrigger disableButtonEnhancement>
            <Button appearance="outline" icon={<ChevronDownRegular />} iconPosition="after">
              {t('header.file')}
            </Button>
          </MenuTrigger>
          <MenuPopover>
            <MenuList>
              <MenuItem onClick={onNew}>{t('header.new')}</MenuItem>
              <Menu>
                <MenuTrigger disableButtonEnhancement>
                  <MenuItem>{t('header.open')}</MenuItem>
                </MenuTrigger>
                <MenuPopover>
                  <MenuList>
                    <MenuItem onClick={onOpenFile}>{t('header.openFile')}</MenuItem>
                    <MenuDivider />
                    <MenuItem onClick={onOpenSample}>{t('header.sample')}</MenuItem>
                    {recentFiles.length > 0 && <MenuDivider />}
                    {recentFiles.map((file) => (
                      <MenuItem key={file.path} onClick={() => onOpenRecent(file.path)} title={file.path}>
                        {file.name}
                      </MenuItem>
                    ))}
                  </MenuList>
                </MenuPopover>
              </Menu>
              <MenuItem onClick={onSave}>{t('header.save')}</MenuItem>
              <MenuItem onClick={onSaveAs}>{t('header.saveAs')}</MenuItem>
              <MenuDivider />
              <MenuItem onClick={onExport}>{t('header.export')}</MenuItem>
              <MenuItem onClick={onImport}>{t('header.import')}</MenuItem>
            </MenuList>
          </MenuPopover>
        </Menu>

        <Button appearance="outline" onClick={onOpenTemplateManager}>
          {t('header.templateManager')}
        </Button>

        <Button size="small" appearance="subtle" icon={<ArrowUndoRegular />} title={t('header.undo')} disabled={!canUndo} onClick={onUndo} />
        <Button size="small" appearance="subtle" icon={<ArrowRedoRegular />} title={t('header.redo')} disabled={!canRedo} onClick={onRedo} />
        <Button
          size="small"
          appearance="subtle"
          icon={<SaveRegular />}
          title={t('header.saveTitle')}
          style={dirty ? undefined : { opacity: 0.5 }}
          onClick={onSave}
        />

        <div className={styles.languageSelector}>
          <LanguageSelector />
        </div>
      </div>
    </div>
  )
}
