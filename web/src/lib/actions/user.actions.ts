'use server';

import { signInFormSchema, signUpFormSchema } from '@/lib/validators';
import { signIn, signOut } from '../../../auth';
import { isRedirectError } from 'next/dist/client/components/redirect-error';
import { ServerActionResult } from '@/types/server-action-result';
import { prisma } from '@/db/prisma';
import { hashSync } from 'bcrypt-ts-edge';

export async function signInWithCredentials(
  prevState: unknown,
  formData: FormData,
): Promise<ServerActionResult> {
  try {
    const inputData = signInFormSchema.safeParse({
      email: formData.get('email'),
      password: formData.get('password'),
    });

    if (!inputData.success) {
      return {
        success: false,
        message: 'Invalid data provided',
        validationErrors: inputData.error.errors.map(e => e.message),
      };
    }

    await signIn('credentials', inputData.data);

    return { success: true, message: 'Sign in successful.' };
  } catch (e) {
    if (isRedirectError(e)) {
      throw e;
    }

    return { success: false, message: 'Invalid credentials' };
  }
}

export async function signOutUser(): Promise<void> {
  await signOut();
}

export async function signUpUser(
    prevState: unknown,
    formData: FormData,
): Promise<ServerActionResult> {
  try {
    const inputData = signUpFormSchema.safeParse({
      name: formData.get('name'),
      email: formData.get('email'),
      password: formData.get('password'),
      confirmPassword: formData.get('confirmPassword'),
    });

    if (!inputData.success) {
      return {
        success: false,
        message: 'Invalid data provided',
        validationErrors: inputData.error.errors.map(e => e.message),
      };
    }
    
    await prisma.user.create({
      data: {
        name: inputData.data.name,
        email: inputData.data.email,
        password: hashSync(inputData.data.password, 10),
      }
    });

    await signIn('credentials', {
      email: inputData.data.email,
      password: inputData.data.password,
    });
    
    return { success: true, message: 'Signed up successfully.' };
  } catch (e) {
    if (isRedirectError(e)) {
      throw e;
    }
    return { success: false, message: 'User was not registered' };
  }
}
