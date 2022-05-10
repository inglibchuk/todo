import { Component, OnInit } from '@angular/core';
import { ToDoTask } from '../ToDoTask';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent implements OnInit {
  tasks: ToDoTask[] = [];

  constructor() { }

  ngOnInit(): void {
  }

  add(taskName: string, taskPriority: string): boolean {
    taskName = taskName.trim();
    if (!taskName){
      return false;
    }

    let taskPriorityAsNumber = +taskPriority < 0 ? 0 : +taskPriority;

    this.tasks.push({Name: taskName, Priority: taskPriorityAsNumber, Status: 0} as ToDoTask)

    return true;
  }

  deleteTask(task: ToDoTask): void{
    var index = this.tasks.indexOf(task);
    if (index !== -1) {
        this.tasks.splice(index, 1);
    }
  }

  updateTask(task: ToDoTask): void{

  }
}
