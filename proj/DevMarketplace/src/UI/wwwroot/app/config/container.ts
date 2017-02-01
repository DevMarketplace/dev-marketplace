import "reflect-metadata";
import { Container } from "inversify";
import { ICurrentUser, CurrentUser } from "../models/current-user.model";
import { IAccountService, AccountService } from "../services/account.service";
import serviceIdentifier from "../config/ioc.identifiers";
import { getDecorators } from "inversify-inject-decorators";

let container: Container = new Container();
container.bind<ICurrentUser>(serviceIdentifier.ICurrentUser).to(CurrentUser);
container.bind<IAccountService>(serviceIdentifier.IAccountService).to(AccountService);
let decorators = getDecorators(container);
let inject = decorators.lazyInject;
export {
    container,
    inject
}