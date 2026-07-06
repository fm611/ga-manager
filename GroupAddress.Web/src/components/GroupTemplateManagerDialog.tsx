import { useEffect, useRef, useState } from 'react'
import {
  Dialog,
  DialogSurface,
  DialogBody,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Field,
  Input,
  Dropdown,
  Option,
  RadioGroup,
  Radio,
  Text,
  MessageBar,
  MessageBarBody,
  tokens,
  makeStyles,
} from '@fluentui/react-components'
import type { Group, GroupTemplate } from '../domain/schema'
import { useProject } from '../state/ProjectContext'
import { createGroupTemplate, getNextStartingBlockIndex, maxGaSubAddress, minGaSubAddress } from '../domain/operations'
import { GaGrid } from './GaGrid'
import { ConfirmDialog } from './dialogs/ConfirmDialog'
import { TextPromptDialog } from './dialogs/TextPromptDialog'

const useStyles = makeStyles({
  body: { display: 'flex', gap: '16px', height: '65vh', minHeight: '480px' },
  leftPane: { width: '300px', flexShrink: 0, display: 'flex', flexDirection: 'column', gap: '10px', overflowY: 'auto' },
  rightPane: { flex: 1, minWidth: 0, display: 'flex', flexDirection: 'column' },
  buttonRow: { display: 'flex', gap: '6px' },
  positionGroup: { display: 'flex', flexDirection: 'column', gap: '10px', width: '100%' },
  positionRow: { display: 'flex', alignItems: 'center', justifyContent: 'space-between', gap: '8px', width: '100%' },
})

type InsertMode = 'nextBlock' | 'atPosition' | 'nextFree'

interface GroupTemplateManagerDialogProps {
  open: boolean
  initialMainGroupId: string | null
  onClose: () => void
  onGroupInserted: (mainGroupId: string, group: Group, firstRow: number) => void
}

export function GroupTemplateManagerDialog({ open, initialMainGroupId, onClose, onGroupInserted }: GroupTemplateManagerDialogProps) {
  const styles = useStyles()
  const {
    project,
    addGroupTemplate,
    removeGroupTemplate,
    renameGroupTemplate,
    addGroupFromTemplate,
    setCellGA,
    removeGAsFromCollection,
    renameSubGroupColumn,
  } = useProject()

  const [selectedTemplateId, setSelectedTemplateId] = useState<string | null>(null)
  const [selectedMainGroupId, setSelectedMainGroupId] = useState<string | null>(null)
  const [prefix, setPrefix] = useState('')
  const [insertMode, setInsertMode] = useState<InsertMode>('nextBlock')
  const [atPositionText, setAtPositionText] = useState('0')
  const [templateDialogMode, setTemplateDialogMode] = useState<'new' | 'edit' | null>(null)
  const [errorMessage, setErrorMessage] = useState<string | null>(null)
  const [pendingDeleteTemplate, setPendingDeleteTemplate] = useState<GroupTemplate | null>(null)
  const lastInsert = useRef<{ mainGroupId?: string; templateId?: string }>({})

  useEffect(() => {
    if (!open) return
    setErrorMessage(null)
    setPrefix('')
    setSelectedMainGroupId((current) => lastInsert.current.mainGroupId ?? initialMainGroupId ?? current ?? project.mainGroups[0]?.id ?? null)
    setSelectedTemplateId((current) => lastInsert.current.templateId ?? current ?? project.groupTemplates[0]?.id ?? null)
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [open])

  const selectedTemplate = project.groupTemplates.find((t) => t.id === selectedTemplateId) ?? null
  const mainGroup = project.mainGroups.find((m) => m.id === selectedMainGroupId) ?? null

  const nextBlockIndex = mainGroup ? getNextStartingBlockIndex(mainGroup) : 0
  const nextFreeIndex = mainGroup ? maxGaSubAddress(mainGroup.gas) + 1 : 0

  function getInsertIndex(): number {
    if (!mainGroup) return 0
    if (insertMode === 'atPosition') {
      const parsed = Number(atPositionText)
      return Number.isFinite(parsed) ? parsed : nextFreeIndex
    }
    if (insertMode === 'nextFree') return nextFreeIndex
    return nextBlockIndex
  }

  const canAdd = prefix.trim().length > 0 && !!selectedTemplate && !!mainGroup

  async function handleAdd() {
    if (!mainGroup || !selectedTemplate) return
    const startIndex = getInsertIndex()
    const group = await addGroupFromTemplate(mainGroup.id, selectedTemplate, prefix.trim(), startIndex)
    if (group) {
      lastInsert.current = { mainGroupId: mainGroup.id, templateId: selectedTemplate.id }
      setErrorMessage(null)
      const firstRow = startIndex + minGaSubAddress(selectedTemplate.gas)
      onGroupInserted(mainGroup.id, group, firstRow)
      onClose()
    } else {
      setErrorMessage('Adressbereich bereits belegt – Gruppe konnte nicht platziert werden.')
    }
  }

  function handleTemplateDialogSubmit(name: string) {
    if (templateDialogMode === 'new') {
      const template = createGroupTemplate(name, [])
      addGroupTemplate(template)
      setSelectedTemplateId(template.id)
    } else if (templateDialogMode === 'edit' && selectedTemplate) {
      renameGroupTemplate(selectedTemplate.id, name)
    }
    setTemplateDialogMode(null)
  }

  return (
    <>
    <Dialog open={open} onOpenChange={(_, data) => !data.open && onClose()}>
      <DialogSurface style={{ maxWidth: '1400px', width: '95vw' }}>
        <DialogBody>
          <DialogTitle>Gruppen Manager</DialogTitle>
          <DialogContent>
            <div className={styles.body}>
              <div className={styles.leftPane}>
                <Dropdown
                  style={{ width: '100%' }}
                  placeholder="Template wählen"
                  value={selectedTemplate?.name ?? ''}
                  selectedOptions={selectedTemplateId ? [selectedTemplateId] : []}
                  onOptionSelect={(_, data) => data.optionValue && setSelectedTemplateId(data.optionValue)}
                >
                  {[...project.groupTemplates]
                    .sort((a, b) => a.name.localeCompare(b.name))
                    .map((t) => (
                      <Option key={t.id} value={t.id} text={t.name}>
                        {t.name}
                      </Option>
                    ))}
                </Dropdown>
                <div className={styles.buttonRow}>
                  <Button size="small" onClick={() => setTemplateDialogMode('new')}>
                    Neu
                  </Button>
                  <Button size="small" disabled={!selectedTemplate} onClick={() => setTemplateDialogMode('edit')}>
                    Bearbeiten
                  </Button>
                  <Button
                    size="small"
                    disabled={!selectedTemplate}
                    onClick={() => selectedTemplate && setPendingDeleteTemplate(selectedTemplate)}
                  >
                    Löschen
                  </Button>
                </div>

                <div style={{ borderTop: `1px solid ${tokens.colorNeutralStroke2}`, paddingTop: '10px' }}>
                  <div style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
                    <Field label="Gruppen Name (PreString)">
                      <Input value={prefix} onChange={(_, data) => setPrefix(data.value)} />
                    </Field>
                    <Field label="Hauptgruppe">
                      <Dropdown
                        style={{ width: '100%' }}
                        placeholder="Hauptgruppe wählen"
                        value={mainGroup ? `${mainGroup.subAddress} - ${mainGroup.name}` : ''}
                        selectedOptions={selectedMainGroupId ? [selectedMainGroupId] : []}
                        onOptionSelect={(_, data) => data.optionValue && setSelectedMainGroupId(data.optionValue)}
                      >
                        {[...project.mainGroups]
                          .sort((a, b) => a.subAddress - b.subAddress)
                          .map((m) => (
                            <Option key={m.id} value={m.id} text={`${m.subAddress} - ${m.name}`}>
                              {m.subAddress} - {m.name}
                            </Option>
                          ))}
                      </Dropdown>
                    </Field>

                    <Text weight="semibold">Einfüge Position</Text>
                    <RadioGroup
                      className={styles.positionGroup}
                      value={insertMode}
                      onChange={(_, data) => setInsertMode(data.value as InsertMode)}
                    >
                      <div className={styles.positionRow}>
                        <Radio value="nextBlock" label="Nächster Block:" />
                        <Input readOnly value={String(nextBlockIndex)} style={{ width: '70px' }} />
                      </div>
                      <div className={styles.positionRow}>
                        <Radio value="atPosition" label="An Position:" />
                        <Input
                          value={atPositionText}
                          readOnly={insertMode !== 'atPosition'}
                          onChange={(_, data) => setAtPositionText(data.value)}
                          style={{ width: '70px' }}
                        />
                      </div>
                      <div className={styles.positionRow}>
                        <Radio value="nextFree" label="Nächste Freie:" />
                        <Input readOnly value={String(nextFreeIndex)} style={{ width: '70px' }} />
                      </div>
                    </RadioGroup>

                    <Button appearance="primary" disabled={!canAdd} onClick={() => void handleAdd()}>
                      Hinzufügen
                    </Button>
                  </div>
                </div>

                {errorMessage && (
                  <MessageBar intent="error">
                    <MessageBarBody>{errorMessage}</MessageBarBody>
                  </MessageBar>
                )}
              </div>

              <div className={styles.rightPane}>
                {selectedTemplate ? (
                  <GaGrid
                    gas={selectedTemplate.gas}
                    subGroupNames={selectedTemplate.subGroupNames}
                    mainGroupSubAddress={-1}
                    readOnly={false}
                    filterGroupIds={null}
                    onCommitCell={(middleGroup, ga, name) => setCellGA('template', selectedTemplate.id, -1, middleGroup, ga, name)}
                    onDeleteGAs={(gaIds) => removeGAsFromCollection('template', selectedTemplate.id, gaIds)}
                    onRenameColumn={(columnIndex, name) => renameSubGroupColumn('template', selectedTemplate.id, columnIndex, name)}
                  />
                ) : (
                  <Text>Kein Template ausgewählt.</Text>
                )}
              </div>
            </div>
          </DialogContent>
          <DialogActions>
            <Button appearance="secondary" onClick={onClose}>
              Schließen
            </Button>
          </DialogActions>
        </DialogBody>
      </DialogSurface>
    </Dialog>

      <ConfirmDialog
        open={pendingDeleteTemplate !== null}
        title="Template löschen"
        message="Template löschen?"
        onConfirm={() => {
          if (pendingDeleteTemplate) {
            removeGroupTemplate(pendingDeleteTemplate.id)
            if (selectedTemplateId === pendingDeleteTemplate.id) setSelectedTemplateId(null)
          }
          setPendingDeleteTemplate(null)
        }}
        onCancel={() => setPendingDeleteTemplate(null)}
      />

      <TextPromptDialog
        open={templateDialogMode !== null}
        title={templateDialogMode === 'new' ? 'Neues Template' : 'Template umbenennen'}
        label="Name"
        initialValue={templateDialogMode === 'edit' ? (selectedTemplate?.name ?? '') : 'Neues Template'}
        onSubmit={handleTemplateDialogSubmit}
        onCancel={() => setTemplateDialogMode(null)}
      />
    </>
  )
}
