import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable, of } from 'rxjs';
import { TodoItemEditComponent } from '../_modules/todos/todo-item-edit/todo-item-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedTodoGuard implements CanDeactivate<TodoItemEditComponent> {
  constructor(private confirmService: ConfirmationService) {}

  canDeactivate(component: TodoItemEditComponent): Observable<boolean> {
    if (component.editForm?.dirty) {
      return confirm("Exit loosing unsaved changes")?
    }
    return of(true);
  }

}
