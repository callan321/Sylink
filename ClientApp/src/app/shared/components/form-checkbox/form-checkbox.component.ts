import { Component, Input, input } from '@angular/core';

@Component({
  selector: 'app-form-checkbox',
  imports: [],
  templateUrl: './form-checkbox.component.html',
  standalone: true,
  styleUrl: './form-checkbox.component.scss',
})
export class FormCheckboxComponent {
  @Input({ required: true }) id!: string;
  @Input({ required: true }) name!: string;
  @Input({ required: true }) label!: string;
}
