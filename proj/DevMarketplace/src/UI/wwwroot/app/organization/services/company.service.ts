import "../../rxjs-operators";
import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { AppConfig } from "../../app.config";
import { Organization } from "../models/organization.model";

@Injectable()

export class CompanyService {
    constructor(private http: Http) { }

    public createOrganization(organization : Organization): Observable<boolean> {
        let headers: Headers = new Headers({ "Content-Type": "application/json" });
        let options: RequestOptions = new RequestOptions({ headers: headers, withCredentials: true });

        return null;

        //return this.http.post(this.CurrentUserUrl, "", options)
        //    .map((res: Response) => res.json())
        //    .catch((error: any) => Observable.throw(error || "Server error"));
    }
}