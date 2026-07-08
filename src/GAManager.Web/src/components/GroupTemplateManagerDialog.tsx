import { useEffect, useRef, useState } from 'react'
import {
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
} from '@fluentui/react-components'
import type { Group, GroupTemplate } from '../domain/schema'
import { useProject } from '../state/ProjectContext'
import { createGroupTemplate, getNextStartingBlockIndex, maxGaSubAddress, minGaSubAddress } from '../domain/operations'
import { useI18n } from '../i18n/I18nContext'
import { GaGrid } from './GaGrid'
import { ConfirmDialog } from './dialogs/ConfirmDialog'
import { TextPromptDialog } from './dialogs/TextPromptDialog'
import { DialogShell } from './dialogs/DialogShell'
import { useStyles } from './GroupTemplateManagerDialog.styles'

type InsertMode = 'nextBlock' | 'atPosition' | 'nextFree'

interface GroupTemplateManagerDialogProps {
  open: boolean
  initialMainGroupId: string | null
  onClose: () => void
  onGroupInserted: (mainGroupId: string, group: Group, firstRow: number) => void
}

export function GroupTemplateManagerDialog({ open, initialMainGroupId, onClose, onGroupInserted }: GroupTemplateManagerDialogProps) {
  const styles = useStyles()
  const { t } = useI18n()
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
      setErrorMessage(t('templateManager.addressOccupiedError'))
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
      <DialogShell
        open={open}
        title={t('templateManager.title')}
        onCancel={onClose}
        surfaceStyle={{ maxWidth: '1400px', width: '95vw' }}
        actions={
          <Button appearance="secondary" onClick={onClose}>
            {t('templateManager.close')}
          </Button>
        }
      >
        <div className={styles.body}>
          <div className={styles.leftPane}>
            <Dropdown
              style={{ width: '100%' }}
              placeholder={t('templateManager.selectTemplate')}
              value={selectedTemplate?.name ?? ''}
              selectedOptions={selectedTemplateId ? [selectedTemplateId] : []}
              onOptionSelect={(_, data) => data.optionValue && setSelectedTemplateId(data.optionValue)}
            >
              {[...project.groupTemplates]
                .sort((a, b) => a.name.localeCompare(b.name))
                .map((template) => (
                  <Option key={template.id} value={template.id} text={template.name}>
                    {template.name}
                  </Option>
                ))}
            </Dropdown>
            <div className={styles.buttonRow}>
              <Button size="small" onClick={() => setTemplateDialogMode('new')}>
                {t('templateManager.new')}
              </Button>
              <Button size="small" disabled={!selectedTemplate} onClick={() => setTemplateDialogMode('edit')}>
                {t('templateManager.edit')}
              </Button>
              <Button
                size="small"
                disabled={!selectedTemplate}
                onClick={() => selectedTemplate && setPendingDeleteTemplate(selectedTemplate)}
              >
                {t('templateManager.delete')}
              </Button>
            </div>

            <div style={{ borderTop: `1px solid ${tokens.colorNeutralStroke2}`, paddingTop: '10px' }}>
              <div style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
                <Field label={t('templateManager.groupNamePrefixLabel')}>
                  <Input value={prefix} onChange={(_, data) => setPrefix(data.value)} />
                </Field>
                <Field label={t('templateManager.mainGroupLabel')}>
                  <Dropdown
                    style={{ width: '100%' }}
                    placeholder={t('templateManager.selectMainGroup')}
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

                <Text weight="semibold">{t('templateManager.insertPosition')}</Text>
                <RadioGroup
                  className={styles.positionGroup}
                  value={insertMode}
                  onChange={(_, data) => setInsertMode(data.value as InsertMode)}
                >
                  <div className={styles.positionRow}>
                    <Radio value="nextBlock" label={t('templateManager.nextBlock')} />
                    <Input readOnly value={String(nextBlockIndex)} style={{ width: '70px' }} />
                  </div>
                  <div className={styles.positionRow}>
                    <Radio value="atPosition" label={t('templateManager.atPosition')} />
                    <Input
                      value={atPositionText}
                      readOnly={insertMode !== 'atPosition'}
                      onChange={(_, data) => setAtPositionText(data.value)}
                      style={{ width: '70px' }}
                    />
                  </div>
                  <div className={styles.positionRow}>
                    <Radio value="nextFree" label={t('templateManager.nextFree')} />
                    <Input readOnly value={String(nextFreeIndex)} style={{ width: '70px' }} />
                  </div>
                </RadioGroup>

                <Button appearance="primary" disabled={!canAdd} onClick={() => void handleAdd()}>
                  {t('templateManager.addButton')}
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
              <Text>{t('templateManager.noTemplateSelected')}</Text>
            )}
          </div>
        </div>
      </DialogShell>

      <ConfirmDialog
        open={pendingDeleteTemplate !== null}
        title={t('templateManager.confirmDeleteTemplateTitle')}
        message={t('templateManager.confirmDeleteTemplateMessage')}
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
        title={templateDialogMode === 'new' ? t('templateManager.newTemplateDialogTitle') : t('templateManager.renameTemplateDialogTitle')}
        label={t('common.name')}
        initialValue={templateDialogMode === 'edit' ? (selectedTemplate?.name ?? '') : t('templateManager.defaultTemplateName')}
        onSubmit={handleTemplateDialogSubmit}
        onCancel={() => setTemplateDialogMode(null)}
      />
    </>
  )
}
