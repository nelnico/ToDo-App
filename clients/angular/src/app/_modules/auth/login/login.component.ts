import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  constructor(
    private accountService: AccountService,
    private router: Router
  ) {}

  loginForm = new FormGroup({
    emailAddress: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [Validators.required]),
  });

  onSubmit() {
    var controls = this.loginForm.controls;
    this.accountService
      .login(controls.emailAddress.value, controls.password.value)
      .subscribe({
        next: () => {
          this.accountService.currentUser$.subscribe();
          this.router.navigateByUrl('/');
        },
      });
  }

  setCreds(role:string) {
    if(role === 'user') {
      this.loginForm.controls.emailAddress.setValue('user@mysite.com');
      this.loginForm.controls.password.setValue('Password@123');
    } else if(role === 'admin'){
      this.loginForm.controls.emailAddress.setValue('admin@mysite.com');
      this.loginForm.controls.password.setValue('Password@123');
    }
    return false;
  }
}
