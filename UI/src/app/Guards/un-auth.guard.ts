import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
export const unAuthGuard: CanActivateFn = (route, state) => {
  const token=localStorage.getItem('token');
  const router= inject(Router);
  console.log('token',token);
  if(!token)
  {
    return true;
  }
  else{
    router.navigate(['/home']);
    return false;
}
};
