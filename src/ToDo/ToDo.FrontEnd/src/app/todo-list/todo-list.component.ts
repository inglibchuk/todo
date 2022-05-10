import { Component, OnInit } from '@angular/core';
import { ToastService } from '../toast.service';
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

  constructor(private taskListService: TodoListService, public toastService: ToastService) { }

  ngOnInit(): void {
    this.getTasks();
  }

  add(taskName: string): boolean {
    taskName = taskName.trim();
    if (!taskName){
      return false;
    }
    if (this.tasks.filter(x=>x.Name == taskName).length > 0){
      this.showError("Task's name should be unique");
      return false;
    }

    this.taskListService
      .addTask({Name: taskName, Priority: this.taskPriority < 0 ? 0 : this.taskPriority, Status: 0} as ToDoTask)
      .subscribe(response=>{
        if (response.Errors != null && response.Errors.length > 0){
          response.Errors.forEach((errorLine:any) =>{
            this.showError(errorLine);
          });
          this.getTasks();
          return;
        }
        this.tasks.push(response.Payload)});

    return true;
  }

  deleteTask(task: ToDoTask): void{
    this.taskListService
      .deleteTask(task.Id)
      .subscribe(response=>{
        if (response.Errors != null && response.Errors.length > 0){
          response.Errors.forEach((errorLine:any) =>{
            this.showError(errorLine);
          });
          this.getTasks();
          return;
        }
        var index = this.tasks.indexOf(task);
        if (index !== -1) {
            this.tasks.splice(index, 1);
        }
      });
  }

  updateTask(task: ToDoTask): void{
    this.taskListService
      .updateTask(task)
      .subscribe(response=>{
        if (response.Errors != null && response.Errors.length > 0){
          response.Errors.forEach(this.showError);
          this.getTasks();
          return;
        }
      });
  }

  getTasks():void{
    this.taskListService.getTasks().subscribe(x=>this.tasks = x);
  }

  showError(message: any){
    this.toastService.show(message, { classname: 'bg-danger text-light', delay: 10000 })
  }
}
