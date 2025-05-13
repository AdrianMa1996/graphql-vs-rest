import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../services/core/auth/auth.service';

export const canEditUserGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const routeUserId = route.paramMap.get('userId');

  if (routeUserId != null && authService.canEditUser(routeUserId)) {
    return true;
  } else {
    router.navigate(['/login']);
    return false;
  }
};
