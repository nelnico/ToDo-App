import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TodoItem } from 'src/app/_models/todo-item';

@Component({
  selector: 'app-todo-item-edit',
  templateUrl: './todo-item-edit.component.html',
  styleUrls: ['./todo-item-edit.component.scss']
})
export class TodoItemEditComponent implements OnInit {

  ngOnInit(): void {
    if(this.todoItem) {
      this.editForm.controls.title.setValue(this.todoItem.title);
      this.editForm.controls.description.setValue(this.todoItem.description);
    }
  }

  @Input() todoItem: TodoItem = null;

  @Output() cancel: EventEmitter<void> = new EventEmitter();
  @Output() save: EventEmitter<TodoItem> = new EventEmitter();

  editForm = new FormGroup({
    title: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
  });

  onCancel() {
    this.cancel.emit();
  }

  onSubmit() {
    if(!this.todoItem) this.todoItem = new TodoItem();
    const controls = this.editForm.controls;
    this.todoItem.title = controls.title.value;
    this.todoItem.description = controls.description.value;
    this.save.emit(this.todoItem);
  }
}
