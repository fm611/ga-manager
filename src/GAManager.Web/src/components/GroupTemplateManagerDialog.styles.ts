import { makeStyles } from '@fluentui/react-components'

export const useStyles = makeStyles({
  body: { display: 'flex', gap: '16px', height: '65vh', minHeight: '480px' },
  leftPane: { width: '300px', flexShrink: 0, display: 'flex', flexDirection: 'column', gap: '10px', overflowY: 'auto' },
  rightPane: { flex: 1, minWidth: 0, display: 'flex', flexDirection: 'column' },
  buttonRow: { display: 'flex', gap: '6px' },
  positionGroup: { display: 'flex', flexDirection: 'column', gap: '10px', width: '100%' },
  positionRow: { display: 'flex', alignItems: 'center', justifyContent: 'space-between', gap: '8px', width: '100%' },
})
