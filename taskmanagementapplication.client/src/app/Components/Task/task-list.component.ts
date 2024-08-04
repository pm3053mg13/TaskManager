import { Component, OnInit } from "@angular/core";
import { TaskService } from "../../Services/task.service";
import { FormsModule } from "@angular/forms";
import { Router, RouterLink } from "@angular/router";
import { MatButtonModule } from '@angular/material/button'
import { MatCardModule } from '@angular/material/card'
import { CommonModule, NgFor } from "@angular/common";

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.css',
  standalone: true,
  imports: [FormsModule, RouterLink, MatButtonModule, MatCardModule, CommonModule, NgFor],
  providers: [TaskService]
})
export class TaskListComponent implements OnInit {

  taskDetails: any[] = [];
  userId: number = 0;
  roleId: number = 0;
  userToken: string = '';
  closedTaskStatusId: number = 3;
  dataFound: boolean = false;
  taskString: string = '';
  isLoading = false;

  constructor(private _taskService: TaskService, private _router: Router) {

  }

  ngOnInit(): void {

    this.userToken = localStorage.getItem('userToken')!;

    this._taskService.setUserToken(this.userToken);

    //if (!this.userToken || this.userToken == '' || this.userToken == null) { this._router.navigate(['login']); }

    this.userId = +localStorage.getItem('userId')!;
    this.roleId = +localStorage.getItem('roleId')!;

    if (this.roleId == 1) {
      this.getAllTask();
    }
    else if (this.roleId == 2) {
      this.getEmployeeTaskByManagerId();
    }
    else { this.getEmployeeTask(); }
  }

  getAllTask() {
    this.isLoading = true;
    this._taskService.getAllTask().subscribe(
      (result: any) => {
        if (result.length > 0) {
          this.dataFound = true;
          this.taskDetails = result;
          this.isLoading = false;
        }
      },
      (error) => {
        console.log(error);

        if (error.error == "401")
          this._router.navigate(['login/401'])

        this.isLoading = false;
      }
    )
  }

  getEmployeeTaskByManagerId() {

    this.isLoading = true;
    this._taskService.getEmployeeTaskByManagerId(this.userId).subscribe(
      (result: any) => {
        if (result.length > 0) {
          this.dataFound = true;
          this.taskDetails = result;
          this.isLoading = false;
        }
      },
      (error) => {
        console.log(error);
        if (error.error == "401")
          this._router.navigate(['login/401']);

        this.isLoading = false;
      }
    )
  }

  getEmployeeTask() {

    this.isLoading = true;
    this._taskService.getEmployeeTaskList(this.userId).subscribe(
      (result: any) => {
        if (result.length > 0) {
          this.dataFound = true;
          this.taskDetails = result;
          this.isLoading = false;
        }
      },
      (error) => {
        console.log(error);

        if (error.error == "401")
          this._router.navigate(['login/401']);

        this.isLoading = false;
      }
    )
  }

  logout() {
    this.isLoading = true;
    localStorage.removeItem('userToken');
    localStorage.removeItem('userId');
    localStorage.removeItem('roleId');
    setTimeout(() => {
      this.isLoading = false;
    }, 3000);
    this._router.navigate(['login/0'])
  }
}
