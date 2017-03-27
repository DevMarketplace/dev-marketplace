import "../rxjs-operators";
import serviceIdentifier from "../config/ioc.identifiers";
import { Subscription } from "rxjs/Subscription";
import { Observable } from "rxjs/Observable";
import { IAppConfig } from "../app.config";
import { Country } from "../models/country.model";
import axios, {
    AxiosRequestConfig,
    AxiosResponse,
    AxiosInstance
} from "axios";

import "reflect-metadata";
import { injectable, inject } from "inversify";

export interface ICountryService {
    getCountries(): Observable<Country[]>;
}

@injectable()
export class CountryService implements ICountryService {
    private appConfig: IAppConfig;
    private getCountriesUrl: string;
    private http: AxiosInstance;
    private apiAddress: string;
    private configurationAwait: Observable<boolean>;

    constructor(@inject(serviceIdentifier.IAppConfig) appConfig: IAppConfig) {
        this.appConfig = appConfig;
        this.http = axios.create();
        this.configurationAwait = appConfig.load();
        let configSubscription : Subscription = this.configurationAwait.subscribe((result: boolean) => {
            this.apiAddress = appConfig.getConfig("apiAddress");
            this.getCountriesUrl = this.apiAddress + "/api/v1/country";
            configSubscription.unsubscribe();
        });
    }

    getCountries(): Observable<Country[]> {
        return this.configurationAwait.flatMap((result: boolean) => {
            let options: AxiosRequestConfig = {
                url: this.getCountriesUrl,
                method: "GET",
                headers: { "Content-Type": "application/json" }
            } as AxiosRequestConfig;

            return Observable.from(axios(options))
                .map((res: AxiosResponse) => res.data)
                .catch((error: any) => Observable.throw(error || "Server error"));
        });
    }
}