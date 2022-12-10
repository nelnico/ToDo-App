import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;

  private currentUserSource = new ReplaySubject<User | undefined>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(
    private http: HttpClient,
    private notifications: NotificationService
  ) {}

  login(emailAddress: string, password: string) {
    var url = this.baseUrl + 'auth/login';
    var data = {
      emailAddress: emailAddress,
      password: password,
    };
    return this.http.post<User>(url, data).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.notifications.info('', `Welcome back ${user.username}!`);
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    let userName = '';
    var storedData = localStorage.getItem('user');
    if (storedData) {
      var user: User = JSON.parse(storedData);
      if (user) {
        userName = user.name;
      }
    }
    let message = 'See you soon';
    if (userName) {
      message += ` ${userName}!`;
    } else {
      message += '!';
    }
    this.notifications.warn('', message);
    localStorage.removeItem('user');
    this.currentUserSource.next(undefined);
  }
}
