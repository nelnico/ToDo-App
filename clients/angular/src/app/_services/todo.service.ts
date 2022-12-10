import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination/paginated-result.model';
import { TodoItem } from '../_models/todo-item';
import { TodoSearchParams } from '../_models/todo-search-params';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  baseUrl = `${environment.apiUrl}todo`;

  constructor(private http: HttpClient) {}

  getTodoItems(searchParams: TodoSearchParams) {
    let params = this.getPaginationHeaders(searchParams);
    var url = `${this.baseUrl}`;
    return this.getPaginatedResults<TodoItem[]>(url, params);
  }

  getTodoItem(id: number) {
    let url = `${this.baseUrl}/${id}`;
    return this.http.get<TodoItem>(url);
  }

  createTodoItem(todo: TodoItem) {
    return this.http.post<TodoItem>(this.baseUrl, todo);
  }

  upateTodoItem(todo: TodoItem) {
    let url = `${this.baseUrl}/${todo.id}`;
    return this.http.put<TodoItem>(url, todo);
  }

  deleteTodoItem(id: number) {
    let url = `${this.baseUrl}/${id}`;
    return this.http.delete<TodoItem>(url);
  }

  private getPaginatedResults<T>(url: string, params: HttpParams) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map((response) => {
        paginatedResult.result = response.body as T;
        if (response.headers.get('Pagination') !== null) {
          var pagination = response.headers.get('Pagination');
          paginatedResult.pagination = JSON.parse(pagination ?? '');
        }
        return paginatedResult;
      })
    );
  }

  private getPaginationHeaders(params: TodoSearchParams): HttpParams {
    let parameters = new HttpParams();
    parameters = parameters.append('pageNumber', params.pageNumber?.toString());
    parameters = parameters.append('pageSize', params.pageSize?.toString());
    parameters = parameters.append('orderBy', params.orderBy);
    parameters = parameters.append('orderDirection', params.orderDirection);
    if (params.query) {
      parameters = parameters.append('query', params.query);
    }
    if (params.statusIds && params.statusIds.length > 0) {
      params.statusIds.forEach((id) => {
        parameters = parameters.append('statusIds', id);
      });
    }
    return parameters;
  }
}
