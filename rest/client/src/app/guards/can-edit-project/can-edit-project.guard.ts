import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../services/core/auth/auth.service';
import { inject } from '@angular/core';

export const canEditProjectGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const routeProjectId = route.paramMap.get('projectId');

  if (routeProjectId != null && authService.canEditProject(routeProjectId)) {
    return true;
  } else {
    router.navigate(['/login']);
    return false;
  }
};
