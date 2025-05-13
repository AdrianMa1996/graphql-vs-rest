export class UpdateUserWithoutProfilePictureDto {
    constructor(
        public userID: string,
        public name: string,
        public email: string,
        public password: string,
        public role: string,
      ) {}
}
