import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthService } from '../Services/Auth/auth.service';
import { inject } from '@angular/core';
import {  lastValueFrom } from 'rxjs';

export const activateGuard: CanActivateFn = async (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  var authService = inject(AuthService);
  var router = inject(Router);
  var token = sessionStorage.getItem('token');

  if (token) {
    try {
      const isTokenValid = await lastValueFrom(
        authService.validateToken(token)
      );
      if (isTokenValid.valid) {
        return true;
      } else {
        router.navigate(['/login']);
        return false;
      }
    } catch {
      router.navigate(['/login']);
      return false;
    }
  } else {
    router.navigate(['/login']);
    return false;
  }
};
