import { makeStyles, tokens } from '@fluentui/react-components'

export const useStyles = makeStyles({
  header: {
    display: 'flex',
    flexDirection: 'column',
    borderBottom: `1px solid ${tokens.colorNeutralStroke2}`,
    flexShrink: 0,
  },
  menuBar: {
    display: 'flex',
    alignItems: 'center',
    gap: '12px',
    padding: '6px 10px',
  },
  logo: { height: '48px', width: 'auto', padding: '2px' },
})
