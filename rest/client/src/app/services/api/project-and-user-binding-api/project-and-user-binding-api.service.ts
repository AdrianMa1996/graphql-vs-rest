import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetProjectAndUserBindingDto } from '../../../models/dtos/project-and-user-binding/responses/get-project-and-user-binding-dto/get-project-and-user-binding-dto';
import { CreateProjectAndUserBindingDto } from '../../../models/dtos/project-and-user-binding/requests/create-project-and-user-binding-dto/create-project-and-user-binding-dto';
import { UpdateProjectAndUserBindingDto } from '../../../models/dtos/project-and-user-binding/requests/update-project-and-user-binding-dto/update-project-and-user-binding-dto';

@Injectable({
  providedIn: 'root'
})
export class ProjectAndUserBindingApiService {
  private apiUrl = 'https://localhost:7091';

  constructor(private http: HttpClient) { }
  
  getAllProjectAndUserBindings() {
    return this.http.get<GetProjectAndUserBindingDto[]>(`${this.apiUrl}/ProjectAndUserBinding`);
  }

  getProjectAndUserBindingById(id: string) {
    return this.http.get<GetProjectAndUserBindingDto>(`${this.apiUrl}/ProjectAndUserBinding/${id}`);
  }

  createProjectAndUserBinding(createProjectAndUserBindingDto: CreateProjectAndUserBindingDto) {
    return this.http.post(`${this.apiUrl}/ProjectAndUserBinding`, createProjectAndUserBindingDto, {
      responseType: 'text'
    });
  }

  updateProjectAndUserBinding(updateProjectAndUserBindingDto: UpdateProjectAndUserBindingDto) {
    return this.http.put(`${this.apiUrl}/ProjectAndUserBinding`, updateProjectAndUserBindingDto, {
      responseType: 'text'
    });
  }

  deleteProjectAndUserBindingById(id: string) {
    return this.http.delete(`${this.apiUrl}/ProjectAndUserBinding/${id}`, {
      responseType: 'text'
    });
  }
}
