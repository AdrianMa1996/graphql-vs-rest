export class UpdateProjectAndUserBindingDto {
    constructor(
        public projectAndUserBindingID: string,
        public projectID: string,
        public userID: string,
      ) {}
}
