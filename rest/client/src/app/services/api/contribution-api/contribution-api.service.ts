import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetContributionDto } from '../../../models/dtos/contribution/responses/get-contribution-dto/get-contribution-dto';
import { CreateContributionDto } from '../../../models/dtos/contribution/requests/create-contribution-dto/create-contribution-dto';
import { UpdateContributionDto } from '../../../models/dtos/contribution/requests/update-contribution-dto/update-contribution-dto';
import { GetContributionOverviewDto } from '../../../models/dtos/contribution/responses/get-contribution-overview-dto/get-contribution-overview-dto';
import { PatchContributionDto } from '../../../models/dtos/contribution/requests/patch-contribution-dto/patch-contribution-dto';
import { GetContributionDetailDto } from '../../../models/dtos/contribution/responses/get-contribution-detail-dto/get-contribution-detail-dto';

@Injectable({
  providedIn: 'root'
})
export class ContributionApiService {
  private apiUrl = 'https://localhost:7091';

  constructor(private http: HttpClient) { }

  getAllContributions() {
    return this.http.get<GetContributionDto[]>(`${this.apiUrl}/Contribution`);
  }

  getContributionById(id: string) {
    return this.http.get<GetContributionDto>(`${this.apiUrl}/Contribution/${id}`);
  }

  createContribution(createContributionDto: CreateContributionDto) {
    return this.http.post(`${this.apiUrl}/Contribution`, createContributionDto, {
      responseType: 'text'
    });
  }

  updateContribution(updateContributionDto: UpdateContributionDto) {
    return this.http.put(`${this.apiUrl}/Contribution`, updateContributionDto, {
      responseType: 'text'
    });
  }

  deleteContributionById(id: string) {
    return this.http.delete(`${this.apiUrl}/Contribution/${id}`, {
      responseType: 'text'
    });
  }

  getAllContributionOverviews() {
    return this.http.get<GetContributionOverviewDto[]>(`${this.apiUrl}/Contribution/overview`);
  }

  getFilteredAndSortedContributionOverviews(projectId?: string, category?: string, status?: string, orderByDate?: string) {
    const params = new URLSearchParams();

    if (projectId) params.append('projectId', projectId);
    if (category) params.append('category', category);
    if (status) params.append('status', status);
    if (orderByDate) params.append('orderByDate', orderByDate);
  
    return this.http.get<GetContributionOverviewDto[]>(`${this.apiUrl}/Contribution/overview?${params.toString()}`);
  }

  getContributionDetailById(id: string) {
    return this.http.get<GetContributionDetailDto>(`${this.apiUrl}/Contribution/detail/${id}`);
  }
  
  patchContribution(patchContributionDto: PatchContributionDto) {
    return this.http.patch(`${this.apiUrl}/Contribution`, patchContributionDto, {
      responseType: 'text'
    });
  }  
}
