import { Injectable } from '@angular/core';
import { GetContributionDto } from '../../../models/dtos/contribution/responses/get-contribution-dto/get-contribution-dto';
import { CreateContributionDto } from '../../../models/dtos/contribution/requests/create-contribution-dto/create-contribution-dto';
import { UpdateContributionDto } from '../../../models/dtos/contribution/requests/update-contribution-dto/update-contribution-dto';

import { Apollo, gql } from 'apollo-angular';
import { map, Observable } from 'rxjs';
import { GetContributionOverviewDto } from '../../../models/dtos/contribution/responses/get-contribution-overview-dto/get-contribution-overview-dto';
import { GetContributionDetailDto } from '../../../models/dtos/contribution/responses/get-contribution-detail-dto/get-contribution-detail-dto';
import { UpdateContributionStatusDto } from '../../../models/dtos/contribution/requests/update-contribution-status-dto/update-contribution-status-dto';

@Injectable({
  providedIn: 'root'
})
export class ContributionApiService {

  constructor(private apollo: Apollo) { }

  getAllContributions() : Observable<GetContributionDto[]> {
    return this.apollo.query<{ contributions: GetContributionDto[] }>({
      query: gql`
        query {
          contributions {
            contributionID
            projectID
            userID
            category
            title
            text
            date
            status
          }
        }
      `,
    }).pipe(map(result => result.data.contributions));
  }

  getContributionById(id: string) : Observable<GetContributionDto> {
    return this.apollo.query<{ contributionById: GetContributionDto }>({
      query: gql`
        query ContributionById($contributionId: UUID!) {
          contributionById(contributionId: $contributionId) {
            contributionID
            projectID
            userID
            category
            title
            text
            date
            status
          }
        }
      `,
      variables: {
        contributionId: id
      }
    }).pipe(map(result => result.data.contributionById));
  }

  createContribution(createContributionDto: CreateContributionDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation CreateContribution($userID: UUID!, $projectID: UUID!, $category: String!, $title: String!, $text: String!) {
          createContribution(input: { userID: $userID, projectID: $projectID, category: $category, title: $title, text: $text })
        }
      `,
      variables: {
        userID: createContributionDto.userID,
        projectID: createContributionDto.projectID,
        category: createContributionDto.category,
        title: createContributionDto.title,
        text: createContributionDto.text
      }
    }).pipe(map(result => result.data?.response));
  }

  updateContribution(updateContributionDto: UpdateContributionDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation UpdateContribution( $contributionID: UUID!, $projectID: UUID!, $userID: UUID!, $category: String!, $title: String!, $text: String!, $date: String!, $status: String!) {
          updateContribution(input: { contributionID: $contributionID, projectID: $projectID, userID: $userID, category: $category, title: $title, text: $text, date: $date, status: $status })
        }
      `,
      variables: {
        contributionID: updateContributionDto.contributionID,
        projectID: updateContributionDto.projectID,
        userID: updateContributionDto.userID,
        category: updateContributionDto.category,
        title: updateContributionDto.title,
        text: updateContributionDto.text,
        date: updateContributionDto.date,
        status: updateContributionDto.status,
      }
    }).pipe(map(result => result.data?.response));
  }

  updateContributionStatus(updateContributionStatusDto: UpdateContributionStatusDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation UpdateContribution( $contributionID: UUID!, $status: String!) {
          updateContribution(input: { contributionID: $contributionID, status: $status })
        }
      `,
      variables: {
        contributionID: updateContributionStatusDto.contributionID,
        status: updateContributionStatusDto.status,
      }
    }).pipe(map(result => result.data?.response));
  }

  deleteContributionById(id: string): Observable<string | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation DeleteContributionById($contributionId: UUID!) {
          deleteContributionById(contributionId: $contributionId)
        }
      `,
      variables: {
        contributionId: id
      }
    }).pipe(map(result => result.data?.response));
  }
  
  getAllContributionsOverview(
    order?: string,
    filterProjectId?: string,
    filterCategory?: string,
    filterOpen?: boolean,
    filterClosed?: boolean,
  ): Observable<GetContributionOverviewDto[]> {
    const filters: string[] = [];
  
    if (filterProjectId) {
      filters.push(`projectID: { eq: "${filterProjectId}" }`);
    }
  
    if (filterCategory) {
      filters.push(`category: { eq: "${filterCategory}" }`);
    }

    if (filterOpen && filterClosed) {
      filters.push(`status: { or: [{eq: "open" } {eq: "closed" }] }`);
    }
    else if (filterOpen) {
      filters.push(`status: { eq: "open" }`);
    }
    else if (filterClosed) {
      filters.push(`status: { eq: "closed" }`);
    }
  
    const whereClause = filters.length > 0 ? `where: { ${filters.join(', ')} }` : '';

    let orderClause = '';
    if (order === 'newest') {
      orderClause = `order: { date: DESC }`;
    } else if (order === 'oldest') {
      orderClause = `order: { date: ASC }`;
    }

    const args = [whereClause, orderClause].filter(Boolean).join(', ');
  
    const query = gql`
      query {
        contributions(${args}) {
          contributionID
          projectID
          userID
          category
          title
          text
          date
          status
          votes {
            voteID
            userID
          }
          comments {
            commentID
          }
        }
      }
    `;
  
    return this.apollo
      .query<{ contributions: GetContributionOverviewDto[] }>({
        query,
      })
      .pipe(map(result => result.data.contributions));
  }
  
  getContributionDetailById(id: string) : Observable<GetContributionDetailDto> {
    return this.apollo.query<{ contributionById: GetContributionDetailDto }>({
      query: gql`
        query ContributionById($contributionId: UUID!) {
          contributionById(contributionId: $contributionId) {
            projectID
            userID
            category
            title
            text
            date
            status
            creator{
              name
              profilPicture
            }
            votes{
              voteID
              userID
            }
            comments{
              text
              date
              creator{
                name
                profilPicture
              }
            }
          }
        }
      `,
      variables: {
        contributionId: id
      }
    }).pipe(map(result => result.data.contributionById));
  }
}
