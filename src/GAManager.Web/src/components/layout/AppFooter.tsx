import { Text, Link } from '@fluentui/react-components'
import { InfoRegular } from '@fluentui/react-icons'
import { useStyles } from './AppFooter.styles'

const GITHUB_REPO_URL = 'https://github.com/fm611/ga-manager'

export function AppFooter() {
  const styles = useStyles()

  return (
    <div className={styles.footer}>
      <Text size={200}>Version {__APP_VERSION__}</Text>
      <Link href={GITHUB_REPO_URL} target="_blank" rel="noreferrer" title="GitHub Repository" style={{ display: 'flex', alignItems: 'center' }}>
        <InfoRegular fontSize={16} />
      </Link>
    </div>
  )
}
