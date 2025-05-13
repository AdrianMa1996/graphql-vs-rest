import { Injectable } from '@angular/core';
import { GetCommentDto } from '../../../models/dtos/comment/responses/get-comment-dto/get-comment-dto';
import { CreateCommentDto } from '../../../models/dtos/comment/requests/create-comment-dto/create-comment-dto';
import { UpdateCommentDto } from '../../../models/dtos/comment/requests/update-comment-dto/update-comment-dto';

import { Apollo, gql } from 'apollo-angular';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommentApiService {

  constructor(private apollo: Apollo) { }

  getAllComments() : Observable<GetCommentDto[]> {
    return this.apollo.query<{ comments: GetCommentDto[] }>({
      query: gql`
        query {
          comments {
            commentID
            userID
            contributionID
            text
            date
          }
        }
      `,
    }).pipe(map(result => result.data.comments));
  }

  getCommentById(id: string) : Observable<GetCommentDto> {
    return this.apollo.query<{ commentById: GetCommentDto }>({
      query: gql`
        query CommentById($commentId: UUID!) {
          commentById(commentId: $commentId) {
            commentID
            userID
            contributionID
            text
            date
          }
        }
      `,
      variables: {
        commentId: id
      }
    }).pipe(map(result => result.data.commentById));
  }

  createComment(createCommentDto: CreateCommentDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation CreateComment($userID: UUID!, $contributionID: UUID!, $text: String!) {
          createComment(input: { userID: $userID, contributionID: $contributionID, text: $text })
        }
      `,
      variables: {
        userID: createCommentDto.userID,
        contributionID: createCommentDto.contributionID,
        text: createCommentDto.text,
      }
    }).pipe(map(result => result.data?.response));
  }

  updateComment(updateCommentDto: UpdateCommentDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation UpdateComment($commentID: UUID!, $userID: UUID!, $contributionID: UUID!, $text: String!) {
          updateComment(input: { commentID: $commentID, userID: $userID, contributionID: $contributionID, text: $text })
        }
      `,
      variables: {
        commentID: updateCommentDto.commentID,
        userID: updateCommentDto.userID,
        contributionID: updateCommentDto.contributionID,
        text: updateCommentDto.text
      }
    }).pipe(map(result => result.data?.response));
  }

  deleteCommentById(id: string): Observable<string | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation DeleteCommentById($commentId: UUID!) {
          deleteCommentById(commentId: $commentId)
        }
      `,
      variables: {
        commentId: id
      }
    }).pipe(map(result => result.data?.response));
  }
}
