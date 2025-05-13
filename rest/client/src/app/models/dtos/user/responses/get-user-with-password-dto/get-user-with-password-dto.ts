export class GetUserWithPasswordDto {
    constructor(
        public userID: string,
        public name: string,
        public profilPicture: string,
        public email: string,
        public password: string,
        public role: string,
      ) {}
}
