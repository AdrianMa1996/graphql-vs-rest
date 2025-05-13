import { TestBed } from '@angular/core/testing';

import { VoteApiService } from './vote-api.service';

describe('VoteApiService', () => {
  let service: VoteApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VoteApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
