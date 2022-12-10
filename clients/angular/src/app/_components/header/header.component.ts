import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {

  constructor(public accountService: AccountService, private router: Router) {}

  onAddTodo() {
    alert('add a todo item (emit event)');
  }

  onLogout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
    return false;
  }
}
