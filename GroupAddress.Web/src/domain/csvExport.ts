import type { Project } from './schema'

const CSV_HEADER = ['Main', 'Middle', 'Sub', 'Main', 'Middle', 'Sub', 'Central', 'Unfiltered', 'Description', 'DatapointType', 'Security']

function csvField(value: string): string {
  return `"${value.replace(/"/g, '""')}"`
}

function csvRow(fields: string[]): string {
  return fields.map(csvField).join(';')
}

/** Builds a semicolon-separated group address export matching the ETS-style Main/Middle/Sub CSV schema. */
export function buildGaExportCsv(project: Project): string {
  const lines: string[] = [csvRow(CSV_HEADER)]

  const mainGroups = [...project.mainGroups].sort((a, b) => a.subAddress - b.subAddress)

  for (const mainGroup of mainGroups) {
    lines.push(csvRow([mainGroup.name, '', '', String(mainGroup.subAddress), '', '', '', '', '', '', 'Auto']))

    for (let middleIdx = 0; middleIdx < mainGroup.subGroupNames.length; middleIdx++) {
      const gasInMiddle = mainGroup.gas
        .filter((ga) => ga.address.middleGroup === middleIdx)
        .sort((a, b) => a.address.ga - b.address.ga)

      lines.push(
        csvRow(['', mainGroup.subGroupNames[middleIdx], '', String(mainGroup.subAddress), String(middleIdx), '', '', '', '', '', 'Auto']),
      )

      for (const ga of gasInMiddle) {
        lines.push(
          csvRow(['', '', ga.name, String(mainGroup.subAddress), String(middleIdx), String(ga.address.ga), '', '', '', '', 'Auto']),
        )
      }
    }
  }

  return lines.join('\r\n') + '\r\n'
}
