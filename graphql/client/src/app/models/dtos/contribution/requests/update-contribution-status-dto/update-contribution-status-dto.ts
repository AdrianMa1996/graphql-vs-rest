export class UpdateContributionStatusDto {
    constructor(
        public contributionID: string,
        public status: string,
      ) {}
}
