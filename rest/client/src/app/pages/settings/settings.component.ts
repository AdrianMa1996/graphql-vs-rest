import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { UserApiService } from '../../services/api/user-api/user-api.service';
import { GetUserNameDto } from '../../models/dtos/user/responses/get-user-name-dto/get-user-name-dto';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})
export class SettingsComponent {
  users:GetUserNameDto[] = [  ];

  constructor(public userApiService: UserApiService){ }

  ngOnInit(): void {
    this.userApiService.getAllUsersName().subscribe(response => {
      this.users = response;
    });
  }
}
