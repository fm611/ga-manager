import { Text, Link } from '@fluentui/react-components'
import { InfoRegular } from '@fluentui/react-icons'
import { useI18n } from '../../i18n/I18nContext'
import { useStyles } from './AppFooter.styles'

const GITHUB_REPO_URL = 'https://github.com/fm611/ga-manager'

export function AppFooter() {
  const styles = useStyles()
  const { t } = useI18n()

  return (
    <div className={styles.footer}>
      <Text size={200}>
        {t('footer.version')} {__APP_VERSION__}
      </Text>
      <Link href={GITHUB_REPO_URL} target="_blank" rel="noreferrer" title={t('footer.githubRepo')} style={{ display: 'flex', alignItems: 'center' }}>
        <InfoRegular fontSize={16} />
      </Link>
    </div>
  )
}
