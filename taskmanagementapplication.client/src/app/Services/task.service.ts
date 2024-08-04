import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { EmployeeTaskRequest } from "../Models/employee-task-request.model";
import { Injectable } from "@angular/core";

@Injectable()

export class TaskService {

  token: string = '';

  constructor(private _http: HttpClient) {
  }

  setUserToken(token: string) {
    this.token = token;
  }

  getTaskDetail(taskId: number): Observable<any> {

    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + this.token
    });

    return this._http.get('http://localhost:8090/api/task/getTaskDetails/' + taskId, { headers });
  }

  getEmployeeTaskList(userId: number): Observable<any> {

    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + this.token
    });
    return this._http.get('http://localhost:8090/api/task/getEmployeeTask/' + userId, { headers });
  }

  getEmployeeTaskByManagerId(managerId: number): Observable<any> {

    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + this.token
    });

    return this._http.get('http://localhost:8090/api/task/getTask/' + managerId, { headers });
  }

  getAllTask(): Observable<any> {

    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + this.token
    });

    return this._http.get('http://localhost:8090/api/task/getAll', { headers });
  }

  upsertTask(employeeTaskRequest: EmployeeTaskRequest): Observable<any> {

    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + this.token
    });

    return this._http.post('http://localhost:8090/api/task/upsert', employeeTaskRequest, { headers });
  }
}
