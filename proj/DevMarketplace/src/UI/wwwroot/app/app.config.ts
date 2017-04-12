import "./rxjs-operators";
import "rxjs/add/operator/take";
import "reflect-metadata";
import "rxjs/add/observable/concat";
import "rxjs/add/observable/of";
import { Observable } from "rxjs/Observable";
import { injectable } from "inversify";
import axios, {
    AxiosRequestConfig,
    AxiosResponse,
    AxiosInstance,
    AxiosError
} from "axios";

export interface IAppConfig {
    getConfig(key: any): string;
    getEnv(key: any): string;
    load(): Observable<boolean>;
}

@injectable()
export class AppConfig implements IAppConfig {
    private config: Object = null;
    private env: Object = null;
    private http: AxiosInstance;
    constructor() {
        this.http = axios.create();
    }

    /**
     * Use to get the data found in the second file (config file)
     */
    public getConfig(key: any): string {
        return this.config[key];
    }

    /**
     * Use to get the data found in the first file (env file)
     */
    public getEnv(key: any): string {
        return this.env[key];
    }

    /**
     * This method:
     *   a) Loads "env.json" to get the current working environment (e.g.: "production", "development")
     *   b) Loads "config.[env].json" to get all env"s variables (e.g.: "config.development.json")
     */
    public load(): Observable<boolean> {
        let request: Observable<any> = null;
        return Observable
            .from(this.http.get("/app/config/env.json"))
            .catch((error: AxiosError) => {
                console.log("Configuration file \"env.json\" could not be read");
                return Observable.throw(error.message || "Server error");
            })
            .flatMap((envResponse: AxiosResponse) => {
                this.env = envResponse.data;
                switch (envResponse.data.env) {
                    case "production": {
                        request = Observable.from(this.http.get("/app/config/config." + envResponse.data.env + ".json"));
                    } break;

                    case "development": {
                        request = Observable.from(this.http.get("/app/config/config." + envResponse.data.env + ".json"));
                    } break;

                    case "default": {
                        console.error("Environment file is not set or invalid");
                    } break;
                }

                if (request) {
                    return request;                    
                }

                return Observable.throw("Error reading " + envResponse.data.env +".json");
            })
            .catch((error: AxiosError) => {
                return Observable.throw(error.message || "Server error");
            })
            .flatMap((envResponse: AxiosResponse) => {
                this.config = envResponse.data;
                return Observable.of(true);
            });
    }
}