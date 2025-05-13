import { Injectable } from '@angular/core';
import { LoginDto } from '../../../models/dtos/account/requests/login-dto/login-dto';

import { Apollo, gql } from 'apollo-angular';

@Injectable({
  providedIn: 'root'
})
export class AccountApiService {
  
  constructor(private apollo: Apollo) { }

  login(loginDto: LoginDto) {
    return this.apollo.mutate({
      mutation: gql`
        mutation Login($name: String!, $password: String!) {
          login(input: { name: $name, password: $password }) {
            token
          }
        }
      `,
      variables: {
        name: loginDto.name,
        password: loginDto.password
      }
    });
  }
}
