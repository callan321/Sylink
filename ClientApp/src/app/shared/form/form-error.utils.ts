export interface FormErrorResponse {
  errorMessage: string;
  fieldErrors: Record<string, string[]>;
}

export function getFormErrors(error: any): FormErrorResponse {
  const apiError = error?.error;

  if (apiError && typeof apiError === 'object') {
    return {
      errorMessage: apiError.title || 'An unexpected error occurred.',
      fieldErrors: apiError.errors || {},
    };
  }

  return {
    errorMessage: 'Something went wrong. Please try again.',
    fieldErrors: {},
  };
}
