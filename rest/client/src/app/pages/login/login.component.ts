import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { AuthService } from '../../services/core/auth/auth.service';
import { LoginDto } from '../../models/dtos/account/requests/login-dto/login-dto';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginDto: LoginDto = new LoginDto('', '');

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.authService.login(this.loginDto).subscribe({
      next: () => this.router.navigate(['']),
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }
}
