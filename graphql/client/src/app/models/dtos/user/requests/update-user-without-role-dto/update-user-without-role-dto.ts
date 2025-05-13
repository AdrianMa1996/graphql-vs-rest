export class UpdateUserWithoutRoleDto {
    constructor(
        public userID: string,
        public name: string,
        public profilPicture: string,
        public email: string,
        public password: string,
      ) {}
}
