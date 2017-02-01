import { injectable } from "inversify";
import "reflect-metadata";

export interface ICurrentUser {
    email: string;
    firstName: string;
    authenticated: boolean;
}

@injectable()
export class CurrentUser implements ICurrentUser {
    public email: string;
    public firstName: string;
    public authenticated: boolean;
}