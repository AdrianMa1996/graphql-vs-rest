import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetUserDto } from '../../../models/dtos/user/responses/get-user-dto/get-user-dto';
import { CreateUserDto } from '../../../models/dtos/user/requests/create-user-dto/create-user-dto';
import { UpdateUserDto } from '../../../models/dtos/user/requests/update-user-dto/update-user-dto';
import { GetUserNameDto } from '../../../models/dtos/user/responses/get-user-name-dto/get-user-name-dto';
import { PatchUserDto } from '../../../models/dtos/user/requests/patch-user-dto/patch-user-dto';
import { GetUserWithPasswordDto } from '../../../models/dtos/user/responses/get-user-with-password-dto/get-user-with-password-dto';

@Injectable({
  providedIn: 'root'
})
export class UserApiService {
  private apiUrl = 'https://localhost:7091';
  
  constructor(private http: HttpClient) { }
  
  getAllUsers() {
    return this.http.get<GetUserDto[]>(`${this.apiUrl}/User`);
  }

  getUserById(id: string) {
    return this.http.get<GetUserDto>(`${this.apiUrl}/User/${id}`);
  }

  createUser(createUserDto: CreateUserDto) {
    return this.http.post(`${this.apiUrl}/User`, createUserDto, {
      responseType: 'text'
    });
  }

  updateUser(updateUserDto: UpdateUserDto) {
    return this.http.put(`${this.apiUrl}/User`, updateUserDto, {
      responseType: 'text'
    });
  }

  deleteUserById(id: string) {
    return this.http.delete(`${this.apiUrl}/User/${id}`, {
      responseType: 'text'
    });
  }
  
  getAllUsersName(){
    return this.http.get<GetUserNameDto[]>(`${this.apiUrl}/User/Name`);
  }

  getUserWithPasswordById(id: string){
    return this.http.get<GetUserWithPasswordDto>(`${this.apiUrl}/User/WithPassword/${id}`);
  }

  patchUser(patchUserDto: PatchUserDto) {
    return this.http.patch(`${this.apiUrl}/User`, patchUserDto, {
      responseType: 'text'
    });
  }  
}
