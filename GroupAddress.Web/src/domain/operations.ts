import type { Address, GA, Group, GroupTemplate, MainGroup, Project } from './schema'
import { MIDDLE_GROUP_COUNT } from './schema'
import { DEFAULT_GROUP_TEMPLATES, DEFAULT_SUB_GROUP_NAMES } from './defaultTemplates'

export function createId(): string {
  return crypto.randomUUID()
}

export function addressToString(a: Address): string {
  return `${a.mainGroup < 0 ? 'x' : a.mainGroup}/${a.middleGroup}/${a.ga}`
}

export function gaAddressName(ga: GA): string {
  return `${addressToString(ga.address)} - ${ga.name}`
}

export function addressEquals(a: Address, b: Address): boolean {
  return a.mainGroup === b.mainGroup && a.middleGroup === b.middleGroup && a.ga === b.ga
}

export function addressEqualsWithoutMainGroup(a: Address, b: Address): boolean {
  return a.middleGroup === b.middleGroup && a.ga === b.ga
}

export function isValidMainGroupSubAddress(addr: number): boolean {
  return addr >= 0 && addr < 32
}

export function minGaSubAddress(gas: GA[]): number {
  if (gas.length === 0) return -1
  return Math.min(...gas.map((g) => g.address.ga))
}

export function maxGaSubAddress(gas: GA[]): number {
  if (gas.length === 0) return -1
  return Math.max(...gas.map((g) => g.address.ga))
}

export function mainGroupAddressName(mg: MainGroup): string {
  return `${mg.subAddress} - ${mg.name}`
}

export function mainGroupListLabel(mg: MainGroup): string {
  const max = maxGaSubAddress(mg.gas)
  return `${mainGroupAddressName(mg)} (${mg.defaultBlockLength} / ${max === -1 ? '-' : max})`
}

export function getNextStartingBlockIndex(mg: Pick<MainGroup, 'gas' | 'defaultBlockLength'>): number {
  const blockCount = (maxGaSubAddress(mg.gas) + 1) / mg.defaultBlockLength
  const ceiled = Math.ceil(blockCount)
  return ceiled * mg.defaultBlockLength
}

export function cloneGaWithPrefix(ga: GA, prefix: string): GA {
  return {
    id: createId(),
    name: `${prefix}_${ga.name}`,
    groupId: null,
    address: { ...ga.address },
  }
}

export function shiftGa(ga: GA, delta: number): GA {
  return { ...ga, address: { ...ga.address, ga: ga.address.ga + delta } }
}

// ---- MainGroup CRUD -------------------------------------------------------

export function createMainGroup(
  subAddress: number,
  name: string,
  defaultBlockLength: number,
  subGroupNames: string[] = DEFAULT_SUB_GROUP_NAMES,
): MainGroup {
  return {
    id: createId(),
    name,
    subAddress,
    defaultBlockLength: defaultBlockLength > 0 ? defaultBlockLength : 1,
    subGroupNames: [...subGroupNames],
    gas: [],
  }
}

export function nextFreeMainGroupAddress(mainGroups: MainGroup[]): number {
  const used = new Set(mainGroups.map((m) => m.subAddress))
  const max = mainGroups.reduce((acc, m) => Math.max(acc, m.subAddress), -1)
  const candidate = max + 1
  if (isValidMainGroupSubAddress(candidate)) return candidate
  for (let i = 0; i < 32; i++) {
    if (!used.has(i)) return i
  }
  return 0
}

export function addMainGroup(project: Project, mainGroup: MainGroup): Project {
  return { ...project, mainGroups: [...project.mainGroups, mainGroup] }
}

export function removeMainGroup(project: Project, mainGroupId: string): Project {
  return { ...project, mainGroups: project.mainGroups.filter((m) => m.id !== mainGroupId) }
}

export function updateMainGroup(
  project: Project,
  mainGroupId: string,
  patch: { name?: string; subAddress?: number; defaultBlockLength?: number },
): Project {
  return {
    ...project,
    mainGroups: project.mainGroups.map((m) => {
      if (m.id !== mainGroupId) return m
      const subAddress = patch.subAddress ?? m.subAddress
      const gasNeedRenumber = patch.subAddress !== undefined && patch.subAddress !== m.subAddress
      return {
        ...m,
        name: patch.name ?? m.name,
        subAddress,
        defaultBlockLength: patch.defaultBlockLength ?? m.defaultBlockLength,
        gas: gasNeedRenumber ? m.gas.map((g) => ({ ...g, address: { ...g.address, mainGroup: subAddress } })) : m.gas,
      }
    }),
  }
}

// ---- GroupTemplate CRUD ----------------------------------------------------

export function createGroupTemplate(name: string, gas: GA[] = []): GroupTemplate {
  return { id: createId(), name, subGroupNames: Array(MIDDLE_GROUP_COUNT).fill(''), gas }
}

export function addGroupTemplate(project: Project, template: GroupTemplate): Project {
  return { ...project, groupTemplates: [...project.groupTemplates, template] }
}

export function removeGroupTemplate(project: Project, templateId: string): Project {
  return { ...project, groupTemplates: project.groupTemplates.filter((t) => t.id !== templateId) }
}

export function renameGroupTemplate(project: Project, templateId: string, name: string): Project {
  return {
    ...project,
    groupTemplates: project.groupTemplates.map((t) => (t.id === templateId ? { ...t, name } : t)),
  }
}

// ---- Group (tag) CRUD -------------------------------------------------------

export function createGroup(name: string): Group {
  return { id: createId(), name }
}

export function addGroup(project: Project, group: Group): Project {
  return { ...project, groups: [...project.groups, group] }
}

export function renameGroup(project: Project, groupId: string, name: string): Project {
  return { ...project, groups: project.groups.map((g) => (g.id === groupId ? { ...g, name } : g)) }
}

/** Removes the given groups; optionally also deletes their GAs (from wherever they live), otherwise just detaches them. */
export function removeGroups(project: Project, groupIds: string[], includeGAs: boolean): Project {
  const idSet = new Set(groupIds)
  const clearOrDrop = (gas: GA[]) =>
    includeGAs
      ? gas.filter((g) => !g.groupId || !idSet.has(g.groupId))
      : gas.map((g) => (g.groupId && idSet.has(g.groupId) ? { ...g, groupId: null } : g))

  return {
    ...project,
    groups: project.groups.filter((g) => !idSet.has(g.id)),
    mainGroups: project.mainGroups.map((m) => ({ ...m, gas: clearOrDrop(m.gas) })),
  }
}

export function getGroupGAs(project: Project, groupId: string): GA[] {
  return project.mainGroups.flatMap((m) => m.gas).filter((g) => g.groupId === groupId)
}

export function assignGAsToGroup(project: Project, mainGroupId: string, gaIds: string[], groupId: string | null): Project {
  const idSet = new Set(gaIds)
  return {
    ...project,
    mainGroups: project.mainGroups.map((m) =>
      m.id !== mainGroupId ? m : { ...m, gas: m.gas.map((g) => (idSet.has(g.id) ? { ...g, groupId } : g)) },
    ),
  }
}

// ---- GA grid operations, shared between MainGroup and GroupTemplate -------

export type CollectionKind = 'mainGroup' | 'template'

function updateCollectionGas(
  project: Project,
  kind: CollectionKind,
  collectionId: string,
  updater: (gas: GA[]) => GA[],
): Project {
  if (kind === 'mainGroup') {
    return {
      ...project,
      mainGroups: project.mainGroups.map((m) => (m.id === collectionId ? { ...m, gas: updater(m.gas) } : m)),
    }
  }
  return {
    ...project,
    groupTemplates: project.groupTemplates.map((t) => (t.id === collectionId ? { ...t, gas: updater(t.gas) } : t)),
  }
}

export function renameSubGroupColumn(
  project: Project,
  kind: CollectionKind,
  collectionId: string,
  columnIndex: number,
  name: string,
): Project {
  const patchNames = (names: string[]) => names.map((n, i) => (i === columnIndex ? name : n))
  if (kind === 'mainGroup') {
    return {
      ...project,
      mainGroups: project.mainGroups.map((m) =>
        m.id === collectionId ? { ...m, subGroupNames: patchNames(m.subGroupNames) } : m,
      ),
    }
  }
  return {
    ...project,
    groupTemplates: project.groupTemplates.map((t) =>
      t.id === collectionId ? { ...t, subGroupNames: patchNames(t.subGroupNames) } : t,
    ),
  }
}

/** Set (create-or-rename) the GA at a given grid cell. Mirrors TopLevelCollection.AddGA's exact-address dedup. */
export function setCellGA(
  project: Project,
  kind: CollectionKind,
  collectionId: string,
  mainGroupSubAddress: number,
  middleGroup: number,
  ga: number,
  name: string,
): Project {
  return updateCollectionGas(project, kind, collectionId, (gas) => {
    const address: Address = { mainGroup: kind === 'mainGroup' ? mainGroupSubAddress : -1, middleGroup, ga }
    const existing = gas.find((g) => addressEquals(g.address, address))
    if (existing) {
      return gas.map((g) => (g.id === existing.id ? { ...g, name } : g))
    }
    return [...gas, { id: createId(), name, groupId: null, address }]
  })
}

export function removeGAsFromCollection(project: Project, kind: CollectionKind, collectionId: string, gaIds: string[]): Project {
  const idSet = new Set(gaIds)
  return updateCollectionGas(project, kind, collectionId, (gas) => gas.filter((g) => !idSet.has(g.id)))
}

/** "Add cells": insert `count` empty rows at `atRow`, shifting existing GAs at/after that row down, per middle-group column. */
export function insertCells(
  project: Project,
  kind: CollectionKind,
  collectionId: string,
  middleGroups: number[],
  atRow: number,
  count: number,
): Project {
  const cols = new Set(middleGroups)
  return updateCollectionGas(project, kind, collectionId, (gas) =>
    gas.map((g) => (cols.has(g.address.middleGroup) && g.address.ga >= atRow ? shiftGa(g, count) : g)),
  )
}

/** "Delete cells": remove GAs at the given addresses and pull everything below each one up by 1, per column. */
export function deleteCellsAndShift(
  project: Project,
  kind: CollectionKind,
  collectionId: string,
  addresses: Address[],
): Project {
  return updateCollectionGas(project, kind, collectionId, (gas) => {
    const gaIdsToDelete = new Set(
      gas.filter((g) => addresses.some((a) => addressEquals(g.address, a))).map((g) => g.id),
    )
    let result = gas.filter((g) => !gaIdsToDelete.has(g.id))
    const sorted = [...addresses].sort((a, b) => b.ga - a.ga)
    for (const addr of sorted) {
      result = result.map((g) =>
        g.address.middleGroup === addr.middleGroup && g.address.ga > addr.ga ? shiftGa(g, -1) : g,
      )
    }
    return result
  })
}

// ---- Template instantiation -------------------------------------------------

export interface AddGroupFromTemplateResult {
  project: Project
  group: Group | null
}

/** Mirrors MainGroup.AddGroup: clone the template's GAs with a name prefix, shift by startIndex,
 *  and bail out (return null group, unmodified project) if any land on an already-occupied cell. */
export function addGroupFromTemplate(
  project: Project,
  mainGroupId: string,
  template: GroupTemplate,
  gaPrefix: string,
  startIndex: number,
): AddGroupFromTemplateResult {
  const mainGroup = project.mainGroups.find((m) => m.id === mainGroupId)
  if (!mainGroup) return { project, group: null }

  const newGroup = createGroup(gaPrefix)
  const newGas = template.gas
    .map((g) => cloneGaWithPrefix(g, gaPrefix))
    .map((g) => ({ ...g, groupId: newGroup.id }))
    .map((g) => shiftGa(g, startIndex))
    .map((g) => ({ ...g, address: { ...g.address, mainGroup: mainGroup.subAddress } }))

  const collides = newGas.some((x) => mainGroup.gas.some((y) => addressEqualsWithoutMainGroup(y.address, x.address)))
  if (collides) return { project, group: null }

  const withGas: Project = {
    ...project,
    mainGroups: project.mainGroups.map((m) => (m.id === mainGroupId ? { ...m, gas: [...m.gas, ...newGas] } : m)),
  }
  return { project: addGroup(withGas, newGroup), group: newGroup }
}

// ---- Sample project ----------------------------------------------------------

export function buildDefaultGroupTemplates(): GroupTemplate[] {
  return DEFAULT_GROUP_TEMPLATES.map((t) => createGroupTemplate(t.name, t.gas.map((g) => ({ ...g, id: createId() }))))
}

export function createEmptyProject(): Project {
  return { created: new Date().toISOString(), mainGroups: [], groupTemplates: [], groups: [] }
}

export function buildSampleProject(): Project {
  let project = createEmptyProject()

  const templates = buildDefaultGroupTemplates()
  for (const t of templates) project = addGroupTemplate(project, t)

  const mainGroupDefs: Array<[number, string, number]> = [
    [1, 'Licht allgemein', 10],
    [2, 'Licht dimmbar', 10],
    [3, 'Licht TW', 10],
    [4, 'Licht RGBW #1', 50],
  ]
  for (const [subAddress, name, blockLength] of mainGroupDefs) {
    project = addMainGroup(project, createMainGroup(subAddress, name, blockLength))
  }

  const firstMainGroup = project.mainGroups.find((m) => m.subAddress === 1)
  const lightTemplate = templates[0]
  if (firstMainGroup && lightTemplate) {
    const result = addGroupFromTemplate(project, firstMainGroup.id, lightTemplate, 'EG_HWR_Licht_Decke', 0)
    project = result.project
  }

  return project
}
