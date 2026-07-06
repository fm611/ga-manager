import { createDarkTheme } from '@fluentui/react-components'
import type { BrandVariants, Theme } from '@fluentui/react-components'

// KNX-green-inspired brand ramp (hue/saturation shifted from Fluent's default blue ramp,
// keeping its lightness curve so contrast on every semantic slot stays balanced).
const knxGreen: BrandVariants = {
  10: '#132109',
  20: '#1c330d',
  30: '#254311',
  40: '#2f5515',
  50: '#396a19',
  60: '#447e1d',
  70: '#4f9321',
  80: '#58aa22',
  90: '#75ca3c',
  100: '#90e25a',
  110: '#a0e572',
  120: '#ade985',
  130: '#c0efa1',
  140: '#d2f2bc',
  150: '#e1f5d4',
  160: '#f2faed',
}

export const knxDarkTheme: Theme = createDarkTheme(knxGreen)
