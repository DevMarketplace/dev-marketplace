import { Observable } from "rxjs/Observable";
import * as Rx from "rxjs/Rx"
import { CurrentUser } from "../models/current-user.model";
import * as axios from 'axios'

export class AccountService {
    private currentUserUrl : string = "/account/getcurrentuser";
    
    constructor(private http: axios.AxiosStatic) { }

    public getCurrentUser(): Observable<CurrentUser> {
        let options: axios.AxiosRequestConfig = {
            url: this.currentUserUrl,
            method: "POST",
            headers: { "Content-Type": "application/json" },
            withCredentials: true
        } as axios.AxiosRequestConfig;


        return Rx.Observable
            .from(this.http(options))
            .map((res: axios.AxiosResponse) => res.data)
            .catch((error: any) => Observable.throw(error || "Server error"));
    }
}