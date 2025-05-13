export class CreateCommentDto {
    constructor(
        public userID: string,
        public contributionID: string,
        public text: string,
      ) {}
}
