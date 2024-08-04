import { Component, OnInit } from "@angular/core";
import { LoginModel } from "../../Models/login.model";
import { LoginService } from "../../Services/login.service";
import { ActivatedRoute, Router } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  loginModel: LoginModel = { username: '', password: '' }
  isError: boolean = false;
  isLoading = false;
  responseCode: number = 0;
  unAuthorized: boolean = false;
  constructor(private loginService: LoginService, private _router: Router, private activatedRoute: ActivatedRoute) {

  }
  ngOnInit(): void {
    this.responseCode = Number(this.activatedRoute.snapshot.paramMap.get('responseCode'));
    if (this.responseCode == 401)
      this.unAuthorized = true;
  }

  login() {
    this.isLoading = true;
    this.isError = false;
    this.unAuthorized = false;
    this.loginService.loginUser(this.loginModel).subscribe(
      (result) => {
        localStorage.setItem('userId', result.id);
        localStorage.setItem('roleId', result.roleId);
        localStorage.setItem('userToken', result.userToken);

        setTimeout(() => {
          this.isLoading = false;
        }, 3000);
        this._router.navigate(['taskList'])
      },
      (error) => {
        console.log(error);
        this.isLoading = false;
        this.isError = true;
      }
    )
  }
}
