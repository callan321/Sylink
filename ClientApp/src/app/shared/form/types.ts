import { FormControl, FormGroup } from '@angular/forms';

type TypedFormGroupControls<T> = {
  [K in keyof T]: FormControl<T[K]>;
};

export type TypedFormGroup<T> = FormGroup<TypedFormGroupControls<T>>;
