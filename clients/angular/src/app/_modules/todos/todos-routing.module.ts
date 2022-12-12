import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TodoItemDetailsComponent } from './todo-item-details/todo-item-details.component';
import { TodoItemEditComponent } from './todo-item-edit/todo-item-edit.component';
import { TodosComponent } from './todos/todos.component';

const routes: Routes = [
  {
    path: '',
    component: TodosComponent
  },
  {
    path: ':id',
    component: TodoItemDetailsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TodosRoutingModule { }
