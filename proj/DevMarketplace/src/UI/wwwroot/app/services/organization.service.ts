import "../rxjs-operators";
import "reflect-metadata";
import { Observable } from "rxjs/Observable";
import { injectable } from "inversify";
import { IAppConfig } from "../app.config";
import axios, {
    AxiosRequestConfig,
    AxiosResponse,
    AxiosInstance
} from "axios";

export interface IOrganizationService {
    createOrganization(): boolean;
}

@injectable()
export class OrganizationService implements IOrganizationService {
    private createOrganizationUrl: string;
    private http: AxiosInstance;
    private apiAddress: string;

    constructor(private appConfig: IAppConfig) {
        this.http = axios.create();
        appConfig.load();
        this.apiAddress = appConfig.getConfig("apiAddress");
        this.createOrganizationUrl = this.apiAddress + "";
    }

    createOrganization(): boolean {
        throw new Error("Not implemented");
    }
}