import { CanActivateFn } from '@angular/router';
import { Injectable, inject } from '@angular/core';
import { UserService } from '../user.service';

@Injectable({ providedIn: 'any' })
class AngularGuardService {

  constructor(private userService: UserService) { }

  canActivate(): boolean {
    const token = this.userService.getAccessToken();

    if (token) {
      return true;
    }
    return false;
  }
}

export const employeeGuard: CanActivateFn = (route, state) => {
  return inject(AngularGuardService).canActivate();
};
