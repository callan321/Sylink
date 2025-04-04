import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-email-sent',
  imports: [],
  templateUrl: './email-sent.component.html',
  standalone: true,
  styleUrl: './email-sent.component.scss',
})
export class EmailSentComponent implements OnInit {
  route = inject(ActivatedRoute);
  email!: string;

  ngOnInit(): void {
    this.email =
      this.route.snapshot.queryParamMap.get('email') ||
      'none, please contact us.';
  }
}
