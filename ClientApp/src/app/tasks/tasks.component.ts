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

  constructor(public services: TasksService) {}

  ngOnInit(): void {
    this.services.getTodos().subscribe({
      next: (response: Todo[]) => {
        this.todoList = response;
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

        if (todo !== null) {
          this.todoList.push(todo);
        } else {
          console.log("Null Todo?");
        }
      },
      error: (err) => {
        console.error(err);
      }
    });

    input.value = '';
  }

  deleteTodos() {
    this.services.deleteAllTodos().subscribe({
      next: () => {
        this.todoList = [];
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  @ViewChild("todos") selectionList: MatSelectionList | undefined
  deleteSelectedTodos() {
    if (typeof this.selectionList !== "undefined") {
      const selected: Todo[] = this.selectionList.selectedOptions.selected.map((s) => s.value);

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
  }

  selectAll() {
    if (typeof this.selectionList !== "undefined") {
      return this.selectionList.selectAll()
    } else {
      console.log("this.selection might be undefined")
      return null;
    }
  }

  deselectAll() {
    if (typeof this.selectionList !== "undefined") {
      return this.selectionList.deselectAll()
    } else {
      console.log("this.selection might be undefined")
      return null;
    }
  }
}
