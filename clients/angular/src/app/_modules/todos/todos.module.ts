import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TodosRoutingModule } from './todos-routing.module';
import { TodosComponent } from './todos/todos.component';
import { SharedModule } from '../shared.module';
import { TodoItemDetailsComponent } from './todo-item-details/todo-item-details.component';
import { TodoItemEditComponent } from './todo-item-edit/todo-item-edit.component';


@NgModule({
  declarations: [
    TodosComponent,
    TodoItemDetailsComponent,
    TodoItemEditComponent
  ],
  imports: [
    CommonModule,
    TodosRoutingModule,
    SharedModule
  ]
})
export class TodosModule { }
