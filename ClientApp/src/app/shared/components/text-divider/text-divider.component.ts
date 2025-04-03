import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-text-divider',
  imports: [],
  templateUrl: './text-divider.component.html',
  standalone: true,
  styleUrl: './text-divider.component.scss',
})
export class TextDividerComponent {
  @Input({ required: true }) label!: string;
}
