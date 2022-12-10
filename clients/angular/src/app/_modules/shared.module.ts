import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ToastrModule } from "ngx-toastr";

@NgModule({
    declarations: [],
    imports: [
      CommonModule,
      ToastrModule.forRoot({
        timeOut: 2000,
        positionClass: 'toast-bottom-right',
        preventDuplicates: true,
      }),
    ],
    exports: [
      ToastrModule,
    ]
  })
  export class SharedModule { }
