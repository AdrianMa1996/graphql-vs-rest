import { Component } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { GetContributionOverviewDto } from '../../models/dtos/contribution/responses/get-contribution-overview-dto/get-contribution-overview-dto';
import { AuthService } from '../../services/core/auth/auth.service';
import { ContributionApiService } from '../../services/api/contribution-api/contribution-api.service';
import { VoteApiService } from '../../services/api/vote-api/vote-api.service';
import { UpdateContributionStatusDto } from '../../models/dtos/contribution/requests/update-contribution-status-dto/update-contribution-status-dto';

@Component({
  selector: 'app-ideas',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent, FormsModule, CommonModule],
  templateUrl: './ideas.component.html',
  styleUrl: './ideas.component.scss'
})
export class IdeasComponent {
  contributions:GetContributionOverviewDto[] = [  ];
  projectId: string = '';
  userId: string | null = null;
  statusSorter: string = 'newest';
  filterOpen: boolean = false;
  filterClosed: boolean = false;

  constructor(
    private authService: AuthService,
    public contributionApiService: ContributionApiService,
    public voteApiService: VoteApiService,
    private route: ActivatedRoute, 
  ){ }

  ngOnInit(): void {
    const tokenData = this.authService.decodeToken();
    this.userId = tokenData?.sub || null;

    this.route.params.subscribe(params => {
      this.projectId = params['projectId'];
      this.loadContributions();
    });
  }

  loadContributions(): void {
    this.contributionApiService
      .getAllContributionsOverview(this.statusSorter, this.projectId, 'idea', this.filterOpen, this.filterClosed)
      .subscribe({
        next: (contributions) => {
          this.contributions = contributions;
        },
        error: (error) => {
          window.alert('Ein Fehler ist aufgetreten: ' + error.message);
        }
      });
  }

  hasUserVoted(contribution: GetContributionOverviewDto): boolean {
    return contribution.votes.some(vote => vote.userID === this.userId);
  }

  toggleVote(contribution: GetContributionOverviewDto): void {
    const existingVote = contribution.votes.find(vote => vote.userID === this.userId);
  
    if (existingVote) {
      this.voteApiService.deleteVoteById(existingVote.voteID).subscribe({
        next: () => {
          this.loadContributions();
        },
        error: (error) => {
          window.alert('Ein Fehler ist aufgetreten: ' + error.message);
        }
      });
    } else {
      if (!this.userId) return;
      this.voteApiService.createVote({ userID: this.userId, contributionID: contribution.contributionID }).subscribe({
        next: () => {
          this.loadContributions();
        },
        error: (error) => {
          window.alert('Ein Fehler ist aufgetreten: ' + error.message);
        }
      });
    }
  }

  toggleContributionStatus(contribution: GetContributionOverviewDto): void {
    const updatedContributionStatusDto = new UpdateContributionStatusDto(
      contribution.contributionID,
      contribution.status === 'open' ? 'closed' : 'open'
    );
    this.contributionApiService.updateContributionStatus(updatedContributionStatusDto).subscribe({
      next: () => {
        this.loadContributions();
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }

  toggleSorter(value: 'newest' | 'oldest'): void {
    if (this.statusSorter === value) {
      this.statusSorter = 'none';
    } else {
      this.statusSorter = value;
    }
    this.loadContributions();
  }
}
