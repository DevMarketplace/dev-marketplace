import "reflect-metadata";
import { Container } from "inversify";
import { ICurrentUser, CurrentUser } from "../models/current-user.model";
import { IAccountService, AccountService } from "../services/account.service";
import { IOrganizationService, OrganizationService } from "../services/organization.service";
import { ICountryService, CountryService } from "../services/country.service";
import { IAppConfig, AppConfig } from "../app.config";
import serviceIdentifier from "../config/ioc.identifiers";
import getDecorators from "inversify-inject-decorators";

let container: Container = new Container();
container.bind<ICurrentUser>(serviceIdentifier.ICurrentUser).to(CurrentUser);
container.bind<IAccountService>(serviceIdentifier.IAccountService).to(AccountService);
container.bind<IAppConfig>(serviceIdentifier.IAppConfig).to(AppConfig);
container.bind<IOrganizationService>(serviceIdentifier.IOrganizationService).to(OrganizationService);
container.bind<ICountryService>(serviceIdentifier.ICountryService).to(CountryService);

let decorators : any = getDecorators(container);
let injectLazy : any = decorators.lazyInject;
export {
    container,
    injectLazy
}