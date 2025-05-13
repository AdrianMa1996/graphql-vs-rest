export class GetUserDto {
    constructor(
        public userID: string,
        public name: string,
        public profilPicture: string,
        public email: string,
        public role: string,
      ) {}
}
