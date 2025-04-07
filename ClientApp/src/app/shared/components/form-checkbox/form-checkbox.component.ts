import { Component, inject, Input, input } from '@angular/core';
import {
  ControlContainer,
  FormGroup,
  ReactiveFormsModule,
} from '@angular/forms';

@Component({
  selector: 'app-form-checkbox',
  imports: [ReactiveFormsModule],
  templateUrl: './form-checkbox.component.html',
  standalone: true,
  styleUrl: './form-checkbox.component.scss',
})
export class FormCheckboxComponent {
  @Input({ required: true }) id!: string;
  @Input({ required: true }) name!: string;
  @Input({ required: true }) label!: string;

  readonly controlContainer = inject(ControlContainer);

  get formGroup() {
    return this.controlContainer.control as FormGroup;
  }
}
