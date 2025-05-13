export class GetCommentDto {
    constructor(
        public commentID: string,
        public userID: string,
        public contributionID: string,
        public text: string,
        public date: string,
      ) {}
}
