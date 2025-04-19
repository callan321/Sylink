import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { inject } from '@angular/core';
import { ExtractedErrors, ApiErrorService } from '@core/services/api-error.service';
import { OperationResultOfUnit } from '@core/api-client/model/models';


export abstract class AbstractFormComponent<PayloadType = any> {
  private readonly apiErrorService = inject(ApiErrorService);
  private isSubmitting = false;

  protected extractedErrors: ExtractedErrors = this.apiErrorService.emptyErrors();

  protected abstract getForm(): FormGroup;
  protected abstract buildPayload(): PayloadType;
  protected abstract onSubmitSuccess<T>(response: T): void;

  /**
   * Public getter to use in templates to disable buttons when submitting.
   */
  public getButtonDisabled(): boolean {
    return this.isSubmitting;
  }

  /**
   * Public getter to show a general error message above the form.
   */
  public get errorMessage(): string | null {
    return (
      this.extractedErrors.message ||
      this.extractedErrors.generalErrors.join(', ') ||
      null
    );
  }

  /**
   * Submits a form with the given API request function.
   */
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

          const response: OperationResultOfUnit = err.error;
          this.extractedErrors = this.apiErrorService.extract(response);
          console.log("abstract form component", this.extractedErrors );
          this.apiErrorService.applyToFormGroup(form, this.extractedErrors);
        },
      });
    } else {
      form.markAllAsTouched();
    }
  }

  /**
   * Clears any stored error messages.
   */
  protected resetErrors(): void {
    this.extractedErrors = this.apiErrorService.emptyErrors();
  }
}
