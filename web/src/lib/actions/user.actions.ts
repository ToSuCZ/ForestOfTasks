'use server';

import { signInFormSchema } from '@/lib/validators';
import { signIn, signOut } from '../../../auth';
import { isRedirectError } from 'next/dist/client/components/redirect-error';
import { ServerActionResult } from '@/types/server-action-result';

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
  } catch (error) {
    if (isRedirectError(error)) {
      throw error;
    }

    return { success: false, message: 'Invalid credentials' };
  }
}

export async function signOutUser(): Promise<void> {
  await signOut();
}
