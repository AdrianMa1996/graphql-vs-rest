import { Component } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { ContributionApiService } from '../../services/api/contribution-api/contribution-api.service';
import { CommentApiService } from '../../services/api/comment-api/comment-api.service';
import { CreateCommentDto } from '../../models/dtos/comment/requests/create-comment-dto/create-comment-dto';
import { AuthService } from '../../services/core/auth/auth.service';
import { FormsModule } from '@angular/forms';
import { GetContributionDetailDto } from '../../models/dtos/contribution/responses/get-contribution-detail-dto/get-contribution-detail-dto';
import { VoteApiService } from '../../services/api/vote-api/vote-api.service';
import { UpdateContributionStatusDto } from '../../models/dtos/contribution/requests/update-contribution-status-dto/update-contribution-status-dto';

@Component({
  selector: 'app-discussion',
  standalone: true,
  imports: [RouterModule, NavbarComponent, FooterComponent, FormsModule],
  templateUrl: './discussion.component.html',
  styleUrl: './discussion.component.scss'
})
export class DiscussionComponent {
  userId: string | null = null;
  contributionId: string = '';
  contribution: GetContributionDetailDto = new GetContributionDetailDto();
  comment: CreateCommentDto = new CreateCommentDto('', '', '');

  constructor(
    private route: ActivatedRoute, 
    private authService: AuthService,
    private contributionApiService: ContributionApiService,
    private commentApiService: CommentApiService,
    public voteApiService: VoteApiService
  ){ }


  ngOnInit(): void {
    const tokenData = this.authService.decodeToken();
    this.userId = tokenData?.sub || null;

    this.route.params.subscribe(params => {
      const discussionId = params['discussionId'];
      if (discussionId) {
        this.contributionId = discussionId;
        this.loadContribution();
      }
    });
  }

  loadContribution() {
    this.contributionApiService.getContributionDetailById(this.contributionId).subscribe({
      next: (data: GetContributionDetailDto) => {
        this.contribution = data;
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }

  hasUserVoted(): boolean {
    return this.contribution.votes.some(vote => vote.userID === this.userId);
  }

  toggleVote(): void {
    const existingVote = this.contribution.votes.find(vote => vote.userID === this.userId);
  
    if (existingVote) {
      this.voteApiService.deleteVoteById(existingVote.voteID).subscribe({
        next: () => {
          this.loadContribution();
        },
        error: (error) => {
          window.alert('Ein Fehler ist aufgetreten: ' + error.message);
        }
      });
    } else {
      if (!this.userId) return;
      this.voteApiService.createVote({ userID: this.userId, contributionID: this.contributionId }).subscribe({
        next: () => {
          this.loadContribution();
        },
        error: (error) => {
          window.alert('Ein Fehler ist aufgetreten: ' + error.message);
        }
      });
    }
  }

  toggleContributionStatus(): void {
    const updatedContributionStatusDto = new UpdateContributionStatusDto(
      this.contributionId,
      this.contribution.status === 'open' ? 'closed' : 'open'
    );
    this.contributionApiService.updateContributionStatus(updatedContributionStatusDto).subscribe({
      next: () => {
        this.loadContribution();
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }

  createComment() {
    this.comment.contributionID = this.contributionId;
    this.comment.userID = this.authService.decodeToken()?.sub || null;
    this.commentApiService.createComment(this.comment).subscribe({
      next: (response) => {
        this.comment = new CreateCommentDto('', '', '');
        this.loadContribution();
      },
      error: (error) => {
        window.alert('Ein Fehler ist aufgetreten: ' + error.message);
      }
    });
  }
}
