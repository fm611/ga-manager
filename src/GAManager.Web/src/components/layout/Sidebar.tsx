import type { MainGroup } from '../../domain/schema'
import { MainGroupPanel } from '../MainGroupPanel'
import { GroupPanel } from '../GroupPanel'
import { useStyles } from './Sidebar.styles'

export interface SidebarProps {
  mainGroups: MainGroup[]
  selectedMainGroupId: string | null
  onSelectMainGroup: (id: string | null) => void
  onAddMainGroup: () => void
  onEditMainGroup: (mainGroup: MainGroup) => void
  onDeleteMainGroup: (mainGroup: MainGroup) => void
  selectedGroupIds: string[]
  onGroupSelectionChange: (ids: string[]) => void
  onOpenTemplateManager: () => void
  noGroupFilterActive: boolean
  onToggleNoGroupFilter: () => void
  onClearFilter: () => void
  filterDisabled: boolean
}

export function Sidebar({
  mainGroups,
  selectedMainGroupId,
  onSelectMainGroup,
  onAddMainGroup,
  onEditMainGroup,
  onDeleteMainGroup,
  selectedGroupIds,
  onGroupSelectionChange,
  onOpenTemplateManager,
  noGroupFilterActive,
  onToggleNoGroupFilter,
  onClearFilter,
  filterDisabled,
}: SidebarProps) {
  const styles = useStyles()

  return (
    <div className={styles.leftColumn}>
      <div className={styles.leftTop}>
        <MainGroupPanel
          mainGroups={mainGroups}
          selectedId={selectedMainGroupId}
          onSelect={onSelectMainGroup}
          onAdd={onAddMainGroup}
          onEdit={onEditMainGroup}
          onDelete={onDeleteMainGroup}
        />
      </div>
      <div className={styles.leftBottom}>
        <GroupPanel
          selectedIds={selectedGroupIds}
          onSelectionChange={onGroupSelectionChange}
          onOpenTemplateManager={onOpenTemplateManager}
          noGroupFilterActive={noGroupFilterActive}
          onToggleNoGroupFilter={onToggleNoGroupFilter}
          onClearFilter={onClearFilter}
          filterDisabled={filterDisabled}
        />
      </div>
    </div>
  )
}
