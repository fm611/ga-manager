import type { GA, MainGroup, Project } from './schema'
import { MAX_GA_SUBADDRESS, MAX_MAIN_GROUP, MIDDLE_GROUP_COUNT, MIN_MAIN_GROUP, ProjectSchema } from './schema'
import { DEFAULT_SUB_GROUP_NAMES } from './defaultTemplates'
import { createId } from './operations'

const EXPECTED_HEADER = ['main', 'middle', 'sub', 'main', 'middle', 'sub', 'central', 'unfiltered', 'description', 'datapointtype', 'security']

function parseCsvRows(text: string): string[][] {
  const rows: string[][] = []
  let row: string[] = []
  let field = ''
  let inQuotes = false

  for (let i = 0; i < text.length; i++) {
    const ch = text[i]
    if (inQuotes) {
      if (ch === '"') {
        if (text[i + 1] === '"') {
          field += '"'
          i += 1
        } else {
          inQuotes = false
        }
      } else {
        field += ch
      }
      continue
    }

    if (ch === '"') {
      inQuotes = true
    } else if (ch === ';') {
      row.push(field)
      field = ''
    } else if (ch === '\r') {
      // ignore, \n (or end of input) terminates the row
    } else if (ch === '\n') {
      row.push(field)
      rows.push(row)
      row = []
      field = ''
    } else {
      field += ch
    }
  }
  if (field !== '' || row.length > 0) {
    row.push(field)
    rows.push(row)
  }

  return rows.filter((r) => r.some((f) => f.trim() !== ''))
}

/** Parses the Main/Middle/Sub group address CSV schema produced by the Export function back into a Project. */
export function parseGaImportCsv(text: string): Project | null {
  const rows = parseCsvRows(text)
  if (rows.length === 0) return null

  const header = rows[0].map((h) => h.trim().toLowerCase())
  if (EXPECTED_HEADER.some((expected, i) => header[i] !== expected)) return null

  const mainGroupsByAddress = new Map<number, MainGroup>()
  const mainOrder: number[] = []

  for (const row of rows.slice(1)) {
    const [mainName, middleName, subName, mainAddrRaw, middleAddrRaw, subAddrRaw] = row.map((f) => f.trim())

    const mainAddr = Number(mainAddrRaw)
    if (!Number.isInteger(mainAddr) || mainAddr < MIN_MAIN_GROUP || mainAddr > MAX_MAIN_GROUP) continue

    let mainGroup = mainGroupsByAddress.get(mainAddr)
    if (!mainGroup) {
      mainGroup = {
        id: createId(),
        name: mainName || `Hauptgruppe ${mainAddr}`,
        subAddress: mainAddr,
        defaultBlockLength: 1,
        subGroupNames: [...DEFAULT_SUB_GROUP_NAMES],
        gas: [],
      }
      mainGroupsByAddress.set(mainAddr, mainGroup)
      mainOrder.push(mainAddr)
    } else if (mainName) {
      mainGroup.name = mainName
    }

    if (middleAddrRaw === '') continue

    const middleAddr = Number(middleAddrRaw)
    if (!Number.isInteger(middleAddr) || middleAddr < 0 || middleAddr >= MIDDLE_GROUP_COUNT) continue

    if (middleName) mainGroup.subGroupNames[middleAddr] = middleName

    if (subAddrRaw === '') continue

    const subAddr = Number(subAddrRaw)
    if (!Number.isInteger(subAddr) || subAddr < 0 || subAddr > MAX_GA_SUBADDRESS) continue
    if (!subName) continue

    const ga: GA = {
      id: createId(),
      name: subName,
      groupId: null,
      address: { mainGroup: mainAddr, middleGroup: middleAddr, ga: subAddr },
    }
    mainGroup.gas.push(ga)
  }

  // A header with no data rows is a valid (empty) project; only reject when there
  // were data rows but none of them produced a usable main group (malformed CSV).
  if (mainOrder.length === 0 && rows.length > 1) return null

  const project: Project = {
    created: new Date().toISOString(),
    mainGroups: mainOrder.map((addr) => mainGroupsByAddress.get(addr)!),
    groupTemplates: [],
    groups: [],
  }

  const validated = ProjectSchema.safeParse(project)
  return validated.success ? validated.data : null
}
