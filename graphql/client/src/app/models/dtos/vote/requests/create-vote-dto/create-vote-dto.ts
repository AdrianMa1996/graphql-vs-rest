export class CreateVoteDto {
    constructor(
        public userID: string,
        public contributionID: string,
      ) {}
}
