import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@core/api-client';
import { VerifyEmailRequest } from '@core/api-client';
import { getAuthPath } from '@core/constants/app.routes';

@Component({
  selector: 'app-confirm-email',
  standalone: true,
  templateUrl: './confirm-email.component.html',
  styleUrl: './confirm-email.component.scss',
})
export class ConfirmEmailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private authService = inject(AuthService);

  ngOnInit() {
    const email = this.route.snapshot.queryParamMap.get('email') || '';
    const token = this.route.snapshot.queryParamMap.get('token') || '';
    const payload: VerifyEmailRequest = { email, token };
    this.authService.apiAuthConfirmEmailPost(payload).subscribe({
      next: () => {
        this.router.navigate([getAuthPath('login')]);
      },
      error: (err) => console.error(err),
    });
  }
}
