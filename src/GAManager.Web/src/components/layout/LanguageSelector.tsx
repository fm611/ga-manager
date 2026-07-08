import { Button, Menu, MenuTrigger, MenuPopover, MenuList, MenuItem } from '@fluentui/react-components'
import { GlobeRegular } from '@fluentui/react-icons'
import { useI18n, LOCALES, type Locale } from '../../i18n/I18nContext'
import { FlagIcon } from './FlagIcon'

const LOCALE_LABELS: Record<Locale, string> = { de: 'Deutsch', en: 'English' }

export function LanguageSelector() {
  const { locale, setLocale, t } = useI18n()

  return (
    <Menu>
      <MenuTrigger disableButtonEnhancement>
        <Button appearance="subtle" size="small" icon={<GlobeRegular />} title={t('header.language')} aria-label={t('header.language')} />
      </MenuTrigger>
      <MenuPopover>
        <MenuList>
          {LOCALES.map((l) => (
            <MenuItem
              key={l}
              icon={<FlagIcon locale={l} />}
              onClick={() => setLocale(l)}
              style={l === locale ? { fontWeight: 600 } : undefined}
            >
              {LOCALE_LABELS[l]}
            </MenuItem>
          ))}
        </MenuList>
      </MenuPopover>
    </Menu>
  )
}
