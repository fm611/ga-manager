import { createContext, useContext, useMemo, useReducer, type ReactNode } from 'react'
import type { Address, GA, Group, GroupTemplate, MainGroup, Project } from '../domain/schema'
import * as ops from '../domain/operations'
import type { CollectionKind } from '../domain/operations'

type Action =
  | { type: 'reset'; project: Project }
  | { type: 'addMainGroup'; mainGroup: MainGroup }
  | { type: 'removeMainGroup'; mainGroupId: string }
  | { type: 'updateMainGroup'; mainGroupId: string; patch: { name?: string; subAddress?: number; defaultBlockLength?: number } }
  | { type: 'addGroupTemplate'; template: GroupTemplate }
  | { type: 'removeGroupTemplate'; templateId: string }
  | { type: 'renameGroupTemplate'; templateId: string; name: string }
  | { type: 'addGroup'; group: Group }
  | { type: 'renameGroup'; groupId: string; name: string }
  | { type: 'removeGroups'; groupIds: string[]; includeGAs: boolean }
  | { type: 'assignGAsToGroup'; mainGroupId: string; gaIds: string[]; groupId: string | null }
  | { type: 'renameSubGroupColumn'; kind: CollectionKind; collectionId: string; columnIndex: number; name: string }
  | { type: 'setCellGA'; kind: CollectionKind; collectionId: string; mainGroupSubAddress: number; middleGroup: number; ga: number; name: string }
  | { type: 'removeGAsFromCollection'; kind: CollectionKind; collectionId: string; gaIds: string[] }
  | { type: 'insertCells'; kind: CollectionKind; collectionId: string; middleGroups: number[]; atRow: number; count: number }
  | { type: 'deleteCellsAndShift'; kind: CollectionKind; collectionId: string; addresses: Address[] }
  | { type: 'addGroupFromTemplate'; mainGroupId: string; template: GroupTemplate; gaPrefix: string; startIndex: number; onResult: (group: Group | null) => void }

function reducer(project: Project, action: Action): Project {
  switch (action.type) {
    case 'reset':
      return action.project
    case 'addMainGroup':
      return ops.addMainGroup(project, action.mainGroup)
    case 'removeMainGroup':
      return ops.removeMainGroup(project, action.mainGroupId)
    case 'updateMainGroup':
      return ops.updateMainGroup(project, action.mainGroupId, action.patch)
    case 'addGroupTemplate':
      return ops.addGroupTemplate(project, action.template)
    case 'removeGroupTemplate':
      return ops.removeGroupTemplate(project, action.templateId)
    case 'renameGroupTemplate':
      return ops.renameGroupTemplate(project, action.templateId, action.name)
    case 'addGroup':
      return ops.addGroup(project, action.group)
    case 'renameGroup':
      return ops.renameGroup(project, action.groupId, action.name)
    case 'removeGroups':
      return ops.removeGroups(project, action.groupIds, action.includeGAs)
    case 'assignGAsToGroup':
      return ops.assignGAsToGroup(project, action.mainGroupId, action.gaIds, action.groupId)
    case 'renameSubGroupColumn':
      return ops.renameSubGroupColumn(project, action.kind, action.collectionId, action.columnIndex, action.name)
    case 'setCellGA':
      return ops.setCellGA(
        project,
        action.kind,
        action.collectionId,
        action.mainGroupSubAddress,
        action.middleGroup,
        action.ga,
        action.name,
      )
    case 'removeGAsFromCollection':
      return ops.removeGAsFromCollection(project, action.kind, action.collectionId, action.gaIds)
    case 'insertCells':
      return ops.insertCells(project, action.kind, action.collectionId, action.middleGroups, action.atRow, action.count)
    case 'deleteCellsAndShift':
      return ops.deleteCellsAndShift(project, action.kind, action.collectionId, action.addresses)
    case 'addGroupFromTemplate': {
      const result = ops.addGroupFromTemplate(project, action.mainGroupId, action.template, action.gaPrefix, action.startIndex)
      action.onResult(result.group)
      return result.project
    }
    default:
      return project
  }
}

interface ProjectContextValue {
  project: Project
  resetProject: (project: Project) => void
  addMainGroup: (mainGroup: MainGroup) => void
  removeMainGroup: (mainGroupId: string) => void
  updateMainGroup: (mainGroupId: string, patch: { name?: string; subAddress?: number; defaultBlockLength?: number }) => void
  addGroupTemplate: (template: GroupTemplate) => void
  removeGroupTemplate: (templateId: string) => void
  renameGroupTemplate: (templateId: string, name: string) => void
  addGroup: (group: Group) => void
  renameGroup: (groupId: string, name: string) => void
  removeGroups: (groupIds: string[], includeGAs: boolean) => void
  assignGAsToGroup: (mainGroupId: string, gaIds: string[], groupId: string | null) => void
  renameSubGroupColumn: (kind: CollectionKind, collectionId: string, columnIndex: number, name: string) => void
  setCellGA: (kind: CollectionKind, collectionId: string, mainGroupSubAddress: number, middleGroup: number, ga: number, name: string) => void
  removeGAsFromCollection: (kind: CollectionKind, collectionId: string, gaIds: GA['id'][]) => void
  insertCells: (kind: CollectionKind, collectionId: string, middleGroups: number[], atRow: number, count: number) => void
  deleteCellsAndShift: (kind: CollectionKind, collectionId: string, addresses: Address[]) => void
  addGroupFromTemplate: (mainGroupId: string, template: GroupTemplate, gaPrefix: string, startIndex: number) => Promise<Group | null>
}

const ProjectContext = createContext<ProjectContextValue | null>(null)

export function ProjectProvider({ project: initialProject, children }: { project: Project; children: ReactNode }) {
  const [project, dispatch] = useReducer(reducer, initialProject)

  const value = useMemo<ProjectContextValue>(
    () => ({
      project,
      resetProject: (p) => dispatch({ type: 'reset', project: p }),
      addMainGroup: (mainGroup) => dispatch({ type: 'addMainGroup', mainGroup }),
      removeMainGroup: (mainGroupId) => dispatch({ type: 'removeMainGroup', mainGroupId }),
      updateMainGroup: (mainGroupId, patch) => dispatch({ type: 'updateMainGroup', mainGroupId, patch }),
      addGroupTemplate: (template) => dispatch({ type: 'addGroupTemplate', template }),
      removeGroupTemplate: (templateId) => dispatch({ type: 'removeGroupTemplate', templateId }),
      renameGroupTemplate: (templateId, name) => dispatch({ type: 'renameGroupTemplate', templateId, name }),
      addGroup: (group) => dispatch({ type: 'addGroup', group }),
      renameGroup: (groupId, name) => dispatch({ type: 'renameGroup', groupId, name }),
      removeGroups: (groupIds, includeGAs) => dispatch({ type: 'removeGroups', groupIds, includeGAs }),
      assignGAsToGroup: (mainGroupId, gaIds, groupId) => dispatch({ type: 'assignGAsToGroup', mainGroupId, gaIds, groupId }),
      renameSubGroupColumn: (kind, collectionId, columnIndex, name) =>
        dispatch({ type: 'renameSubGroupColumn', kind, collectionId, columnIndex, name }),
      setCellGA: (kind, collectionId, mainGroupSubAddress, middleGroup, ga, name) =>
        dispatch({ type: 'setCellGA', kind, collectionId, mainGroupSubAddress, middleGroup, ga, name }),
      removeGAsFromCollection: (kind, collectionId, gaIds) => dispatch({ type: 'removeGAsFromCollection', kind, collectionId, gaIds }),
      insertCells: (kind, collectionId, middleGroups, atRow, count) =>
        dispatch({ type: 'insertCells', kind, collectionId, middleGroups, atRow, count }),
      deleteCellsAndShift: (kind, collectionId, addresses) => dispatch({ type: 'deleteCellsAndShift', kind, collectionId, addresses }),
      addGroupFromTemplate: (mainGroupId, template, gaPrefix, startIndex) =>
        new Promise<Group | null>((resolve) => {
          dispatch({ type: 'addGroupFromTemplate', mainGroupId, template, gaPrefix, startIndex, onResult: resolve })
        }),
    }),
    [project],
  )

  return <ProjectContext.Provider value={value}>{children}</ProjectContext.Provider>
}

export function useProject(): ProjectContextValue {
  const ctx = useContext(ProjectContext)
  if (!ctx) throw new Error('useProject must be used within a ProjectProvider')
  return ctx
}
