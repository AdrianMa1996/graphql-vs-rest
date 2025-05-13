import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginDto } from '../../../models/dtos/account/requests/login-dto/login-dto';

@Injectable({
  providedIn: 'root'
})
export class AccountApiService {
  private apiUrl = 'https://localhost:7091';
  
    constructor(private http: HttpClient) { }

    login(loginDto: LoginDto) {
        return this.http.post<any>(`${this.apiUrl}/Account`, loginDto)
      }
}
