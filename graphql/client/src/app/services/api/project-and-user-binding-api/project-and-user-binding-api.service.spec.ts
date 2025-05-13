import { TestBed } from '@angular/core/testing';

import { ProjectAndUserBindingApiService } from './project-and-user-binding-api.service';

describe('ProjectAndUserBindingApiService', () => {
  let service: ProjectAndUserBindingApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProjectAndUserBindingApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
