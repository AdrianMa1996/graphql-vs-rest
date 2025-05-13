import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { AuthService } from '../../services/core/auth/auth.service';
import { UserApiService } from '../../services/api/user-api/user-api.service';
import { FormsModule } from '@angular/forms';
import { GetUserWithPasswordDto } from '../../models/dtos/user/responses/get-user-with-password-dto/get-user-with-password-dto';
import { PatchUserDto } from '../../models/dtos/user/requests/patch-user-dto/patch-user-dto';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent, FormsModule],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.scss'
})
export class EditProfileComponent {
  user: GetUserWithPasswordDto = new GetUserWithPasswordDto('', '', '', '', '', '');

  constructor(private authService: AuthService, private route: ActivatedRoute, private userApiService: UserApiService, private router: Router) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['userId'];
      if (id) {
        this.userApiService.getUserWithPasswordById(id).subscribe({
          next: (data: GetUserWithPasswordDto) => {
            this.user = data;
          },
          error: (error) => {
            window.alert('Ein Fehler ist aufgetreten: ' + error.message);
          }
        });
      }
    });
  }
  
  logout() {
    this.authService.logout();
    this.router.navigate(['']);
  }

  onFileSelected(event: any) {
    if (event.target.files && event.target.files[0]) {
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.user.profilPicture = e.target.result.split(',')[1];
      };
      reader.readAsDataURL(file);
    }
  }

  updateUser() {
    const patchUserDto = new PatchUserDto(
      this.user.userID,
      this.user.name || undefined,
      this.user.profilPicture || undefined,
      this.user.email || undefined,
      this.user.password || undefined,
      undefined
    );

    this.userApiService.patchUser(patchUserDto).subscribe({
      next: (response) => {
        this.router.navigate(['']);
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }
}
