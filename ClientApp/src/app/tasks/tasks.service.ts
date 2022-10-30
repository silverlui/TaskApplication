import { Injectable } from '@angular/core';
import { getBaseUrl } from "../../main";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Todo } from "../interfaces/Todo";
import { CreateTodoDto } from "../interfaces/CreateTodoDto";

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  private url = getBaseUrl() + 'api/v1/todo'

  constructor(private http: HttpClient) { }

  getTodos() {
    return this.http.get<Todo[]>(this.url);
  }

  postTodo(post: CreateTodoDto) {
    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json'
    })
    return this.http.post<Todo>(this.url, post,
      {
        headers: httpHeaders,
        observe: 'response'
      }
    );
  }

  deleteAllTodos() {
    return this.http.delete(this.url);
  }

  deleteTodo(id: string) {
    return this.http.delete(this.url + "/" + id)
  }
}
