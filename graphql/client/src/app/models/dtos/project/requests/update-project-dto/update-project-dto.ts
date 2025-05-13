export class UpdateProjectDto {
    constructor(
        public projectID: string,
        public name: string,
        public logo: string,
      ) {}
}
