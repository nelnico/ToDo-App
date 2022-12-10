import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {

  constructor(public accountService: AccountService) {}

  ngOnInit(): void {
    var storedData = localStorage.getItem('user');
    if (storedData) {
      var user: User = JSON.parse(storedData);
      if (user) {
        this.accountService.setCurrentUser(user);
      }
    }
  }

}
