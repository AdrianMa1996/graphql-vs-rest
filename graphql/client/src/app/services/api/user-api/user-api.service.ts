import { Injectable } from '@angular/core';
import { GetUserDto } from '../../../models/dtos/user/responses/get-user-dto/get-user-dto';
import { CreateUserDto } from '../../../models/dtos/user/requests/create-user-dto/create-user-dto';
import { UpdateUserDto } from '../../../models/dtos/user/requests/update-user-dto/update-user-dto';

import { Apollo, gql } from 'apollo-angular';
import { map, Observable } from 'rxjs';
import { GetUserNameDto } from '../../../models/dtos/user/responses/get-user-name-dto/get-user-name-dto';
import { GetUserWithoutProfilePictureDto } from '../../../models/dtos/user/responses/get-user-without-profile-picture-dto/get-user-without-profile-picture-dto';
import { UpdateUserWithoutProfilePictureDto } from '../../../models/dtos/user/requests/update-user-without-profile-picture-dto/update-user-without-profile-picture-dto';
import { GetUserWithoutRoleDto } from '../../../models/dtos/user/responses/get-user-without-role-dto/get-user-without-role-dto';
import { UpdateUserWithoutRoleDto } from '../../../models/dtos/user/requests/update-user-without-role-dto/update-user-without-role-dto';

@Injectable({
  providedIn: 'root'
})
export class UserApiService {
  
  constructor(private apollo: Apollo) { }

  getAllUsers() : Observable<GetUserDto[]> {
    return this.apollo.query<{ users: GetUserDto[] }>({
      query: gql`
        query {
          users {
            userID
            name
            profilPicture
            email
            password
            role
          }
        }
      `,
    }).pipe(map(result => result.data.users));
  }

  getAllUsersName() : Observable<GetUserNameDto[]> {
    return this.apollo.query<{ users: GetUserNameDto[] }>({
      query: gql`
        query {
          users {
            userID
            name
          }
        }
      `,
    }).pipe(map(result => result.data.users));
  }

  getUserById(id: string): Observable<GetUserDto> {
    return this.apollo.query<{ userById: GetUserDto }>({
      query: gql`
        query UserById($userId: UUID!) {
          userById(userId: $userId) {
            userID
            name
            profilPicture
            email
            password
            role
          }
        }
      `,
      variables: {
        userId: id
      }
    }).pipe(map(result => result.data.userById));
  }

  getUserWithoutProfilePictureById(id: string): Observable<GetUserWithoutProfilePictureDto> {
    return this.apollo.query<{ userById: GetUserWithoutProfilePictureDto }>({
      query: gql`
        query UserById($userId: UUID!) {
          userById(userId: $userId) {
            userID
            name
            email
            password
            role
          }
        }
      `,
      variables: {
        userId: id
      }
    }).pipe(map(result => result.data.userById));
  }

  getGetUserWithoutRoleById(id: string): Observable<GetUserWithoutRoleDto> {
    return this.apollo.query<{ userById: GetUserWithoutRoleDto }>({
      query: gql`
        query UserById($userId: UUID!) {
          userById(userId: $userId) {
            userID
            name
            profilPicture
            email
            password
          }
        }
      `,
      variables: {
        userId: id
      }
    }).pipe(map(result => result.data.userById));
  }

  createUser(createUserDto: CreateUserDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation CreateUser($name: String!, $profilPicture: String!, $email: String!, $password: String!, $role: String!) {
          createUser(input: { name: $name, profilPicture: $profilPicture, email: $email, password: $password, role: $role })
        }
      `,
      variables: {
        name: createUserDto.name,
        profilPicture: createUserDto.profilPicture,
        email: createUserDto.email,
        password: createUserDto.password,
        role: createUserDto.role,
      }
    }).pipe(map(result => result.data?.response));
  }

  updateUser(updateUserDto: UpdateUserDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation UpdateUser($userID: UUID!, $name: String!, $profilPicture: String!, $email: String!, $password: String!, $role: String!) {
          updateUser(input: { userID: $userID, name: $name, profilPicture: $profilPicture, email: $email, password: $password, role: $role })
        }
      `,
      variables: {
        userID: updateUserDto.userID,
        name: updateUserDto.name,
        profilPicture: updateUserDto.profilPicture,
        email: updateUserDto.email,
        password: updateUserDto.password,
        role: updateUserDto.role,
      }
    }).pipe(map(result => result.data?.response));
  }

  updateUserWithoutProfilePicture(updateUserDto: UpdateUserWithoutProfilePictureDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation UpdateUser($userID: UUID!, $name: String!, $email: String!, $password: String!, $role: String!) {
          updateUser(input: { userID: $userID, name: $name, email: $email, password: $password, role: $role })
        }
      `,
      variables: {
        userID: updateUserDto.userID,
        name: updateUserDto.name,
        email: updateUserDto.email,
        password: updateUserDto.password,
        role: updateUserDto.role,
      }
    }).pipe(map(result => result.data?.response));
  }

  updateUserWithoutRole(updateUserWithoutRoleDto: UpdateUserWithoutRoleDto) : Observable<string  | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation UpdateUser($userID: UUID!, $name: String!, $profilPicture: String!, $email: String!, $password: String!) {
          updateUser(input: { userID: $userID, name: $name, profilPicture: $profilPicture, email: $email, password: $password })
        }
      `,
      variables: {
        userID: updateUserWithoutRoleDto.userID,
        name: updateUserWithoutRoleDto.name,
        profilPicture: updateUserWithoutRoleDto.profilPicture,
        email: updateUserWithoutRoleDto.email,
        password: updateUserWithoutRoleDto.password,
      }
    }).pipe(map(result => result.data?.response));
  }

  deleteUserById(id: string): Observable<string | undefined> {
    return this.apollo.mutate<{ response: string }>({
      mutation: gql`
        mutation DeleteUserById($userId: UUID!) {
          deleteUserById(userId: $userId)
        }
      `,
      variables: {
        userId: id
      }
    }).pipe(map(result => result.data?.response));
  }
}
