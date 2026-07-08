import { createContext, useContext, useEffect, useMemo, useState, type ReactNode } from 'react'
import de from './locales/de.json'
import en from './locales/en.json'
import { onHostLanguageChanged, requestSetLanguage } from '../host/wpfBridge'

export type Locale = 'de' | 'en'
export const LOCALES: Locale[] = ['de', 'en']

const dictionaries: Record<Locale, Record<string, unknown>> = { de, en }

const STORAGE_KEY = 'ga-manager:locale'

function isLocale(value: unknown): value is Locale {
  return value === 'de' || value === 'en'
}

function getByPath(obj: unknown, path: string): unknown {
  return path.split('.').reduce<unknown>((acc, part) => {
    if (acc && typeof acc === 'object' && part in acc) return (acc as Record<string, unknown>)[part]
    return undefined
  }, obj)
}

function interpolate(template: string, vars?: Record<string, string | number>): string {
  if (!vars) return template
  return template.replace(/\{\{(\w+)\}\}/g, (match, key) => (key in vars ? String(vars[key]) : match))
}

function loadStoredLocale(): Locale {
  if (typeof window === 'undefined') return 'de'
  const stored = window.localStorage.getItem(STORAGE_KEY)
  return isLocale(stored) ? stored : 'de'
}

export interface I18nContextValue {
  locale: Locale
  setLocale: (locale: Locale) => void
  t: (key: string, vars?: Record<string, string | number>) => string
}

const I18nContext = createContext<I18nContextValue | null>(null)

export function I18nProvider({ children }: { children: ReactNode }) {
  const [locale, setLocaleState] = useState<Locale>(loadStoredLocale)

  // When hosted in the WPF app, the language stored in its config.json is authoritative and
  // overrides the browser-only localStorage fallback used above for the initial render.
  useEffect(() => {
    onHostLanguageChanged((language) => {
      if (isLocale(language)) setLocaleState(language)
    })
  }, [])

  const value = useMemo<I18nContextValue>(() => {
    const dict = dictionaries[locale]
    return {
      locale,
      setLocale: (next) => {
        setLocaleState(next)
        window.localStorage.setItem(STORAGE_KEY, next)
        requestSetLanguage(next)
      },
      t: (key, vars) => {
        const raw = getByPath(dict, key)
        return typeof raw === 'string' ? interpolate(raw, vars) : key
      },
    }
  }, [locale])

  return <I18nContext.Provider value={value}>{children}</I18nContext.Provider>
}

export function useI18n(): I18nContextValue {
  const ctx = useContext(I18nContext)
  if (!ctx) throw new Error('useI18n must be used within an I18nProvider')
  return ctx
}
