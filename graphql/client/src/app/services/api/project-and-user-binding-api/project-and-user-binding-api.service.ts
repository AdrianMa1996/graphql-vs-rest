import { Injectable } from '@angular/core';
import { CreateProjectAndUserBindingDto } from '../../../models/dtos/project-and-user-binding/requests/create-project-and-user-binding-dto/create-project-and-user-binding-dto';
import { UpdateProjectAndUserBindingDto } from '../../../models/dtos/project-and-user-binding/requests/update-project-and-user-binding-dto/update-project-and-user-binding-dto';

import { Apollo, gql } from 'apollo-angular';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectAndUserBindingApiService {

  constructor(private apollo: Apollo) { }

  createProjectAndUserBinding(createProjectAndUserBindingDto: CreateProjectAndUserBindingDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation CreateProjectAndUserBinding($projectID: UUID!, $userID: UUID!) {
          createProjectAndUserBinding(input: { projectID: $projectID, userID: $userID })
        }
      `,
      variables: {
        projectID: createProjectAndUserBindingDto.projectID,
        userID: createProjectAndUserBindingDto.userID
      }
    }).pipe(map(result => result.data?.response));
  }

  updateProjectAndUserBinding(updateProjectAndUserBindingDto: UpdateProjectAndUserBindingDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation UpdateProjectAndUserBinding($projectAndUserBindingID: UUID!, $projectID: UUID!, $userID: UUID!) {
          updateProjectAndUserBinding(input: { projectAndUserBindingID: $projectAndUserBindingID, projectID: $projectID, userID: $userID })
        }
      `,
      variables: {
        projectAndUserBindingID: updateProjectAndUserBindingDto.projectAndUserBindingID,
        projectID: updateProjectAndUserBindingDto.projectID,
        userID: updateProjectAndUserBindingDto.userID
      }
    }).pipe(map(result => result.data?.response));
  }

  deleteProjectAndUserBindingById(id: string): Observable<string | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation DeleteProjectAndUserBindingById($projectAndUserBindingId: UUID!) {
          deleteProjectAndUserBindingById(projectAndUserBindingId: $projectAndUserBindingId)
        }
      `,
      variables: {
        projectAndUserBindingId: id
      }
    }).pipe(map(result => result.data?.response));
  }
}
