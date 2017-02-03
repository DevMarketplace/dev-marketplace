//import "../../rxjs-operators";
//import { Injectable } from "@angular/core";
//import { Http, Response, Headers, RequestOptions } from "@angular/http";
//import { Observable } from "rxjs/Observable";
//import { AppConfig } from "../../app.config";
//import { Country } from "../models/country.model";
//@Injectable()
//export class CountryService {
//    constructor(private http: Http, private config : AppConfig) { }
//    public getCountries(): Observable<Country[]> {
//        let headers: Headers = new Headers({ "Content-Type": "application/json" });
//        let options: RequestOptions = new RequestOptions({ headers: headers, withCredentials: true });
//        let apiAddress: string = this.config.getConfig("apiAddress") + "/api/v1/country/";
//        return this.http.get(apiAddress, options)
//            .map((res: Response) => res.json())
//            .catch((error: any) => Observable.throw(error || "Server error"));
//    }
//} 
