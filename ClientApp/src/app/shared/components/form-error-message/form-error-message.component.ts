import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-form-error-message',
  imports: [],
  templateUrl: './form-error-message.component.html',
  standalone: true,
  styleUrl: './form-error-message.component.scss',
})
export class FormErrorMessageComponent {
  @Input({ required: true }) errorMessage!: string | null;
}
