import { Observable } from "rxjs/Observable";
import "../rxjs-operators";
import { ICurrentUser } from "../models/current-user.model";
import axios, {
    AxiosRequestConfig,
    AxiosResponse,
    AxiosInstance
} from "axios";

import "reflect-metadata";
import { injectable } from "inversify";

export interface IAccountService {
    getCurrentUser(): Observable<ICurrentUser>;
}

@injectable()
export class AccountService implements IAccountService {
    private currentUserUrl: string = "/account/getcurrentuser";
    private http: AxiosInstance;

    constructor() { this.http = axios.create(); }

    public getCurrentUser(): Observable<ICurrentUser> {

        let options : AxiosRequestConfig = {
            url: this.currentUserUrl,
            method: "POST",
            headers: { "Content-Type": "application/json" },
            withCredentials: true
        } as AxiosRequestConfig;

        return Observable.from(axios(options))
            .map((res: AxiosResponse) => res.data)
            .catch((error: any) => Observable.throw(error || "Server error"));
    }
}