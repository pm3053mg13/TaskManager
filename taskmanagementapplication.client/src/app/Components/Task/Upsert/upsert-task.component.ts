import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router, RouterLink } from "@angular/router";
import { TaskService } from "../../../Services/task.service";
import { EmployeeTaskDetails } from "../../../Models/employee-task-details.model";
import { EmployeeTaskRequest } from "../../../Models/employee-task-request.model";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";

@Component({
  selector: 'app-upsert-task',
  templateUrl: './upsert-task.component.html',
  styleUrl: './upsert-task.component.css',
  imports: [FormsModule, CommonModule, RouterLink],
  standalone: true
})
export class UpsertTaskComponent implements OnInit {
  taskId: number = 0;
  closedTaskStatusId: number = 0;
  taskDetails: EmployeeTaskDetails = new EmployeeTaskDetails();
  employeeTaskRequest: EmployeeTaskRequest = new EmployeeTaskRequest();
  userId: number = +localStorage.getItem('userId')!;
  userToken: string = '';
  isLoading = false;

  constructor(private route: ActivatedRoute, private _taskService: TaskService, private _router: Router) {

  }

  ngOnInit(): void {

    this.userToken = localStorage.getItem('userToken')!;

    //if (!this.userToken || this.userToken == '' || this.userToken == null) { this._router.navigate(['login']); }
    this.taskDetails.TaskStatusId = 0;
    this.taskDetails.DaysToFinish = 0;
    this._taskService.setUserToken(this.userToken);

    this.taskId = Number(this.route.snapshot.paramMap.get('TaskId'));
    this.closedTaskStatusId = Number(this.route.snapshot.paramMap.get('closedTaskStatusId'));

    //get task detail based on task Id
    if (this.taskId > 0) {
      this.getTaskById();
    }
  }

  getTaskById() {
    this.isLoading = true;
    this._taskService.getTaskDetail(this.taskId).subscribe(
      (result) => {
        this.taskDetails = result;
        this.isLoading = false;
      },
      (error) => {
        console.log(error);

        if (error.error == "401")
          this._router.navigate(['login/401']);

        this.isLoading = false;
      }
    )
  }

  upsertTask(task: any) {

    this.isLoading = true;
    this.employeeTaskRequest.Id = this.taskId;
    this.employeeTaskRequest.TaskName = task.taskName;
    this.employeeTaskRequest.StatusId = task.TaskStatusId;
    this.employeeTaskRequest.UserId = this.userId

    this._taskService.upsertTask(this.employeeTaskRequest).subscribe(
      (result) => {
        setTimeout(() => {
          this.isLoading = false;
        }, 3000);
        this._router.navigate(['taskList']);
      },
      (error) => {
        setTimeout(() => {
          this.isLoading = false;
        }, 3000);
        if (error.status == "401")
          this._router.navigate(['login', 401]);

        this._router.navigate(['taskList']);
      }
    )
  }

  createTask(task: any) {

    this.isLoading = true;
    this.employeeTaskRequest.Id = 0;
    this.employeeTaskRequest.TaskName = task.TaskName;
    this.employeeTaskRequest.UserId = this.userId;
    this.employeeTaskRequest.FinishedDays = task.DaysToFinish;

    this._taskService.upsertTask(this.employeeTaskRequest).subscribe(
      (result) => {
        setTimeout(() => {
          this.isLoading = false;
        }, 3000);
        this._router.navigate(['taskList']);
      },
      (error) => {
        console.log(error);
        setTimeout(() => {
          this.isLoading = false;
        }, 3000);

        if (error.status == "401")
          this._router.navigate(['login', 401]);
        this._router.navigate(['taskList'])
      }
    )
  }
}
