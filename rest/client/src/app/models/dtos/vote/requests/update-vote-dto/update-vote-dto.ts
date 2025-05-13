export class UpdateVoteDto {
    constructor(
        public voteID: string,
        public userID: string,
        public contributionID: string,
      ) {}
}
