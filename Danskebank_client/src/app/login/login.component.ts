import { Component } from '@angular/core';
import { AuthService } from 'src/shared/services/auth/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  username!: string;
  password!: string;

  isLoginFailed = false;

  constructor(private authService: AuthService) {}

  onSubmit() {
    this.authService.login(this.username, this.password).subscribe({
      next: () => {
        window.location.href = '/products';
      },
      error: err => {
        this.isLoginFailed=true;
      }
    });
  }
}
