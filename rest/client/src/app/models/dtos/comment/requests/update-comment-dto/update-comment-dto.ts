export class UpdateCommentDto {
    constructor(
        public commentID: string,
        public userID: string,
        public contributionID: string,
        public text: string,
      ) {}
}
