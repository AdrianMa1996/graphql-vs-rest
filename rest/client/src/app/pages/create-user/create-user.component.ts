import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { CreateUserDto } from '../../models/dtos/user/requests/create-user-dto/create-user-dto';
import { UserApiService } from '../../services/api/user-api/user-api.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-create-user',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent, FormsModule],
  templateUrl: './create-user.component.html',
  styleUrl: './create-user.component.scss'
})
export class CreateUserComponent {
  user: CreateUserDto = new CreateUserDto('', '', '', '', '');
  isAdmin: boolean = false;

  constructor(public userApiService: UserApiService, private router: Router){ }

  createUser() {
    this.user.role = this.isAdmin ? 'Admin' : 'User';
    this.userApiService.createUser(this.user).subscribe({
      next: (response) => {
        this.router.navigate(['']);
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }
}
