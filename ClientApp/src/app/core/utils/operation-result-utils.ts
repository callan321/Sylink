import { OperationResultOfUnit, FieldError, FieldName } from '@core/api-client/model/models';
import { FormGroup } from '@angular/forms';

export class OperationErrorExtractor {
  readonly message: string | null;
  readonly generalErrors: string[];
  readonly fieldErrors: Partial<Record<FieldName, string[]>>;

  constructor(response: OperationResultOfUnit) {
    const errors = response.errors ?? [];

    this.message = response.message ?? null;

    this.generalErrors = errors
      .filter(e => e.field === FieldName.General)
      .map(e => e.message);

    this.fieldErrors = errors
      .filter(e => e.field !== FieldName.General)
      .reduce((acc, curr) => {
        if (!acc[curr.field]) {
          acc[curr.field] = [];
        }
        acc[curr.field]!.push(curr.message);
        return acc;
      }, {} as Record<FieldName, string[]>);
  }

  /**
   * Returns errors for a specific field, or an empty array if none exist.
   */
  getFieldErrors(field: FieldName): string[] {
    return this.fieldErrors[field] ?? [];
  }

  /**
   * Optional: Applies field errors directly to a FormGroup.
   */
  applyToForm(form: FormGroup): void {
    Object.entries(this.fieldErrors).forEach(([field, messages]) => {
      const control = form.get(field);
      if (control && messages?.length) {
        control.setErrors({ server: messages });
      }
    });
  }

  /**
   * Optional: Combines message + generalErrors for display.
   */
  getCombinedErrorMessage(): string | null {
    return this.message || this.generalErrors.join(', ') || null;
  }
}
