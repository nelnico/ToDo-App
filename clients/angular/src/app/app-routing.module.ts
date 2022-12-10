import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { HomeComponent } from './home/home.component';
import { AdminRoleGuard } from './_guards/adminrole.guard';
import { AuthGuard } from './_guards/auth.guard';
import { UserRoleGuard } from './_guards/userrole.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'about',
    component: AboutComponent
  },
  {
    path: 'profile',
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('./_modules/profile/profile.module').then(
        (m) => m.ProfileModule
      ),
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./_modules/auth/auth.module').then(
        (m) => m.AuthModule
      ),
  },
  {
    path: 'todos',
    canActivate: [UserRoleGuard],
    loadChildren: () =>
      import('./_modules/todos/todos.module').then(
        (m) => m.TodosModule
      ),
  },
  {
    path: 'users',
    canActivate: [AdminRoleGuard],
    loadChildren: () =>
      import('./_modules/users/users.module').then(
        (m) => m.UsersModule
      ),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
