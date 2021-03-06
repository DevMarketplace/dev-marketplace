"use strict";
require("reflect-metadata");
var inversify_1 = require("inversify");
var current_user_model_1 = require("../models/current-user.model");
var account_service_1 = require("../services/account.service");
var organization_service_1 = require("../services/organization.service");
var country_service_1 = require("../services/country.service");
var app_config_1 = require("../app.config");
var ioc_identifiers_1 = require("../config/ioc.identifiers");
var inversify_inject_decorators_1 = require("inversify-inject-decorators");
var container = new inversify_1.Container();
exports.container = container;
container.bind(ioc_identifiers_1.default.ICurrentUser).to(current_user_model_1.CurrentUser);
container.bind(ioc_identifiers_1.default.IAccountService).to(account_service_1.AccountService);
container.bind(ioc_identifiers_1.default.IAppConfig).to(app_config_1.AppConfig);
container.bind(ioc_identifiers_1.default.IOrganizationService).to(organization_service_1.OrganizationService);
container.bind(ioc_identifiers_1.default.ICountryService).to(country_service_1.CountryService);
var decorators = inversify_inject_decorators_1.default(container);
var injectLazy = decorators.lazyInject;
exports.injectLazy = injectLazy;
//# sourceMappingURL=container.js.map