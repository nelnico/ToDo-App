import {
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
  } from '@angular/common/http';
  import { Injectable } from '@angular/core';
  import { Observable } from 'rxjs';
  import { take } from 'rxjs/operators';
  import { AccountService } from 'src/app/_services/account.service';
import { User } from '../_models/user';

  @Injectable()
  export class JwtInterceptor implements HttpInterceptor {
    constructor(private accountService: AccountService) {}

    intercept(
      request: HttpRequest<unknown>,
      next: HttpHandler
    ): Observable<HttpEvent<unknown>> {
      let currentUser: User | null = null;

      this.accountService.currentUser$
        .pipe(take(1))
        .subscribe((user) => (currentUser = user ? user : null));
      if (currentUser) {
        request = request.clone({
          setHeaders: {
            Authorization: `Bearer ${currentUser['token']}`,
          },
        });
      }

      return next.handle(request);
    }
  }
