import {booleanAttribute, Component, inject, Input, OnInit} from '@angular/core';
import {
  ControlContainer,
  FormGroup,
  ReactiveFormsModule,
} from '@angular/forms';

@Component({
  selector: 'app-form-input-group',
  imports: [ReactiveFormsModule],
  templateUrl: './form-input-group.component.html',
  standalone: true,
  styleUrl: './form-input-group.component.scss',
})
export class FormInputGroupComponent {
  @Input({ required: true }) id!: string;
  @Input({ required: true }) label!: string;
  @Input() type: string = 'text';
  @Input() autocomplete?: string;
  @Input({ transform: booleanAttribute }) required: boolean = false;
  @Input() fieldErrors: string[] = [];

  readonly controlContainer = inject(ControlContainer);

  get formGroup() {
    return this.controlContainer.control as FormGroup;
  }

}
