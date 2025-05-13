import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { UpdateUserDto } from '../../models/dtos/user/requests/update-user-dto/update-user-dto';
import { UserApiService } from '../../services/api/user-api/user-api.service';
import { FormsModule } from '@angular/forms';
import { GetUserWithoutProfilePictureDto } from '../../models/dtos/user/responses/get-user-without-profile-picture-dto/get-user-without-profile-picture-dto';
import { UpdateUserWithoutProfilePictureDto } from '../../models/dtos/user/requests/update-user-without-profile-picture-dto/update-user-without-profile-picture-dto';

@Component({
  selector: 'app-edit-user',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent, FormsModule],
  templateUrl: './edit-user.component.html',
  styleUrl: './edit-user.component.scss'
})
export class EditUserComponent {
  user: GetUserWithoutProfilePictureDto = new GetUserWithoutProfilePictureDto('', '', '', '', '');
  isAdmin: boolean = false;
  
    constructor(private route: ActivatedRoute, private userApiService: UserApiService, private router: Router) {}
  
    ngOnInit(): void {
      this.route.params.subscribe(params => {
        const id = params['userId'];
        if (id) {
          this.userApiService.getUserById(id).subscribe({
            next: (data: UpdateUserDto) => {
              this.user = data;
              this.isAdmin = this.user.role === 'Admin';
            },
            error: (error) => {
              window.alert('Ein Fehler ist aufgetreten: ' + error.message);
            }
          });
        }
      });
    }

    deleteUser() {
      this.userApiService.deleteUserById(this.user.userID).subscribe({
        next: (response) => {
          this.router.navigate(['']);
        },
        error: (error) => {
          window.alert('Ein Fehler ist aufgetreten: ' + error.message);
        }
      });
    }
  
    updateUser() {
      const updatedUserDto = new UpdateUserWithoutProfilePictureDto(
        this.user.userID,
        this.user.name,
        this.user.email,
        this.user.password,
        this.isAdmin ? 'Admin' : 'User'
      );
      this.userApiService.updateUserWithoutProfilePicture(updatedUserDto).subscribe({
        next: (response) => {
          this.router.navigate(['']);
        },
        error: (error) => {
          window.alert('Ein Fehler ist aufgetreten: ' + error.message);
        }
      });
    }
}
