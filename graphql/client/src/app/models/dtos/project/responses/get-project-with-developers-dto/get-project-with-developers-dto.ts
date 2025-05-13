export class GetProjectWithDevelopersDto {
    constructor(
        public projectID: string,
        public name: string,
        public logo: string,
        public projectAndUserBindings: DeveloperDto[],
      ) {}
}

export class DeveloperDto {
  constructor(
    public projectAndUserBindingID: string,
    public projectID: string,
    public userID: string,
  ) {}
}
