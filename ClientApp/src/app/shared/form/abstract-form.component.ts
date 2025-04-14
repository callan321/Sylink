import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { getFormErrors } from './form-error.utils';

export abstract class AbstractFormComponent<PayloadType = any> {
  protected fieldErrors: Record<string, string[]> = {};
  protected errorMessage: string = '';
  private isSubmitting = false;

  protected abstract getForm(): FormGroup;
  protected abstract buildPayload(): PayloadType;
  protected abstract onSubmitSuccess<T>(response: T): void;

  public getButtonDisabled(): boolean {
    return this.isSubmitting;
  }

  protected submitForm<T>(
    requestFn: (payload: PayloadType) => Observable<T>,
  ): void {
    const form = this.getForm();
    if (!this.isSubmitting) {
      this.isSubmitting = true;
      const payload = this.buildPayload();

      requestFn(payload).subscribe({
        next: (res) => {
          this.isSubmitting = false;
          this.resetErrors();
          this.onSubmitSuccess(res);
        },
        error: (err) => {
          this.isSubmitting = false;
          const { fieldErrors, errorMessage } = getFormErrors(err);
          this.fieldErrors = fieldErrors;
          this.errorMessage = errorMessage;
        },
      });
    } else {
      form.markAllAsTouched();
    }
  }

  protected resetErrors(): void {
    this.fieldErrors = {};
    this.errorMessage = '';
  }
}
