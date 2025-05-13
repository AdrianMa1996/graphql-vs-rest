export class GetContributionOverviewDto {
    constructor(
        public contributionID: string,
        public projectID: string,
        public userID: string,
        public category: string,
        public title: string,
        public text: string,
        public date: string,
        public status: string,
        public votes: VoteDto[],
        public comments: CommentDto[],
      ) {}
}

export class VoteDto {
  constructor(
    public voteID: string,
    public userID: string,
  ) {}
}

export class CommentDto {
  constructor(
    public commentID: string,
  ) {}
}

