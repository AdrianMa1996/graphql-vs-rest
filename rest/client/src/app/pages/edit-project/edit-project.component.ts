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
import { GetProjectAndUserBindingDto } from '../../models/dtos/project-and-user-binding/responses/get-project-and-user-binding-dto/get-project-and-user-binding-dto';

@Component({
  selector: 'app-edit-project',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent, FormsModule],
  templateUrl: './edit-project.component.html',
  styleUrl: './edit-project.component.scss'
})
export class EditProjectComponent {
  project: UpdateProjectDto = new UpdateProjectDto('', '', '');
  users: GetUserDto[] = [];
  projectAndUserBindings: GetProjectAndUserBindingDto[] = [];
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
        this.loadUsers(projectId);
      }
    });
  }

  loadProject(projectId: string) {
    this.projectApiService.getProjectById(projectId).subscribe({
      next: (data: UpdateProjectDto) => {
        this.project = data;
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }

  loadUsers(projectId: string) {
    this.userApiService.getAllUsers().subscribe({
      next: (users: GetUserDto[]) => {
        this.users = users;
        this.loadDeveloperBindings(projectId);
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }

  loadDeveloperBindings(projectId: string) {
    this.projectAndUserBindingApiService.getAllProjectAndUserBindings().subscribe({
      next: (bindings: GetProjectAndUserBindingDto[]) => {
        this.projectAndUserBindings = bindings.filter(binding => binding.projectID === projectId);
        this.users.forEach(user => {
          this.developerBindings[user.userID] = false;
        });
        this.projectAndUserBindings.forEach(binding => {
          this.developerBindings[binding.userID] = true;
        });
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }

  updateProject() {
    this.projectApiService.updateProject(this.project).subscribe({
      next: () => {
        this.updateDeveloperBindings();
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }

  updateDeveloperBindings() {
    const projectId = this.project.projectID;

    this.users.forEach(user => {
      const existingBinding = this.projectAndUserBindings.find(binding => binding.userID === user.userID);

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
        this.project.logo = e.target.result.split(',')[1];
      };
      reader.readAsDataURL(file);
    }
  }

  deleteProject() {
    this.projectApiService.deleteProjectById(this.project.projectID).subscribe({
      next: (response) => {
        this.router.navigate(['']);
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }
}