export class UpsertUserRequestModel {
  UserId: number = 0;
  UserName: string = '';
  FirstName: string = '';
  LastName: string = '';
  Email: string = '';
  ManagerId: number = 0;
  RoleId: number = 0;
  Password: string = '';
}
