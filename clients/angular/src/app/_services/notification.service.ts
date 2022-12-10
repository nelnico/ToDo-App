import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  constructor(
    private toastr: ToastrService) {

  }
  success(title: string, message: string = '') {
    this.toastr.success(message,title);
  }
  info(title: string, message: string = '') {
    this.toastr.info(message,title);
  }
  warn(title: string, message: string = '') {
    this.toastr.warning(message,title);
  }
  error(title: string, message: string = '') {
    this.toastr.error(message,title);
  }
}
