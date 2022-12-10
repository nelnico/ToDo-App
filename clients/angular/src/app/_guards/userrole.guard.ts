import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { User } from '../_models/user';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { NotificationService } from '../_services/notification.service';

@Injectable({
  providedIn: 'root',
})
export class UserRoleGuard implements CanActivate {
  constructor(
    private accountService: AccountService,
    private notificationService: NotificationService
  ) {}

  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map((user) => {
        if (!user) {
          this.notificationService.error(
            'Not Authorized',
            'You must be logged in to enter'
          );
          return false;
        }
        if (user.role !== 'User') {
          this.notificationService.error(
            'Not Authorized',
            'Only users can enter'
          );
          return false;
        }
        return true;
      })
    );
  }
}
