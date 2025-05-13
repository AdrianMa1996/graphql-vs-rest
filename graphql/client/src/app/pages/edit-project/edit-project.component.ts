import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { FormsModule } from '@angular/forms';
import { UpdateProjectDto } from '../../models/dtos/project/requests/update-project-dto/update-project-dto';
import { ProjectApiService } from '../../services/api/project-api/project-api.service';
import { GetUserDto } from '../../models/dtos/user/responses/get-user-dto/get-user-dto';
import { UserApiService } from '../../services/api/user-api/user-api.service';
import { ProjectAndUserBindingApiService } from '../../services/api/project-and-user-binding-api/project-and-user-binding-api.service';
import { GetProjectWithDevelopersDto } from '../../models/dtos/project/responses/get-project-with-developers-dto/get-project-with-developers-dto';

@Component({
  selector: 'app-edit-project',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent, FormsModule],
  templateUrl: './edit-project.component.html',
  styleUrl: './edit-project.component.scss'
})
export class EditProjectComponent {
  projectWithDevelopers: GetProjectWithDevelopersDto = new GetProjectWithDevelopersDto('', '', '', []);
  users: GetUserDto[] = [];
  developerBindings: { [key: string]: boolean } = {};

  constructor(
    private route: ActivatedRoute, 
    private projectApiService: ProjectApiService,
    private userApiService: UserApiService,
    private projectAndUserBindingApiService: ProjectAndUserBindingApiService,
    private router: Router
  ){ }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const projectId = params['projectId'];
      if (projectId) {
        this.loadProject(projectId);
      }
    });
  }

  loadProject(projectId: string) {
    this.projectApiService.getProjectWithDevelopersById(projectId).subscribe({
      next: (data: GetProjectWithDevelopersDto) => {
        this.projectWithDevelopers = data;
        this.loadUsers();
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }

  loadUsers() {
    this.userApiService.getAllUsers().subscribe({
      next: (users: GetUserDto[]) => {
        this.users = users;
        this.loadDeveloperBindings();
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }

  loadDeveloperBindings() {
    this.users.forEach(user => {
      this.developerBindings[user.userID] = false;
    });
    this.projectWithDevelopers.projectAndUserBindings.forEach(binding => {
      this.developerBindings[binding.userID] = true;
    });
  }

  updateProject() {
    const project = new UpdateProjectDto(this.projectWithDevelopers.projectID, this.projectWithDevelopers.name, this.projectWithDevelopers.logo);

    this.projectApiService.updateProject(project).subscribe({
      next: () => {
        this.updateDeveloperBindings();
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }

  updateDeveloperBindings() {
    const projectId = this.projectWithDevelopers.projectID;

    this.users.forEach(user => {
      const existingBinding = this.projectWithDevelopers.projectAndUserBindings.find(binding => binding.userID === user.userID);

      if (this.developerBindings[user.userID]) {
        if (!existingBinding) {
          this.projectAndUserBindingApiService.createProjectAndUserBinding({ projectID: projectId, userID: user.userID }).subscribe();
        }
      } else {
        if (existingBinding) {
          this.projectAndUserBindingApiService.deleteProjectAndUserBindingById(existingBinding.projectAndUserBindingID).subscribe();
        }
      }
    });

    this.router.navigate(['']);
  }

  onFileSelected(event: any) {
    if (event.target.files && event.target.files[0]) {
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.projectWithDevelopers.logo = e.target.result.split(',')[1];
      };
      reader.readAsDataURL(file);
    }
  }

  deleteProject() {
    this.projectApiService.deleteProjectById(this.projectWithDevelopers.projectID).subscribe({
      next: (response) => {
        this.router.navigate(['']);
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }
}