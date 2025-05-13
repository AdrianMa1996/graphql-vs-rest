import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetCommentDto } from '../../../models/dtos/comment/responses/get-comment-dto/get-comment-dto';
import { CreateCommentDto } from '../../../models/dtos/comment/requests/create-comment-dto/create-comment-dto';
import { UpdateCommentDto } from '../../../models/dtos/comment/requests/update-comment-dto/update-comment-dto';

@Injectable({
  providedIn: 'root'
})
export class CommentApiService {
  private apiUrl = 'https://localhost:7091';

  constructor(private http: HttpClient) { }

  getAllComments() {
    return this.http.get<GetCommentDto[]>(`${this.apiUrl}/Comment`);
  }

  getCommentById(id: string) {
    return this.http.get<GetCommentDto>(`${this.apiUrl}/Comment/${id}`);
  }

  createComment(createCommentDto: CreateCommentDto) {
    return this.http.post(`${this.apiUrl}/Comment`, createCommentDto, {
      responseType: 'text'
    });
  }

  updateComment(updateCommentDto: UpdateCommentDto) {
    return this.http.put(`${this.apiUrl}/Comment`, updateCommentDto, {
      responseType: 'text'
    });
  }

  deleteCommentById(id: string) {
    return this.http.delete(`${this.apiUrl}/Comment/${id}`, {
      responseType: 'text'
    });
  }
}
