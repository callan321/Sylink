import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-form-submit-button',
  imports: [],
  templateUrl: './form-submit-button.component.html',
  standalone: true,
  styleUrl: './form-submit-button.component.scss',
})
export class FormSubmitButtonComponent {
  @Input({ required: true }) label!: string;
  @Input({ required: true }) disabled!: boolean;
  @Input() loadingLabel: string = 'Submitting...';
}
