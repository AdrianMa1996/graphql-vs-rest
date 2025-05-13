export class PatchContributionDto {
    constructor(
        public contributionID: string,
        public projectID?: string,
        public userID?: string,
        public category?: string,
        public title?: string,
        public text?: string,
        public date?: string,
        public status?: string,
      ) {}
}
