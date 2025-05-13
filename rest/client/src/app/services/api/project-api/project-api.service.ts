import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GetProjectDto } from '../../../models/dtos/project/responses/get-project-dto/get-project-dto';
import { CreateProjectDto } from '../../../models/dtos/project/requests/create-project-dto/create-project-dto';
import { UpdateProjectDto } from '../../../models/dtos/project/requests/update-project-dto/update-project-dto';

@Injectable({
  providedIn: 'root'
})
export class ProjectApiService {
  private apiUrl = 'https://localhost:7091';

  constructor(private http: HttpClient) { }

  getAllProjects() {
    return this.http.get<GetProjectDto[]>(`${this.apiUrl}/Project`);
  }

  getProjectById(id: string) {
    return this.http.get<GetProjectDto>(`${this.apiUrl}/Project/${id}`);
  }

  createProject(createProjectDto: CreateProjectDto) {
    return this.http.post(`${this.apiUrl}/Project`, createProjectDto, {
      responseType: 'text'
    });
  }

  updateProject(updateProjectDto: UpdateProjectDto) {
    return this.http.put(`${this.apiUrl}/Project`, updateProjectDto, {
      responseType: 'text'
    });
  }

  deleteProjectById(id: string) {
    return this.http.delete(`${this.apiUrl}/Project/${id}`, {
      responseType: 'text'
    });
  }
}

