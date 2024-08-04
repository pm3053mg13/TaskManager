import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UpsertUserRequestModel } from "../Models/upsert-user-request.model";
@Injectable()
export class UserService {

  //_http: HttpClient = inject(HttpClient);

  token: string = '';

  constructor(private _http: HttpClient) {
  }

  setUserToken(token: string) {
    this.token = token;
  }

  public createUser(createUserRequestModel: UpsertUserRequestModel): Observable<any> {

    const headers = new HttpHeaders({
      'Authorization': "Bearer " + this.token
    })

    return this._http.post('http://localhost:8090/api/user/create', createUserRequestModel, { headers });
  }

  public updateUser(updateUserRequestModel: UpsertUserRequestModel): Observable<any> {

    const headers = new HttpHeaders({
      'Authorization': "Bearer " + this.token
    })

    return this._http.post('http://localhost:8090/api/user/updateUser', updateUserRequestModel, { headers });
  }

  public getManagerDetails(): Observable<any> {

    const headers = new HttpHeaders({
      'Authorization': "Bearer " + this.token
    })

    return this._http.get('http://localhost:8090/api/user/getManagerList', { headers });
  }
}
