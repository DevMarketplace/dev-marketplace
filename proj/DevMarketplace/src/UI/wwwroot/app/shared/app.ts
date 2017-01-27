import * as Vue from "vue"
import { Vue } from "vue"
import AccountMenu from "./account-menu";
import { Container } from "inversify";
import { ICurrentUser, CurrentUser } from "../models/current-user.model";
import { IAccountService, AccountService } from "../services/account.service";

let container : Container = new Container();
container.bind<ICurrentUser>("ICurrentUser").to(CurrentUser);
container.bind<IAccountService>("ICurrentUser").to(AccountService);

// new Vue({
//     el: "main"
// });

new Vue : Vue({
    el: "nav",
    components: { AccountMenu }
});