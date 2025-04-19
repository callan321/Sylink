import { Injectable } from '@angular/core';
import { OperationResultOfUnit, FieldName } from '@core/api-client/model/models';
import { FormGroup } from '@angular/forms';

export interface ExtractedErrors {
  message: string | null;
  generalErrors: string[];
  fieldErrors: Record<FieldName, string[]>; // all fields guaranteed
}

@Injectable({ providedIn: 'root' })
export class ApiErrorService {
  private readonly allFieldNames: FieldName[] = Object.values(FieldName);

  extract(response: OperationResultOfUnit): ExtractedErrors {
    const message = response.message ?? null;
    const errors = response.errors ?? [];

    const generalErrors = errors
      .filter(e => e.field === FieldName.General)
      .map(e => e.message);

    const fieldErrors = this.allFieldNames.reduce((acc, field) => {
      acc[field] = [];
      return acc;
    }, {} as Record<FieldName, string[]>);

    errors
      .filter(e => e.field !== FieldName.General)
      .forEach((e) => {
        fieldErrors[e.field].push(e.message);
      });

    return {
      message,
      generalErrors,
      fieldErrors
    };
  }

  applyToFormGroup(form: FormGroup, errors: ExtractedErrors): void {
    Object.entries(errors.fieldErrors).forEach(([field, messages]) => {
      const control = form.get(field);
      if (control && messages.length) {
        control.setErrors({ server: messages });
      }
    });
  }

  emptyErrors(): ExtractedErrors {
    const fieldErrors = this.allFieldNames.reduce((acc, field) => {
      acc[field] = [];
      return acc;
    }, {} as Record<FieldName, string[]>);

    return {
      message: null,
      generalErrors: [],
      fieldErrors
    };
  }
}
