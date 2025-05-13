import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { canEditUserGuard } from './can-edit-user.guard';

describe('canEditUserGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => canEditUserGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
