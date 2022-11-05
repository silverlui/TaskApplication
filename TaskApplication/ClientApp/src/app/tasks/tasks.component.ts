import { Component, OnInit, ViewChild } from '@angular/core';
import { Todo } from "../interfaces/Todo";
import { TasksService } from "./tasks.service";
import { CreateTodoDto } from "../interfaces/CreateTodoDto";
import { HttpResponse } from "@angular/common/http";
import { MatSelectionList } from "@angular/material/list";

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  public todoList: Todo[] = [];
  public completedTodoList: Todo[] = [];

  constructor(public services: TasksService) {}

  ngOnInit(): void {
    this.services.getTodos().subscribe({
      next: (response: Todo[]) => {
        response.map((todo) => {
          if (todo.is_completed) {
            this.completedTodoList.push(todo)
          } else {
            this.todoList.push(todo)
          }
        })
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  postTodo(input: HTMLInputElement) {
    if (input.value === '') {
      return
    }

    let todo: CreateTodoDto = {
      description: input.value
    }

    this.services.postTodo(todo).subscribe({
      next: (response: HttpResponse<Todo>) => {
        let todo = response.body;

        if (!todo)
          return console.error("Null Post Response")

        this.todoList.push(todo);
      },
      error: (err) => {
        console.error(err);
      }
    });

    input.value = '';
  }

  deleteAllTodos() {
    this.services.deleteAllTodos().subscribe({
      next: () => {
        this.todoList = [];
        this.completedTodoList = [];
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  @ViewChild("todos") deleteSelectionList: MatSelectionList | undefined
  deleteSelectedTodos() {
    if (!this.deleteSelectionList)
      return console.error("Selection List is undefined");

    const selected: Todo[] = this.deleteSelectionList.selectedOptions
      .selected
      .map((s) => s.value);

    selected.forEach((todo: Todo) => {
      this.services.deleteTodo(todo.id).subscribe({
        next: () => {
          this.todoList = this.todoList.filter(todo => !selected.includes(todo))
        },
        error: (err) => {
          console.error(err)
        }
      });
    })
  }

  @ViewChild("todos") completeSelectionList: MatSelectionList | undefined
  completeSelectedTodos() {
    if (!this.completeSelectionList)
      return console.error("Selection List is undefined");

    const selected: Todo[] = this.completeSelectionList.selectedOptions.selected.map((s) => s.value);
    selected.forEach((todo: Todo) => {
      this.services.completeTodo(todo).subscribe({
        next: () => {
          this.todoList = this.todoList.filter(todo => !todo.is_completed)
          this.completedTodoList.push(todo)
        },
        error: (err) => {
          console.error(err)
        }
      });
    })
  }

  selectAll() {
    if (!this.deleteSelectionList)
      return console.error("Selection List is undefined");

    return this.deleteSelectionList.selectAll()
  }

  deselectAll() {
    if (!this.deleteSelectionList)
      return console.error("Selection List is undefined");

    return this.deleteSelectionList.deselectAll()
  }
}
