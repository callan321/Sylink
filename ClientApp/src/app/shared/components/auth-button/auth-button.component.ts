import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-auth-button',
  imports: [],
  templateUrl: './auth-button.component.html',
  standalone: true,
  styleUrl: './auth-button.component.scss',
})
export class AuthButtonComponent {
  @Input({ required: true }) provider!: 'google' | 'github';
  @Input({ required: true }) href!: string;
  @Input() label: string = '';
}
