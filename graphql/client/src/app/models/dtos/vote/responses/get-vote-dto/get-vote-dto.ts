export class GetVoteDto {
    constructor(
        public voteID: string,
        public userID: string,
        public contributionID: string,
      ) {}
}
