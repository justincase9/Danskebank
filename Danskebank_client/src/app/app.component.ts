import { Component } from '@angular/core';
import { AuthService } from 'src/shared/services/auth/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(public authService: AuthService) {}

  logout() {
    this.authService.logout();
    window.location.href = '/login';

  }
}
