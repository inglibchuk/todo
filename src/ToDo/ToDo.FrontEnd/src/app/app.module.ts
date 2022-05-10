import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TodoListComponent } from './todo-list/todo-list.component';
import { TaskComponent } from './task/task.component';
import { OrderByPipe } from './order-by.pipe';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastsContainer } from './toast-container/toast-container.component';

@NgModule({
  declarations: [
    AppComponent,
    TodoListComponent,
    TaskComponent,
    OrderByPipe,
    ToastsContainer
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
