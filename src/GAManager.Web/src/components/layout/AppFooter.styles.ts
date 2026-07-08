import { makeStyles, tokens } from '@fluentui/react-components'

export const useStyles = makeStyles({
  footer: {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    gap: '6px',
    padding: '4px 10px',
    borderTop: `1px solid ${tokens.colorNeutralStroke2}`,
    flexShrink: 0,
    color: tokens.colorNeutralForeground3,
  },
})
