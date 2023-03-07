import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/shared/services/auth/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  username!: string;
  email!: string;
  password!: string;

  ifRegisterFailed = false;

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.authService.register(this.username, this.password, this.email).subscribe({
      next: () => {
        this.router.navigate(['/login']);
      },
      error: err => {
        this.ifRegisterFailed=true;
      }
    }
    );
  }

}
