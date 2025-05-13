import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { LoginDto } from '../../../models/dtos/account/requests/login-dto/login-dto';
import { AccountApiService } from '../../api/account-api/account-api.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(public accountApiService: AccountApiService) {}

  login(loginDto: LoginDto): Observable<any> {
    return this.accountApiService.login(loginDto).pipe(
      tap((response: any) => {
        const token = response?.data?.login?.token;
        if (token) {
          localStorage.setItem('token', token);
        }
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  isAdmin(): boolean {
    const decodedToken = this.decodeToken();
    return decodedToken?.Role === 'Admin';
  }

  canEditProject(projectID: string): boolean {
    if (this.isAdmin()){
      return true;
    }
    const decodedToken = this.decodeToken();
    const allowedProjectIds: string[] = decodedToken?.CanEditProject || [];
    return allowedProjectIds.includes(projectID!);
  }

  canEditUser(userID: string): boolean {
    if (this.isAdmin()){
      return true;
    }
    const decodedToken = this.decodeToken();
    const allowedUserId = decodedToken?.CanEditUser;
    return allowedUserId === userID;
  }

  decodeToken(): any {
    const token = localStorage.getItem('token');
    if (!token) return null;
  
    try {
      const payload = token.split('.')[1];
      return JSON.parse(atob(payload));
    } catch (e) {
      return null;
    }
  }
}
