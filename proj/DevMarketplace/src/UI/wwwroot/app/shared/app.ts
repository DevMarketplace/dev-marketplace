import Vue = require("vue");
import "reflect-metadata";
import AccountMenu from "./account-menu";
import { Container } from "inversify";
import { ICurrentUser, CurrentUser } from "../models/current-user.model";
import { IAccountService, AccountService } from "../services/account.service";

var container : Container = new Container();
container.bind<ICurrentUser>("ICurrentUser").to(CurrentUser);
container.bind<IAccountService>("ICurrentUser").to(AccountService);

// new Vue({
//     el: "main"
// });

var app = new Vue ({
    el: "#app",
    components: { AccountMenu }
});