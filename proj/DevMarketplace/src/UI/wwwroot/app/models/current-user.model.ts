import getDecorators from "inversify-inject-decorators";
import { injectable } from "inversify";
import "reflect-metadata";

export interface ICurrentUser {
    email: string,
    firstName: string,
    authenticated: boolean;
}

@injectable()
export class CurrentUser implements ICurrentUser {
    email: string;
    firstName: string;
    authenticated: boolean;
}