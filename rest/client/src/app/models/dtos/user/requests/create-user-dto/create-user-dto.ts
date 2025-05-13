export class CreateUserDto {
    constructor(
        public name: string,
        public profilPicture: string,
        public email: string,
        public password: string,
        public role: string,
      ) {}
}
