import { Component, OnInit } from '@angular/core';
import { TodoListService } from '../todo-list.service';
import { ToDoTask } from '../ToDoTask';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent implements OnInit {
  tasks: ToDoTask[] = [];
  taskPriority: any = 1;

  constructor(private taskListService: TodoListService) { }

  ngOnInit(): void {
    this.getTasks();
  }

  add(taskName: string): boolean {
    taskName = taskName.trim();
    if (!taskName){
      return false;
    }

    this.taskListService
      .addTask({Name: taskName, Priority: this.taskPriority < 0 ? 0 : this.taskPriority, Status: 0} as ToDoTask)
      .subscribe(newTask=>this.tasks.push(newTask.Payload));

    return true;
  }

  deleteTask(task: ToDoTask): void{
    this.taskListService
      .deleteTask(task.Id)
      .subscribe(newTask=>{
        var index = this.tasks.indexOf(task);
        if (index !== -1) {
            this.tasks.splice(index, 1);
        }
      });
  }

  updateTask(task: ToDoTask): void{
    this.taskListService
      .updateTask(task)
      .subscribe();
  }

  getTasks():void{
    this.taskListService.getTasks().subscribe(x=>this.tasks = x);
  }
}
