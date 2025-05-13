import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetVoteDto } from '../../../models/dtos/vote/responses/get-vote-dto/get-vote-dto';
import { CreateVoteDto } from '../../../models/dtos/vote/requests/create-vote-dto/create-vote-dto';
import { UpdateVoteDto } from '../../../models/dtos/vote/requests/update-vote-dto/update-vote-dto';

@Injectable({
  providedIn: 'root'
})
export class VoteApiService {
  private apiUrl = 'https://localhost:7091';

  constructor(private http: HttpClient) { }

  getAllVotes() {
    return this.http.get<GetVoteDto[]>(`${this.apiUrl}/Vote`);
  }

  getVoteById(id: string) {
    return this.http.get<GetVoteDto>(`${this.apiUrl}/Vote/${id}`);
  }

  createVote(createVoteDto: CreateVoteDto) {
    return this.http.post(`${this.apiUrl}/Vote`, createVoteDto, {
      responseType: 'text'
    });
  }

  updateVote(updateVoteDto: UpdateVoteDto) {
    return this.http.put(`${this.apiUrl}/Vote`, updateVoteDto, {
      responseType: 'text'
    });
  }

  deleteVoteById(id: string) {
    return this.http.delete(`${this.apiUrl}/Vote/${id}`, {
      responseType: 'text'
    });
  }
}
