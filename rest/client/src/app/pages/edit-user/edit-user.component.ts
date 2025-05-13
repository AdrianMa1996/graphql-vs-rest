import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { AuthService } from '../../services/core/auth/auth.service';
import { UserApiService } from '../../services/api/user-api/user-api.service';
import { FormsModule } from '@angular/forms';
import { PatchUserDto } from '../../models/dtos/user/requests/patch-user-dto/patch-user-dto';
import { GetUserWithPasswordDto } from '../../models/dtos/user/responses/get-user-with-password-dto/get-user-with-password-dto';

@Component({
  selector: 'app-edit-user',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent, FormsModule],
  templateUrl: './edit-user.component.html',
  styleUrl: './edit-user.component.scss'
})
export class EditUserComponent {
  user: GetUserWithPasswordDto = new GetUserWithPasswordDto('', '', '', '', '', '');
  isAdmin: boolean = false;
  
    constructor(private authService: AuthService, private route: ActivatedRoute, private userApiService: UserApiService, private router: Router) {}
  
    ngOnInit(): void {
      this.route.params.subscribe(params => {
        const id = params['userId'];
        if (id) {
          this.userApiService.getUserWithPasswordById(id).subscribe({
            next: (data: GetUserWithPasswordDto) => {
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
      const patchUserDto = new PatchUserDto(
        this.user.userID,
        this.user.name || undefined,
        undefined,
        this.user.email || undefined,
        this.user.password || undefined,
        this.isAdmin ? 'Admin' : 'User'
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
