import { Component, OnInit } from '@angular/core';
import { PaginatedResult } from 'src/app/_models/pagination/paginated-result.model';
import { Pagination } from 'src/app/_models/pagination/pagination.model';
import { TodoItem } from 'src/app/_models/todo-item';
import { TodoSearchParams } from 'src/app/_models/todo-search-params';
import { TodoService } from 'src/app/_services/todo.service';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.scss'],
})
export class TodosComponent implements OnInit {

  isEditing: boolean = false;
  editToDo: TodoItem;

  model: TodoItem[] = [];
  pagination: Pagination;
  searchParams: TodoSearchParams = new TodoSearchParams();

  constructor(private todoService: TodoService) { }

  ngOnInit(): void {
    this.findTodos();
  }

  findTodos() {
    this.todoService
      .getTodoItems(this.searchParams)
      .subscribe((result: PaginatedResult<TodoItem[]>) => {
        console.log(result);
        this.pagination = result.pagination;
        this.model = result.result;
      });
  }

  onDelete(todoItem: TodoItem) {
    if (confirm(`Are you sure you want to delete ${todoItem.title}`)) {
      this.todoService.deleteTodoItem(todoItem.id).subscribe({
        next: () => {
        },
        error: (error) => {
          alert(error);
        },
        complete: () => {
          this.findTodos();
        },
      });
    }
    return false;
  }

  onEdit(todoItem: TodoItem) {
    this.editToDo = todoItem;
    this.isEditing = true;
    return false;
  }

  onCancelEdit() {
    this.editToDo = null;
    this.isEditing = false;
  }

  onCreateTodo() {
    this.isEditing = true;
  }

  onSaveTodo(event: TodoItem) {
    if (event.id) {
      this.todoService.upateTodoItem(event).subscribe({
        next: () => {
          this.findTodos();
        },
        error: (error) => {
          alert(error);
        },
        complete: () => {
          this.isEditing = false;
          this.editToDo = null;
        },
      });
    } else {
      this.todoService.createTodoItem(event).subscribe({
        next: () => {
          this.findTodos();
        },
        error: (error) => {
          alert(error);
        },
        complete: () => {
          this.isEditing = false;
          this.editToDo = null;
        },
      });
    }


  }
}
