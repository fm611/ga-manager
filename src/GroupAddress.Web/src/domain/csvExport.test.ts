import { describe, expect, it } from 'vitest'
import { buildGaExportCsv } from './csvExport'
import type { Project } from './schema'

function makeProject(): Project {
  return {
    created: '2026-01-01T00:00:00.000Z',
    groups: [],
    groupTemplates: [],
    mainGroups: [
      {
        id: 'mg1',
        name: 'Beleuchtung',
        subAddress: 1,
        defaultBlockLength: 1,
        subGroupNames: ['Schalten', '', '', '', '', '', '', ''],
        gas: [
          {
            id: 'ga1',
            name: 'Flur',
            groupId: null,
            address: { mainGroup: 1, middleGroup: 0, ga: 0 },
          },
        ],
      },
    ],
  }
}

describe('buildGaExportCsv', () => {
  it('emits the ETS-style Main/Middle/Sub header row', () => {
    const csv = buildGaExportCsv(makeProject())

    expect(csv.split('\r\n')[0]).toBe(
      '"Main";"Middle";"Sub";"Main";"Middle";"Sub";"Central";"Unfiltered";"Description";"DatapointType";"Security"',
    )
  })

  it('emits a row for the main group, one per middle group slot, and one per GA', () => {
    const csv = buildGaExportCsv(makeProject())
    const rows = csv.trim().split('\r\n')

    // header + main group row + 8 middle group slots + 1 GA
    expect(rows).toHaveLength(1 + 1 + 8 + 1)
    expect(rows[1]).toBe('"Beleuchtung";"";"";"1";"";"";"";"";"";"";"Auto"')
    expect(rows[2]).toBe('"";"Schalten";"";"1";"0";"";"";"";"";"";"Auto"')
    expect(rows).toContain('"";"";"Flur";"1";"0";"0";"";"";"";"";"Auto"')
  })
})
