import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { inject } from "@angular/core";
import { Observable } from "rxjs";
import { LoginModel } from "../Models/login.model";

@Injectable()
export class LoginService {

  //_http: HttpClient = inject(HttpClient);

  constructor(private _http: HttpClient) {
  }

  public loginUser(userLoginModel: LoginModel): Observable<any> {
    return this._http.post('http://localhost:8090/api/login/loginUser', userLoginModel);
  }
}
