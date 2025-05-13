import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/core/auth/auth.service';
import { UpdateUserDto } from '../../models/dtos/user/requests/update-user-dto/update-user-dto';
import { UserApiService } from '../../services/api/user-api/user-api.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  userId: string | null = null;
  user: UpdateUserDto = new UpdateUserDto('', '', '', '', '', '');

  constructor(private authService: AuthService, private userApiService: UserApiService) {}

  ngOnInit(): void {
    const tokenData = this.authService.decodeToken();
    this.userId = tokenData?.sub || null;
      if (this.userId) {
        this.userApiService.getUserById(this.userId).subscribe({
          next: (data: UpdateUserDto) => {
            this.user = data;
          },
          error: (error) => {
            window.alert('Ein Fehler ist aufgetreten: ' + error.message);
          }
        });
      }
  }
}
