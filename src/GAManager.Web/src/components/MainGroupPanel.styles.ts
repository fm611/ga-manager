import { makeStyles, tokens } from '@fluentui/react-components'

export const useStyles = makeStyles({
  root: { display: 'flex', flexDirection: 'column', height: '100%', gap: '4px', minHeight: 0 },
  header: { display: 'flex', alignItems: 'center', justifyContent: 'space-between' },
  titleGroup: { display: 'flex', alignItems: 'baseline', gap: '6px' },
  list: { flex: 1, overflowY: 'auto', border: `1px solid ${tokens.colorNeutralStroke2}`, borderRadius: tokens.borderRadiusMedium },
})
