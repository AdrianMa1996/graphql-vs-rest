import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { CreateContributionDto } from '../../models/dtos/contribution/requests/create-contribution-dto/create-contribution-dto';
import { ContributionApiService } from '../../services/api/contribution-api/contribution-api.service';
import { AuthService } from '../../services/core/auth/auth.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-create-discussion',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent, FormsModule],
  templateUrl: './create-discussion.component.html',
  styleUrl: './create-discussion.component.scss'
})
export class CreateDiscussionComponent {
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
    this.contribution.category = 'discussion';
  }

  createContribution() {
    this.contributionApiService.createContribution(this.contribution).subscribe({
      next: (response) => {
        this.router.navigate(['discussions', this.contribution.projectID]);
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }
}
