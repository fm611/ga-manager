import { makeStyles, tokens } from '@fluentui/react-components'

export const useStyles = makeStyles({
  rightColumn: { flex: 1, minWidth: 0, display: 'flex', flexDirection: 'column', gap: '8px' },
  gridToolbar: { display: 'flex', alignItems: 'center', gap: '8px', justifyContent: 'space-between' },
  gridWrapper: { flex: 1, minHeight: 0, borderRadius: tokens.borderRadiusMedium },
  gridWrapperFiltered: { boxShadow: `0 0 0 2px ${tokens.colorPaletteRedBorder2}` },
})
