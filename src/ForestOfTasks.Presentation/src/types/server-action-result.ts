export type ServerActionResult = {
  success: boolean;
  message: string;
  validationErrors?: string[];
};

export const DefaultActionResult: ServerActionResult = {
  success: false,
  message: '',
};
