import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { FormsModule } from '@angular/forms';
import { CreateContributionDto } from '../../models/dtos/contribution/requests/create-contribution-dto/create-contribution-dto';
import { AuthService } from '../../services/core/auth/auth.service';
import { ContributionApiService } from '../../services/api/contribution-api/contribution-api.service';

@Component({
  selector: 'app-create-idea',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent, FormsModule],
  templateUrl: './create-idea.component.html',
  styleUrl: './create-idea.component.scss'
})
export class CreateIdeaComponent {
  contribution: CreateContributionDto = new CreateContributionDto('', '', '', '', '');

  constructor(
    private route: ActivatedRoute, 
    private authService: AuthService,
    public contributionApiService: ContributionApiService, 
    private router: Router
  ){ }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.contribution.projectID = params['projectId'];
    });
    this.contribution.userID = this.authService.decodeToken()?.sub || null;
    this.contribution.category = 'idea';
  }

  createContribution() {
    this.contributionApiService.createContribution(this.contribution).subscribe({
      next: (response) => {
        this.router.navigate(['ideas', this.contribution.projectID]);
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }
}
