import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ToDoTask } from '../ToDoTask';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent implements OnInit {
  @Input() task!: ToDoTask;
  @Output("onDelete") deleteRequest = new EventEmitter<ToDoTask>();
  @Output("onUpdate") updateRequest = new EventEmitter<ToDoTask>();

  constructor() { }

  ngOnInit(): void {
  }

  deleteTask():void{
    this.deleteRequest.emit(this.task);
  }

  completeTask():void{
    this.task.Status = 2;
    this.updateRequest.emit(this.task);
  }

  startTask():void{
    this.task.Status = 1;
    this.updateRequest.emit(this.task);
  }

  stopTask():void{
    this.task.Status = 0;
    this.updateRequest.emit(this.task);
  }
}
