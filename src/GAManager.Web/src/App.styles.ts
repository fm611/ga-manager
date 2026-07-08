import { makeStyles } from '@fluentui/react-components'

export const useStyles = makeStyles({
  root: { display: 'flex', flexDirection: 'column', height: '100vh', width: '100vw', overflow: 'hidden' },
  body: { display: 'flex', flex: 1, minHeight: 0, gap: '10px', padding: '10px' },
})
