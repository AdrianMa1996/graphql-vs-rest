import { Component} from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { FormsModule } from '@angular/forms';
import { CreateProjectDto } from '../../models/dtos/project/requests/create-project-dto/create-project-dto';
import { ProjectApiService } from '../../services/api/project-api/project-api.service';

@Component({
  selector: 'app-create-project',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent, FormsModule],
  templateUrl: './create-project.component.html',
  styleUrl: './create-project.component.scss'
})
export class CreateProjectComponent {
  projectLogo = "";
  projectName = "";

  constructor(public projectApiService: ProjectApiService, private router: Router){ }

  onFileSelected(event: any) {
    if (event.target.files && event.target.files[0]) {
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.projectLogo = e.target.result.split(',')[1];
      };

      reader.readAsDataURL(file);
    }
  }

  createProject() {
    const newProject: CreateProjectDto = {
      name: this.projectName,
      logo: this.projectLogo
    };

    this.projectApiService.createProject(newProject).subscribe({
      next: (response) => {
        this.router.navigate(['']);
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }
}
