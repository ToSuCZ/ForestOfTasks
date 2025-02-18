import { clsx, type ClassValue } from "clsx"
import { twMerge } from "tailwind-merge"

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export function capitalizeFirstLetter(string: string) {
  return string.charAt(0).toUpperCase() + string.slice(1)
}

// convert prisma object into regular js object
export function convertToPlainObject<T>(value: T): T {
  return JSON.parse(JSON.stringify(value))
}
