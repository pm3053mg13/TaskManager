export class EmployeeTaskDetails {
  TaskId: number = 0;
  UserName: string = '';
  TaskName: string = '';
  TaskStatusId: number = 0;
  TaskStatus: string = '';
  CreatedDate: Date = new Date();
  FinishedDate: Date = new Date();
  DaysToFinish: number = 0;
}
