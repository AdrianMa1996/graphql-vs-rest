import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { canEditProjectGuard } from './can-edit-project.guard';

describe('canEditProjectGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => canEditProjectGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
