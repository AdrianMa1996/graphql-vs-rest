import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/core/auth/auth.service';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent {
  isAdmin = true;

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.isAdmin = this.authService.isAdmin();
  }
}
