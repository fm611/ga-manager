import { useCallback, useRef } from 'react'

// Returns a function with a stable identity across renders that always invokes the latest
// `fn`, so memoized children only re-render when their own visual props change.
export function useStableCallback<A extends unknown[], R>(fn: (...args: A) => R): (...args: A) => R {
  const ref = useRef(fn)
  ref.current = fn
  return useCallback((...args: A) => ref.current(...args), [])
}
