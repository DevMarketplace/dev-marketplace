import Vue = require("vue");
import "reflect-metadata";
import AccountMenu from "./account-menu";
import { Container } from "inversify";
import { ICurrentUser, CurrentUser } from "../models/current-user.model";
import { IAccountService, AccountService } from "../services/account.service";

let container : Container = new Container();
container.bind<ICurrentUser>("ICurrentUser").to(CurrentUser);
container.bind<IAccountService>("IAccountService").to(AccountService);

export default container;
// new Vue({
//     el: "main"
// });

var app = new Vue ({
    el: "#app",
    components: { AccountMenu }
});