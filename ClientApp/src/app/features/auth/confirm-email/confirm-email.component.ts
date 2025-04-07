import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@core/data-access/auth.service';

@Component({
  selector: 'app-confirm-email',
  imports: [],
  templateUrl: './confirm-email.component.html',
  standalone: true,
  styleUrl: './confirm-email.component.scss',
})
export class ConfirmEmailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private authService = inject(AuthService);

  email!: string;
  token!: string;

  ngOnInit() {
    this.email = this.route.snapshot.queryParamMap.get('email') || '';
    this.token = this.route.snapshot.queryParamMap.get('token') || '';
    const payload = {
      email: this.email,
      token: this.token,
    };
    this.authService.confirmEmail(payload).subscribe({
      next: (result) => {
        console.log(result);
        this.router
          .navigate(['/auth/login'])
          .then((result) => console.log(result));
      },
      error: (error) => console.log(error),
    });
  }
}
