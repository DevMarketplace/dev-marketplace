import "../rxjs-operators";
import "reflect-metadata";
import serviceIdentifier from "../config/ioc.identifiers";
import { Observable } from "rxjs/Observable";
import { injectable, inject } from "inversify";
import { IAppConfig } from "../app.config";
import { Organization } from "../models/organization.model";
import axios, {
    AxiosRequestConfig,
    AxiosResponse,
    AxiosInstance
} from "axios";

export interface IOrganizationService {
    createOrganization(organization: Organization): Observable<boolean>;
}

@injectable()
export class OrganizationService implements IOrganizationService {
    private organizationApiUrl: string;
    private http: AxiosInstance;
    private apiAddress: string;
    private configurationAwait: Observable<boolean>;

    constructor( @inject(serviceIdentifier.IAppConfig) private appConfig: IAppConfig) {
        this.http = axios.create();
        this.configurationAwait = appConfig.load();
        this.configurationAwait.subscribe((result: boolean) => {
            this.apiAddress = appConfig.getConfig("apiAddress");
            this.organizationApiUrl = this.apiAddress + "/api/v1/organization";
        });
    }

    public createOrganization(organization: Organization): Observable<boolean> {
        return this.configurationAwait.flatMap((result: boolean) => {
            let options: AxiosRequestConfig = {
                url: this.organizationApiUrl,
                method: "POST",
                headers: { "Content-Type": "application/json" },
                data: organization
            } as AxiosRequestConfig;

            return Observable.from(axios(options))
                .map((res: AxiosResponse) => res.data)
                .catch((error: any) => Observable.throw(error || "Server error"));
        });
    }
}