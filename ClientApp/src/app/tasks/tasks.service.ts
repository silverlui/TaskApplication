import { Injectable } from '@angular/core';
import { getBaseUrl } from "../../main";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Todo } from "../interfaces/Todo";
import { CreateTodoDto } from "../interfaces/CreateTodoDto";
import { UpdateTodoDto } from "../interfaces/UpdateTodoDto";

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  private url = getBaseUrl() + 'api/v1/todo'

  constructor(private http: HttpClient) { }

  getTodos() {
    return this.http.get<Todo[]>(this.url);
  }

  postTodo(createTodoDto: CreateTodoDto) {
    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json'
    })
    return this.http.post<Todo>(this.url, createTodoDto,
      {
        headers: httpHeaders,
        observe: 'response'
      }
    );
  }

  completeTodo(updateTodoDto: UpdateTodoDto) {
    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json'
    })
    updateTodoDto.is_completed = true;
    return this.http.put<Todo>(this.url + "/" + updateTodoDto.id, updateTodoDto,
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
