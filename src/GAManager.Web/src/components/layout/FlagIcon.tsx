import type { Locale } from '../../i18n/I18nContext'

const SIZE = { width: 20, height: 14 }

function GermanyFlag() {
  return (
    <svg {...SIZE} viewBox="0 0 5 3" aria-hidden="true" style={{ display: 'block', borderRadius: '2px' }}>
      <rect width="5" height="1" y="0" fill="#000000" />
      <rect width="5" height="1" y="1" fill="#DD0000" />
      <rect width="5" height="1" y="2" fill="#FFCE00" />
    </svg>
  )
}

function UkFlag() {
  return (
    <svg width={21} height={14} viewBox="0 0 30 20" aria-hidden="true" style={{ display: 'block', borderRadius: '2px' }}>
      <rect width="30" height="20" fill="#00247D" />
      <path d="M0,0 L30,20 M30,0 L0,20" stroke="#FFFFFF" strokeWidth="4" />
      <path d="M0,0 L13.5,9 M16.5,11 L30,20" stroke="#CF142B" strokeWidth="1.4" />
      <path d="M30,0 L16.5,9 M13.5,11 L0,20" stroke="#CF142B" strokeWidth="1.4" />
      <rect x="12" y="0" width="6" height="20" fill="#FFFFFF" />
      <rect x="0" y="7" width="30" height="6" fill="#FFFFFF" />
      <rect x="13.5" y="0" width="3" height="20" fill="#CF142B" />
      <rect x="0" y="8.5" width="30" height="3" fill="#CF142B" />
    </svg>
  )
}

export function FlagIcon({ locale }: { locale: Locale }) {
  return locale === 'de' ? <GermanyFlag /> : <UkFlag />
}
