export class CreateContributionDto {
    constructor(
        public userID: string,
        public projectID: string,
        public category: string,
        public title: string,
        public text: string,
      ) {}
}
