import {
  booleanAttribute,
  Component,
  Input,
  inject,
  computed,
  signal,
} from '@angular/core';
import {
  ControlContainer,
  FormGroup,
  FormGroupDirective,
  ReactiveFormsModule,
} from '@angular/forms';

@Component({
  selector: 'app-form-input-group',
  standalone: true,
  templateUrl: './form-input-group.component.html',
  styleUrl: './form-input-group.component.scss',
  imports: [ReactiveFormsModule],
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }],
})
export class FormInputGroupComponent {
  @Input({ required: true }) id!: string;
  @Input({ required: true }) label!: string;
  @Input() type: string = 'text';
  @Input() autocomplete?: string;
  @Input({ transform: booleanAttribute }) required: boolean = false;

  readonly controlContainer = inject(ControlContainer);

  get formGroup(): FormGroup {
    return this.controlContainer.control as FormGroup;
  }

  get control() {
    return this.formGroup.get(this.id);
  }

  get errorMessages(): string[] {
    const control = this.control;
    const errors = control?.errors ?? {};
    const messages: string[] = [];

    // Only show errors if the field has been touched or dirty
    if (control && (control.touched || control.dirty)) {
      for (const key of Object.keys(errors)) {
        const value = errors[key];
        if (Array.isArray(value)) {
          messages.push(...value); // server-side or multi-messages
        } else if (typeof value === 'string') {
          messages.push(value); // simple error
        } else {
          messages.push('Invalid value.'); // generic fallback
        }
      }
    }

    return messages;
  }
}
