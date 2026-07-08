import type { GA } from './schema'

export const DEFAULT_SUB_GROUP_NAMES = [
  'Zentral',
  'Zentral Status',
  'Schalten',
  'Schalten Status',
  'SET Wert',
  'GET Wert',
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
    name: 'Licht allgemein',
    gas: [
      tga(2, 0, 'Schalten'), tga(3, 0, 'Schalten_Status'),

      tga(6, 0, 'Sperren1'), tga(7, 0, 'Sperren_Status'),
      tga(6, 1, 'Sperren2'),
      tga(6, 2, 'Szene'),
    ],
  },
  {
    name: 'Licht Dimm_bar',
    gas: [
      tga(2, 0, 'Schalten'), tga(3, 0, 'Schalten_Status'),
      tga(2, 2, 'Sequqenz1_Start'), tga(3, 2, 'Sequqenz1_Status'),
      tga(2, 3, 'Sequqenz2_Start'), tga(3, 3, 'Sequqenz2_Status'),

      tga(4, 0, 'Dimm_Hell_relativ'),
      tga(4, 1, 'Dimm_Hell_absolut'), tga(5, 1, 'Dimm_Hell_Wert'),

      tga(6, 0, 'Sperren1'), tga(7, 0, 'Sperren_Status'),
      tga(6, 1, 'Sperren2'),
      tga(6, 2, 'Szene'),
      tga(6, 3, 'Bitszene1'),
      tga(6, 4, 'Bitszene2'),
      tga(6, 5, 'Bitszene3'),
      tga(6, 6, 'Bitszene4'),
    ],
  },
  {
    name: 'Licht TW',
    gas: [
      tga(2, 0, 'Schalten'), tga(3, 0, 'Schalten_Status'),
      tga(2, 1, 'HCL_Start'), tga(3, 1, 'HCL_Status'),
      tga(2, 2, 'Sequqenz1_Start'), tga(3, 2, 'Sequqenz1_Status'),
      tga(2, 3, 'Sequqenz2_Start'), tga(3, 3, 'Sequqenz2_Status'),

      tga(4, 0, 'Dimm_Hell_relativ'),
      tga(4, 1, 'Dimm_Hell_absolut'), tga(5, 1, 'Dimm_Hell_Wert'),
      tga(4, 2, 'Dimm_Temp_P_relativ'),
      tga(4, 3, 'Dimm_Temp_P_absolut'), tga(5, 3, 'Dimm_Temp_P_Wert'),
      tga(4, 4, 'Dimm_Temp_K_relativ'),
      tga(4, 5, 'Dimm_Temp_K_absolut'), tga(5, 5, 'Dimm_Temp_K_Wert'),

      tga(6, 0, 'Sperren1'), tga(7, 0, 'Sperren_Status'),
      tga(6, 1, 'Sperren2'),
      tga(6, 2, 'Szene'),
      tga(6, 3, 'Bitszene1'),
      tga(6, 4, 'Bitszene2'),
      tga(6, 5, 'Bitszene3'),
      tga(6, 6, 'Bitszene4'),
    ],
  },
  {
    name: 'Licht RGBW',
    gas: [
      tga(2, 0, 'Schalten'), tga(3, 0, 'Schalten_Status'),
      tga(2, 1, 'HCL_Start'), tga(3, 1, 'HCL_Status'),
      tga(2, 2, 'Sequqenz1_Start'), tga(3, 2, 'Sequqenz1_Status'),
      tga(2, 3, 'Sequqenz2_Start'), tga(3, 3, 'Sequqenz2_Status'),
      tga(2, 4, 'Sequqenz3_Start'), tga(3, 4, 'Sequqenz3_Status'),
      tga(2, 5, 'Sequqenz4_Start'), tga(3, 5, 'Sequqenz4_Status'),
      tga(2, 6, 'Sequqenz5_Start'), tga(3, 6, 'Sequqenz5_Status'),
      tga(2, 7, 'Sequqenz6_Start'), tga(3, 7, 'Sequqenz6_Status'),

      tga(2, 8, 'R_Schalten'), tga(3, 8, 'R_Schalten_Status'),
      tga(2, 9, 'G_Schalten'), tga(3, 9, 'G_Schalten_Status'),
      tga(2, 10, 'B_Schalten'), tga(3, 10, 'B_Schalten_Status'),
      tga(2, 11, 'W_Schalten'), tga(3, 11, 'W_Schalten_Status'),

      tga(4, 0, 'Dimm_H_relativ'),
      tga(4, 1, 'Dimm_H_absolut'), tga(5, 1, 'Dimm_H_Wert'),
      tga(4, 2, 'Dimm_S_relativ'),
      tga(4, 3, 'Dimm_S_absolut'), tga(5, 3, 'Dimm_S_Wert'),
      tga(4, 4, 'Dimm_V_relativ'),
      tga(4, 5, 'Dimm_V_absolut'), tga(5, 5, 'Dimm_V_Wert'),

      tga(4, 6, 'Dimm_R_relativ'),
      tga(4, 7, 'Dimm_R_absolut'), tga(5, 7, 'Dimm_R_Wert'),
      tga(4, 8, 'Dimm_G_relativ'),
      tga(4, 9, 'Dimm_G_absolut'), tga(5, 9, 'Dimm_G_Wert'),
      tga(4, 10, 'Dimm_B_relativ'),
      tga(4, 11, 'Dimm_B_absolut'), tga(5, 11, 'Dimm_B_Wert'),
      tga(4, 12, 'Dimm_W_relativ'),
      tga(4, 13, 'Dimm_W_absolut'), tga(5, 13, 'Dimm_W_Wert'),

      tga(4, 14, 'Dimm_Temp_P_relativ'),
      tga(4, 15, 'Dimm_Temp_P_absolut'), tga(5, 15, 'Dimm_Temp_P_Wert'),
      tga(4, 16, 'Dimm_Temp_K_relativ'),
      tga(4, 17, 'Dimm_Temp_K_absolut'), tga(5, 17, 'Dimm_Temp_K_Wert'),

      tga(4, 18, 'HSV'), tga(5, 18, 'HSV_Wert'),
      tga(4, 19, 'RGB'), tga(5, 19, 'RGB_Wert'),
      tga(5, 20, 'RGBW'),

      tga(6, 0, 'Sperren1'), tga(7, 0, 'Sperren_Status'),
      tga(6, 1, 'Sperren2'),
      tga(6, 2, 'Szene'),
      tga(6, 3, 'Bitszene1'),
      tga(6, 4, 'Bitszene2'),
      tga(6, 5, 'Bitszene3'),
      tga(6, 6, 'Bitszene4'),
    ],
  },
]
