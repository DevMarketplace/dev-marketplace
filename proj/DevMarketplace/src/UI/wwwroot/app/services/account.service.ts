import { Observable } from "rxjs/Observable";
import * as Rx from "rxjs/Rx";
import { ICurrentUser } from "../models/current-user.model";
import * as axios from "axios";
import "reflect-metadata";
import { injectable } from "inversify";

export interface IAccountService {
    getCurrentUser(): Observable<ICurrentUser>;
}

//@injectable()
export class AccountService {
    private currentUserUrl: string = "/account/getcurrentuser";             
    constructor(private http: axios.AxiosStatic) { }

    public getCurrentUser(): Observable<ICurrentUser> {
        let options: axios.AxiosRequestConfig = {
            url: this.currentUserUrl,
            method: "POST",
            headers: { "Content-Type": "application/json" },
            withCredentials: true
        } as axios.AxiosRequestConfig;


        return Observable
            .from(this.http(options))
            .map((res: axios.AxiosResponse) => res.data)
            .catch((error: any) => Observable.throw(error || "Server error"));
    }
}