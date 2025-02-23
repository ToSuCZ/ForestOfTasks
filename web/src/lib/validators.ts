import { z } from 'zod';

export const productInsertSchema = z.object({
  name: z.string().min(3).max(255),
  slug: z.string().min(3).max(255),
  category: z.string().min(3).max(255),
  brand: z.string().min(3).max(255),
  description: z.string().min(3).max(255),
  stock: z.coerce.number(),
  images: z.array(z.string()).min(1),
  isFeatured: z.boolean(),
  banner: z.string().nullable(),
  rating: z.string(),
  numReviews: z.number(),
  price: z.string(),
});

export const signInFormSchema = z.object({
  email: z.string().email('Invalid email address'),
  password: z.string().min(6, 'Password must be at least 6 characters'),
});

export const signUpFormSchema = z.object({
  name: z.string().min(3, 'Name must be at least 3 characters long'),
  email: z.string().email('Invalid email address'),
  password: z.string().min(6, 'Password must be at least 6 characters'),
  confirmPassword: z.string().min(6, 'Confirm Password must be at least 6 characters'),
}).refine((data) => data.password === data.confirmPassword, {
  message: "Passwords don't match",
  path: ['confirmPassword'],
});