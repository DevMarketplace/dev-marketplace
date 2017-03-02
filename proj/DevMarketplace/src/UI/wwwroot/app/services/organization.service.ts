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
    createOrganization(): Observable<boolean>;
}

@injectable()
export class OrganizationService implements IOrganizationService {
    private createOrganizationUrl: string;
    private http: AxiosInstance;
    private apiAddress: string;
    private configurationAwait: Observable<boolean>;

    constructor(private appConfig: IAppConfig) {
        this.http = axios.create();
        this.configurationAwait = appConfig.load();
        this.configurationAwait.subscribe((result: boolean) => {
            this.apiAddress = appConfig.getConfig("apiAddress");
            this.createOrganizationUrl = this.apiAddress + "";
        });
    }

    createOrganization(): Observable<boolean> {
        throw new Error("Not implemented");
    }
}