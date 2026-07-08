import { z } from 'zod'

export const MIN_MAIN_GROUP = 0
export const MAX_MAIN_GROUP = 31
export const MIDDLE_GROUP_COUNT = 8
export const MAX_GA_SUBADDRESS = 255

export const AddressSchema = z.object({
  // -1 means "not yet placed in a main group" (used by group templates)
  mainGroup: z.number().int(),
  middleGroup: z.number().int().min(0).max(MIDDLE_GROUP_COUNT - 1),
  ga: z.number().int().min(0).max(MAX_GA_SUBADDRESS),
})
export type Address = z.infer<typeof AddressSchema>

export const GASchema = z.object({
  id: z.string(),
  name: z.string(),
  address: AddressSchema,
  groupId: z.string().nullable(),
})
export type GA = z.infer<typeof GASchema>

export const GroupSchema = z.object({
  id: z.string(),
  name: z.string().min(1),
})
export type Group = z.infer<typeof GroupSchema>

export const GroupTemplateSchema = z.object({
  id: z.string(),
  name: z.string().min(1),
  subGroupNames: z.array(z.string()).length(MIDDLE_GROUP_COUNT),
  gas: z.array(GASchema),
})
export type GroupTemplate = z.infer<typeof GroupTemplateSchema>

export const MainGroupSchema = z.object({
  id: z.string(),
  name: z.string().min(1),
  subGroupNames: z.array(z.string()).length(MIDDLE_GROUP_COUNT),
  gas: z.array(GASchema),
  subAddress: z.number().int().min(MIN_MAIN_GROUP).max(MAX_MAIN_GROUP),
  defaultBlockLength: z.number().int().min(1),
})
export type MainGroup = z.infer<typeof MainGroupSchema>

export const ProjectSchema = z.object({
  created: z.string(),
  mainGroups: z.array(MainGroupSchema),
  groupTemplates: z.array(GroupTemplateSchema),
  groups: z.array(GroupSchema),
})
export type Project = z.infer<typeof ProjectSchema>

export function createMainGroupFormSchema(t: (key: string, vars?: Record<string, string | number>) => string) {
  return z.object({
    subAddress: z
      .number({ error: t('validation.addressMustBeNumber') })
      .int(t('validation.addressMustBeNumber'))
      .min(MIN_MAIN_GROUP, t('validation.addressInvalid', { min: MIN_MAIN_GROUP, max: MAX_MAIN_GROUP }))
      .max(MAX_MAIN_GROUP, t('validation.addressInvalid', { min: MIN_MAIN_GROUP, max: MAX_MAIN_GROUP })),
    name: z.string().min(1, t('validation.nameRequired')),
    defaultBlockLength: z
      .number({ error: t('validation.lengthMustBeNumber') })
      .int(t('validation.lengthMustBeNumber'))
      .min(1, t('validation.lengthMustBePositive')),
  })
}
export type MainGroupFormValues = z.infer<ReturnType<typeof createMainGroupFormSchema>>

export const NonEmptyTextSchema = z.string().min(1, 'Darf nicht leer sein')
