import { makeStyles, tokens } from '@fluentui/react-components'

export const useStyles = makeStyles({
  container: {
    height: '100%',
    overflow: 'auto',
    border: `1px solid ${tokens.colorNeutralStroke2}`,
    outline: 'none',
  },
  table: {
    borderCollapse: 'collapse',
    fontSize: tokens.fontSizeBase200,
    userSelect: 'none',
  },
  th: {
    position: 'sticky',
    top: 0,
    backgroundColor: tokens.colorNeutralBackground3,
    border: `1px solid ${tokens.colorNeutralStroke2}`,
    padding: '4px 6px',
    whiteSpace: 'nowrap',
    overflow: 'hidden',
    textOverflow: 'ellipsis',
    cursor: 'default',
    zIndex: 1,
  },
  colResizer: {
    position: 'absolute',
    top: 0,
    right: 0,
    width: '6px',
    height: '100%',
    cursor: 'col-resize',
    zIndex: 2,
    ':hover': {
      backgroundColor: tokens.colorNeutralStroke1,
    },
  },
  rowHeader: {
    position: 'sticky',
    left: 0,
    backgroundColor: tokens.colorNeutralBackground3,
    border: `1px solid ${tokens.colorNeutralStroke2}`,
    padding: '2px 6px',
    textAlign: 'center',
    fontVariantNumeric: 'tabular-nums',
    cursor: 'pointer',
    userSelect: 'none',
  },
  cell: {
    border: `1px solid ${tokens.colorNeutralStroke2}`,
    padding: '2px 6px',
    overflow: 'hidden',
    textOverflow: 'ellipsis',
    whiteSpace: 'nowrap',
    cursor: 'default',
  },
  cellSelected: {
    backgroundColor: tokens.colorBrandBackground2,
  },
  cellHighlighted: {
    backgroundColor: tokens.colorPaletteYellowBackground2,
    color: tokens.colorPaletteYellowForeground2,
  },
  cellSearchMatch: {
    backgroundColor: tokens.colorPaletteGreenBackground2,
    color: tokens.colorPaletteGreenForeground2,
  },
  cellInput: {
    width: '100%',
    boxSizing: 'border-box',
    font: 'inherit',
    userSelect: 'text',
  },
  contextOverlay: {
    position: 'fixed',
    inset: 0,
    zIndex: 1000,
  },
  contextMenu: {
    position: 'fixed',
    zIndex: 1001,
    backgroundColor: tokens.colorNeutralBackground1,
    border: `1px solid ${tokens.colorNeutralStroke1}`,
    borderRadius: tokens.borderRadiusMedium,
    boxShadow: tokens.shadow16,
    minWidth: '200px',
  },
})
