"use strict";
var Vue = require("vue");
require("reflect-metadata");
var account_menu_1 = require("./account-menu");
var inversify_1 = require("inversify");
var current_user_model_1 = require("../models/current-user.model");
var account_service_1 = require("../services/account.service");
var container = new inversify_1.Container();
container.bind("ICurrentUser").to(current_user_model_1.CurrentUser);
container.bind("ICurrentUser").to(account_service_1.AccountService);
// new Vue({
//     el: "main"
// });
var app = new Vue({
    el: "#app",
    components: { AccountMenu: account_menu_1.default }
});
//# sourceMappingURL=app.js.map