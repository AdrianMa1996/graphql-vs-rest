export class GetProjectAndUserBindingDto {
    constructor(
        public projectAndUserBindingID: string,
        public projectID: string,
        public userID: string,
      ) {}
}
