import type { GA } from './schema'

export const DEFAULT_SUB_GROUP_NAMES = [
  'SET Central',
  'GET Central',
  'SET Switch',
  'GET Switch',
  'SET Value',
  'GET Value',
  'SET Misc',
  'GET Misc',
]

let templateGaCounter = 0

// Template GAs have no main group yet (mainGroup: -1); ids are stable across
// app reloads since these are fixed built-in templates, not user data.
function tga(middleGroup: number, ga: number, name: string): GA {
  templateGaCounter += 1
  return {
    id: `template-ga-${templateGaCounter}`,
    name,
    groupId: null,
    address: { mainGroup: -1, middleGroup, ga },
  }
}

export interface DefaultTemplateDef {
  name: string
  gas: GA[]
}

export const DEFAULT_GROUP_TEMPLATES: DefaultTemplateDef[] = [
  {
    name: 'Light General',
    gas: [
      tga(2, 0, 'Switch'), tga(3, 0, 'Switch_Status'),

      tga(6, 0, 'Lock1'), tga(7, 0, 'Lock_Status'),
      tga(6, 1, 'Lock2'),
      tga(6, 2, 'Scene'),
    ],
  },
  {
    name: 'Light Dimmable',
    gas: [
      tga(2, 0, 'Switch'), tga(3, 0, 'Switch_Status'),
      tga(2, 2, 'Sequence1_Start'), tga(3, 2, 'Sequence1_Status'),
      tga(2, 3, 'Sequence2_Start'), tga(3, 3, 'Sequence2_Status'),

      tga(4, 0, 'Dim_Bright_relative'),
      tga(4, 1, 'Dim_Bright_absolute'), tga(5, 1, 'Dim_Bright_Value'),

      tga(6, 0, 'Lock1'), tga(7, 0, 'Lock_Status'),
      tga(6, 1, 'Lock2'),
      tga(6, 2, 'Scene'),
      tga(6, 3, 'BitScene1'),
      tga(6, 4, 'BitScene2'),
      tga(6, 5, 'BitScene3'),
      tga(6, 6, 'BitScene4'),
    ],
  },
  {
    name: 'Light TW',
    gas: [
      tga(2, 0, 'Switch'), tga(3, 0, 'Switch_Status'),
      tga(2, 1, 'HCL_Start'), tga(3, 1, 'HCL_Status'),
      tga(2, 2, 'Sequence1_Start'), tga(3, 2, 'Sequence1_Status'),
      tga(2, 3, 'Sequence2_Start'), tga(3, 3, 'Sequence2_Status'),

      tga(4, 0, 'Dim_Bright_relative'),
      tga(4, 1, 'Dim_Bright_absolute'), tga(5, 1, 'Dim_Bright_Value'),
      tga(4, 2, 'Dim_ColorTempPct_relative'),
      tga(4, 3, 'Dim_ColorTempPct_absolute'), tga(5, 3, 'Dim_ColorTempPct_Value'),
      tga(4, 4, 'Dim_ColorTempK_relative'),
      tga(4, 5, 'Dim_ColorTempK_absolute'), tga(5, 5, 'Dim_ColorTempK_Value'),

      tga(6, 0, 'Lock1'), tga(7, 0, 'Lock_Status'),
      tga(6, 1, 'Lock2'),
      tga(6, 2, 'Scene'),
      tga(6, 3, 'BitScene1'),
      tga(6, 4, 'BitScene2'),
      tga(6, 5, 'BitScene3'),
      tga(6, 6, 'BitScene4'),
    ],
  },
  {
    name: 'Light RGBW',
    gas: [
      tga(2, 0, 'Switch'), tga(3, 0, 'Switch_Status'),
      tga(2, 1, 'HCL_Start'), tga(3, 1, 'HCL_Status'),
      tga(2, 2, 'Sequence1_Start'), tga(3, 2, 'Sequence1_Status'),
      tga(2, 3, 'Sequence2_Start'), tga(3, 3, 'Sequence2_Status'),
      tga(2, 4, 'Sequence3_Start'), tga(3, 4, 'Sequence3_Status'),
      tga(2, 5, 'Sequence4_Start'), tga(3, 5, 'Sequence4_Status'),
      tga(2, 6, 'Sequence5_Start'), tga(3, 6, 'Sequence5_Status'),
      tga(2, 7, 'Sequence6_Start'), tga(3, 7, 'Sequence6_Status'),

      tga(2, 8, 'R_Switch'), tga(3, 8, 'R_Switch_Status'),
      tga(2, 9, 'G_Switch'), tga(3, 9, 'G_Switch_Status'),
      tga(2, 10, 'B_Switch'), tga(3, 10, 'B_Switch_Status'),
      tga(2, 11, 'W_Switch'), tga(3, 11, 'W_Switch_Status'),

      tga(4, 0, 'Dim_Hue_relative'),
      tga(4, 1, 'Dim_Hue_absolute'), tga(5, 1, 'Dim_Hue_Value'),
      tga(4, 2, 'Dim_Saturation_relative'),
      tga(4, 3, 'Dim_Saturation_absolute'), tga(5, 3, 'Dim_Saturation_Value'),
      tga(4, 4, 'Dim_Brightness_relative'),
      tga(4, 5, 'Dim_Brightness_absolute'), tga(5, 5, 'Dim_Brightness_Value'),

      tga(4, 6, 'Dim_Red_relative'),
      tga(4, 7, 'Dim_Red_absolute'), tga(5, 7, 'Dim_Red_Value'),
      tga(4, 8, 'Dim_Green_relative'),
      tga(4, 9, 'Dim_Green_absolute'), tga(5, 9, 'Dim_Green_Value'),
      tga(4, 10, 'Dim_Blue_relative'),
      tga(4, 11, 'Dim_Blue_absolute'), tga(5, 11, 'Dim_Blue_Value'),
      tga(4, 12, 'Dim_White_relative'),
      tga(4, 13, 'Dim_White_absolute'), tga(5, 13, 'Dim_White_Value'),

      tga(4, 14, 'Dim_ColorTempPct_relative'),
      tga(4, 15, 'Dim_ColorTempPct_absolute'), tga(5, 15, 'Dim_ColorTempPct_Value'),
      tga(4, 16, 'Dim_ColorTempK_relative'),
      tga(4, 17, 'Dim_ColorTempK_absolute'), tga(5, 17, 'Dim_ColorTempK_Value'),

      tga(4, 18, 'HSV'), tga(5, 18, 'HSV_Value'),
      tga(4, 19, 'RGB'), tga(5, 19, 'RGB_Value'),
      tga(5, 20, 'RGBW'),

      tga(6, 0, 'Lock1'), tga(7, 0, 'Lock_Status'),
      tga(6, 1, 'Lock2'),
      tga(6, 2, 'Scene'),
      tga(6, 3, 'BitScene1'),
      tga(6, 4, 'BitScene2'),
      tga(6, 5, 'BitScene3'),
      tga(6, 6, 'BitScene4'),
    ],
  },
]
