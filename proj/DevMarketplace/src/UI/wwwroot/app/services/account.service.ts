import "../../rxjs-operators";
import { Observable } from "rxjs/Observable";
import { CurrentUser } from "../models/current-user.model";

export class AccountService {
    private currentUserUrl : string = "/account/getcurrentuser";

    constructor(private http: Http) { }

    public getCurrentUser(): Observable<CurrentUser> {
        let headers: Headers = new Headers({ "Content-Type": "application/json" });
        let options: RequestOptions = new RequestOptions({ headers: headers, withCredentials: true });

        return this.http.post(this.currentUserUrl, "", options)
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error || "Server error"));
    }
}