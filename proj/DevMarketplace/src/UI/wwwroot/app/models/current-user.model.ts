import "reflect-metadata";
import { injectable } from "inversify";

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