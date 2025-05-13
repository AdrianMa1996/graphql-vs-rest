import { Component } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { ContributionApiService } from '../../services/api/contribution-api/contribution-api.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/core/auth/auth.service';
import { VoteApiService } from '../../services/api/vote-api/vote-api.service';
import { CommentDto, GetContributionOverviewDto, VoteDto } from '../../models/dtos/contribution/responses/get-contribution-overview-dto/get-contribution-overview-dto';
import { CommentApiService } from '../../services/api/comment-api/comment-api.service';
import { forkJoin } from 'rxjs';
import { PatchContributionDto } from '../../models/dtos/contribution/requests/patch-contribution-dto/patch-contribution-dto';

@Component({
  selector: 'app-discussions',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent, FormsModule, CommonModule],
  templateUrl: './discussions.component.html',
  styleUrl: './discussions.component.scss'
})
export class DiscussionsComponent {
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
    public commentApiService: CommentApiService,
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
    // this.loadContributionsWithUnoptimizedEndpoints();
    // this.loadContributionsWithOptimizedEndpoint();
    this.loadFilteredAndSortedContributionsWithOptimizedEndpoint();
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
    const patchContributionDto = new PatchContributionDto(
      contribution.contributionID,
      undefined,
      undefined,
      undefined,
      undefined,
      undefined,
      undefined,
      contribution.status === 'open' ? 'closed' : 'open'
    );
    this.contributionApiService.patchContribution(patchContributionDto).subscribe({
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

  loadContributionsWithUnoptimizedEndpoints(): void {
    forkJoin({
      contributions: this.contributionApiService.getAllContributions(),
      votes: this.voteApiService.getAllVotes(),
      comments: this.commentApiService.getAllComments()
    }).subscribe({
      next: ({ contributions, votes, comments }) => {
        this.contributions = contributions.map(c => {
          const contributionVotes = votes.filter(v => v.contributionID === c.contributionID);
          const contributionComments = comments.filter(cm => cm.contributionID === c.contributionID);
          return new GetContributionOverviewDto(
            c.contributionID,
            c.projectID,
            c.userID,
            c.category,
            c.title,
            c.text,
            c.date,
            c.status,
            contributionVotes.map(v => new VoteDto(v.voteID, v.userID)),
            contributionComments.map(cm => new CommentDto(cm.commentID))
          );
        });

        this.contributions = this.contributions.filter(c => c.projectID === this.projectId);

        this.contributions = this.contributions.filter(c => c.category === 'discussion');

        if (this.filterOpen && !this.filterClosed) {
          this.contributions = this.contributions.filter(c => c.status === 'open');
        } else if (!this.filterOpen && this.filterClosed) {
          this.contributions = this.contributions.filter(c => c.status === 'closed');
        }

        if (this.statusSorter === 'newest') {
          this.contributions.sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime());
        } else if (this.statusSorter === 'oldest') {
          this.contributions.sort((a, b) => new Date(a.date).getTime() - new Date(b.date).getTime());
        }
      },
      error: (error) => {
        console.error('Fehler beim Laden der Daten:', error);
        window.alert('Ein Fehler ist beim Laden der Beiträge aufgetreten.');
      }
    });
  }

  loadContributionsWithOptimizedEndpoint(): void {
    this.contributionApiService.getAllContributionOverviews().subscribe({
      next: (overviews) => {
        this.contributions = overviews;

        this.contributions = this.contributions.filter(c => c.projectID === this.projectId);

        this.contributions = this.contributions.filter(c => c.category === 'discussion');

        if (this.filterOpen && !this.filterClosed) {
          this.contributions = this.contributions.filter(c => c.status === 'open');
        } else if (!this.filterOpen && this.filterClosed) {
          this.contributions = this.contributions.filter(c => c.status === 'closed');
        }

        if (this.statusSorter === 'newest') {
          this.contributions.sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime());
        } else if (this.statusSorter === 'oldest') {
          this.contributions.sort((a, b) => new Date(a.date).getTime() - new Date(b.date).getTime());
        }
      },
      error: (error) => {
        console.error('Fehler beim Laden der Overview-Daten:', error);
        window.alert('Ein Fehler ist beim Laden der Beiträge aufgetreten.');
      }
    });
  }

  loadFilteredAndSortedContributionsWithOptimizedEndpoint(): void {
    let status: string | undefined = undefined;
    if (this.filterOpen && !this.filterClosed) {
      status = 'open';
    } else if (!this.filterOpen && this.filterClosed) {
      status = 'closed';
    }
    
    let orderByDate: string | undefined = undefined;
    if (this.statusSorter === 'newest') {
      orderByDate = 'DESC';
    } else if (this.statusSorter === 'oldest') {
      orderByDate = 'ASC';
    }
    
    this.contributionApiService.getFilteredAndSortedContributionOverviews(
      this.projectId,
      'discussion',
      status, 
      orderByDate
    ).subscribe({
      next: (data) => {
        this.contributions = data;
      },
      error: (err) => {
        console.error('Fehler beim Laden:', err);
        window.alert('Ein Fehler ist beim Laden der Beiträge aufgetreten.');
      }
    });
  }
}
