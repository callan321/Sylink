import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-not-found',
  imports: [RouterLink],
  templateUrl: './not-found.component.html',
  standalone: true,
  styleUrl: './not-found.component.scss',
})
export class NotFoundComponent {}
