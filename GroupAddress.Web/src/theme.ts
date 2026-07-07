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

// Blue brand ramp: same hue/saturation-shift technique as knxGreen above, rotated to the
// app-icon blue (#4fa8dc) instead of green, keeping each step's lightness/saturation curve.
const knxBlue: BrandVariants = {
  10: '#091821',
  20: '#0d2533',
  30: '#113143',
  40: '#153d55',
  50: '#194c6a',
  60: '#1d5a7e',
  70: '#216993',
  80: '#2278aa',
  90: '#3c96ca',
  100: '#5ab0e2',
  110: '#72bbe5',
  120: '#85c4e9',
  130: '#a1d2ef',
  140: '#bcdef2',
  150: '#d4e9f5',
  160: '#edf5fa',
}

export const knxGreenTheme: Theme = createDarkTheme(knxGreen)
export const knxBlueTheme: Theme = createDarkTheme(knxBlue)

// Active theme for the whole app. Swap to knxGreenTheme to switch back.
export const knxDarkTheme: Theme = knxBlueTheme
