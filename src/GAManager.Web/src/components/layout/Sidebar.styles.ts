import { makeStyles } from '@fluentui/react-components'

export const useStyles = makeStyles({
  leftColumn: { width: '300px', flexShrink: 0, display: 'flex', flexDirection: 'column', gap: '10px', minHeight: 0 },
  leftTop: { flex: '1 1 55%', minHeight: 0 },
  leftBottom: { flex: '1 1 45%', minHeight: 0 },
})
