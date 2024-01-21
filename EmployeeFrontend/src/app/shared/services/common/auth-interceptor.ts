import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { UserService } from '../user.service';

export const authInterceptorServiceInterceptor: HttpInterceptorFn = (req, next) => {

  const userService = inject(UserService);

  const token = userService.getAccessToken();
  if (token) {
    const request = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`)
    });
    return next(request);
  }
  return next(req);
};
