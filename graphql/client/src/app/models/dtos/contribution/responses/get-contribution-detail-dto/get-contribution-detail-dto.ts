export class GetContributionDetailDto {
    constructor(
        public projectID: string = '',
        public userID: string = '',
        public category: string = '',
        public title: string = '',
        public text: string = '',
        public date: string = '',
        public status: string = '',
        public creator: CreatorDto = new CreatorDto('', ''),
        public votes: VoteDto[] = [],
        public comments: CommentDto[] = [],
        ) {}
}

export class CreatorDto {
    constructor(
      public name: string,
      public profilPicture: string,
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
      public text: string,
      public date: string,
      public creator: CreatorDto,
    ) {}
  }
