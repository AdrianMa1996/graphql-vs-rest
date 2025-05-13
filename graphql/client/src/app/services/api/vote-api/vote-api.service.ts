import { Injectable } from '@angular/core';
import { CreateVoteDto } from '../../../models/dtos/vote/requests/create-vote-dto/create-vote-dto';
import { UpdateVoteDto } from '../../../models/dtos/vote/requests/update-vote-dto/update-vote-dto';

import { Apollo, gql } from 'apollo-angular';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VoteApiService {

  constructor(private apollo: Apollo) { }

  createVote(createVoteDto: CreateVoteDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation CreateVote($userID: UUID!, $contributionID: UUID!) {
          createVote(input: { userID: $userID, contributionID: $contributionID })
        }
      `,
      variables: {
        userID: createVoteDto.userID,
        contributionID: createVoteDto.contributionID
      }
    }).pipe(map(result => result.data?.response));
  }

  updateVote(updateVoteDto: UpdateVoteDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation UpdateVote($voteID: UUID!, $userID: UUID!, $contributionID: UUID!) {
          updateVote(input: { voteID: $voteID, userID: $userID, contributionID: $contributionID })
        }
      `,
      variables: {
        voteID: updateVoteDto.voteID,
        userID: updateVoteDto.userID,
        contributionID: updateVoteDto.contributionID
      }
    }).pipe(map(result => result.data?.response));
  }

  deleteVoteById(id: string): Observable<string | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation DeleteVoteById($voteId: UUID!) {
          deleteVoteById(voteId: $voteId)
        }
      `,
      variables: {
        voteId: id
      }
    }).pipe(map(result => result.data?.response));
  }
}
