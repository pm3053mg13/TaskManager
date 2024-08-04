import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router, RouterLink } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { UserService } from "../../Services/user.service";
import { UpsertUserRequestModel } from "../../Models/upsert-user-request.model";
import { ManagerDetailModel } from "../../Models/manager-detail.model";


@Component({
  selector: 'app-user-upsert',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './user-upsert.component.html',
  styleUrl: './user-upsert.component.css'
})
export class UserUpsertComponent implements OnInit {

  managerDetailList: any[] = [];;
  upsertUserRequestModel: UpsertUserRequestModel = new UpsertUserRequestModel();
  isUpdate: boolean = false;
  userId: number = +localStorage.getItem('userId')!;
  userToken: string = '';
  isLoading = false;

  constructor(private userService: UserService, private _router: Router, private _route: ActivatedRoute) {

  }
  ngOnInit(): void {

    this.userToken = localStorage.getItem('userToken')!;

    this.userService.setUserToken(this.userToken);

    //if (!this.userToken || this.userToken == '' || this.userToken == null) { this._router.navigate(['login']); }
    this.upsertUserRequestModel.RoleId = 0;
    this.isUpdate = (Number(this._route.snapshot.paramMap.get('isUpdate')) == 1);

    this.getManagerDetail();
  }

  updateUser(upsertUserRequestModel: UpsertUserRequestModel) {

    this.isLoading = true;
    upsertUserRequestModel.UserId = this.userId;

    this.userService.updateUser(upsertUserRequestModel).subscribe(
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
        console.log(error);

        if (error.error == "401")
          this._router.navigate(['login/401']);

      }
    )
  }

  createUser(upsertUserRequestModel: UpsertUserRequestModel) {
    this.isLoading = true;
    this.userService.createUser(upsertUserRequestModel).subscribe(
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
        console.log(error);

        if (error.error == "401")
          this._router.navigate(['login/401']);
      }
    )
  }

  getManagerDetail() {
    this.isLoading = true;
    this.userService.getManagerDetails().subscribe(
      (result: any) => {
        this.managerDetailList = result;
        this.isLoading = false;
      },
      (error) => {
        console.log(error);
        this.isLoading = false;

        if (error.error == "401")
          this._router.navigate(['login/401']);
      }
    )
  }
}
