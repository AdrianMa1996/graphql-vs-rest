import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { GetProjectDto } from '../../models/dtos/project/responses/get-project-dto/get-project-dto';
import { AuthService } from '../../services/core/auth/auth.service';
import { ProjectApiService } from '../../services/api/project-api/project-api.service';

@Component({
  selector: 'app-overview',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent],
  templateUrl: './overview.component.html',
  styleUrl: './overview.component.scss'
})
export class OverviewComponent {
  isAdmin = true;
  @Input() projects:GetProjectDto[] = [  ];

  constructor(public projectApiService: ProjectApiService, private authService: AuthService){ }

  ngOnInit(): void {
    this.isAdmin = this.authService.isAdmin();
    this.projectApiService.getAllProjects().subscribe(response => {
      this.projects = response;
    });
  }

  canEditProject(projectID: string): boolean {
    return this.authService.canEditProject(projectID);
  }
}
