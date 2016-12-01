import "../../rxjs-operators";
import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { CurrentUser } from "../models/current-user.model";

@Injectable()

export class AccountService {
    private CurrentUserUrl : string = "account/GetCurrentUser";

    constructor(private http: Http) { }

    public getCurrentUser(): Observable<CurrentUser> {
        let headers: Headers = new Headers({ "Content-Type": "application/json" });
        let options: RequestOptions = new RequestOptions({ headers: headers, withCredentials: true });

        return this.http.post(this.CurrentUserUrl, "", options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error || "Server error"));
    }
}