import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { AuthService } from '../../services/core/auth/auth.service';
import { UserApiService } from '../../services/api/user-api/user-api.service';
import { FormsModule } from '@angular/forms';
import { GetUserWithoutRoleDto } from '../../models/dtos/user/responses/get-user-without-role-dto/get-user-without-role-dto';
import { UpdateUserWithoutRoleDto } from '../../models/dtos/user/requests/update-user-without-role-dto/update-user-without-role-dto';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent, FormsModule],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.scss'
})
export class EditProfileComponent {
  user: GetUserWithoutRoleDto = new GetUserWithoutRoleDto('', '', '', '', '');

  constructor(private authService: AuthService, private route: ActivatedRoute, private userApiService: UserApiService, private router: Router) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['userId'];
      if (id) {
        this.userApiService.getGetUserWithoutRoleById(id).subscribe({
          next: (data: GetUserWithoutRoleDto) => {
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
    const updatedUserWithoutRoleDto = new UpdateUserWithoutRoleDto(
      this.user.userID,
      this.user.name,
      this.user.profilPicture,
      this.user.email,
      this.user.password,
    );
    this.userApiService.updateUserWithoutRole(updatedUserWithoutRoleDto).subscribe({
      next: (response) => {
        this.router.navigate(['']);
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }
}
