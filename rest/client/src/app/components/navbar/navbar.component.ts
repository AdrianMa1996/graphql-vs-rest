import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/core/auth/auth.service';
import { UserApiService } from '../../services/api/user-api/user-api.service';
import { GetUserDto } from '../../models/dtos/user/responses/get-user-dto/get-user-dto';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  userId: string | null = null;
  user: GetUserDto = new GetUserDto('', '', '', '', '');

  constructor(private authService: AuthService, private userApiService: UserApiService) {}

  ngOnInit(): void {
    const tokenData = this.authService.decodeToken();
    this.userId = tokenData?.sub || null;
      if (this.userId) {
        this.userApiService.getUserById(this.userId).subscribe({
          next: (data: GetUserDto) => {
            this.user = data;
          },
          error: (error) => {
            window.alert('Ein Fehler ist aufgetreten: ' + error.message);
          }
        });
      }
  }
}
