import { Injectable } from '@angular/core';
import { GetProjectDto } from '../../../models/dtos/project/responses/get-project-dto/get-project-dto';
import { CreateProjectDto } from '../../../models/dtos/project/requests/create-project-dto/create-project-dto';
import { UpdateProjectDto } from '../../../models/dtos/project/requests/update-project-dto/update-project-dto';

import { Apollo, gql } from 'apollo-angular';
import { map, Observable } from 'rxjs';
import { GetProjectWithDevelopersDto } from '../../../models/dtos/project/responses/get-project-with-developers-dto/get-project-with-developers-dto';

@Injectable({
  providedIn: 'root'
})
export class ProjectApiService {

  constructor(private apollo: Apollo) { }

  getAllProjects() : Observable<GetProjectDto[]> {
    return this.apollo.query<{ projects: GetProjectDto[] }>({
      query: gql`
        query {
          projects {
            projectID
            name
            logo
          }
        }
      `,
    }).pipe(map(result => result.data.projects));
  }

  getProjectById(id: string) : Observable<GetProjectDto> {
    return this.apollo.query<{ projectById: GetProjectDto }>({
      query: gql`
        query ProjectById($projectId: UUID!) {
          projectById(projectId: $projectId) {
            projectID
            name
            logo
          }
        }
      `,
      variables: {
        projectId: id
      }
    }).pipe(map(result => result.data.projectById));
  }

  createProject(createProjectDto: CreateProjectDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation CreateProject($name: String!, $logo: String!) {
          createProject(input: { name: $name, logo: $logo })
        }
      `,
      variables: {
        name: createProjectDto.name,
        logo: createProjectDto.logo
      }
    }).pipe(map(result => result.data?.response));
  }

  updateProject(updateProjectDto: UpdateProjectDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation UpdateProject($projectID: UUID!, $name: String!, $logo: String!) {
          updateProject(input: { projectID: $projectID, name: $name, logo: $logo })
        }
      `,
      variables: {
        projectID: updateProjectDto.projectID,
        name: updateProjectDto.name,
        logo: updateProjectDto.logo
      }
    }).pipe(map(result => result.data?.response));
  }

  deleteProjectById(id: string): Observable<string | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation DeleteProjectById($projectId: UUID!) {
          deleteProjectById(projectId: $projectId)
        }
      `,
      variables: {
        projectId: id
      }
    }).pipe(map(result => result.data?.response));
  }

  getProjectWithDevelopersById(id: string) : Observable<GetProjectWithDevelopersDto> {
    return this.apollo.query<{ projectById: GetProjectWithDevelopersDto }>({
      query: gql`
        query ProjectById($projectId: UUID!) {
          projectById(projectId: $projectId) {
            projectID
            name
            logo
            projectAndUserBindings{
              projectAndUserBindingID
              projectID
              userID
            }
          }
        }
      `,
      variables: {
        projectId: id
      }
    }).pipe(map(result => result.data.projectById));
  }
}

