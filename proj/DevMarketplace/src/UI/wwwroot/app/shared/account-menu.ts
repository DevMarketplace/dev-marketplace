import { Subscription } from "rxjs/Subscription";
import { CurrentUser, ICurrentUser } from "../models/current-user.model";
import { AccountService, IAccountService } from "../services/account.service";
import { inject } from "inversify";
import "reflect-metadata";
import Component from "vue-class-component";
import Vue = require("vue");

import {
    ComponentOptions
} from "vue";

declare var $: any;

@Component({
    template: "#account-menu",
    // props: {
    //     //user: CurrentUser
    // }
})

export default class AccountMenu extends Vue {    

    @inject("ICurrentUser")
    private _user: ICurrentUser;

    @inject("IAccountService")
    private _accountService: IAccountService;

    created(): void {
        const subscription : Subscription = this._accountService.getCurrentUser().subscribe(
            (userResponse: CurrentUser) => { this._user = userResponse; },
            (error: any) => console.log(<any>error));

        subscription.unsubscribe();
    }

    mounted(): void {
        $(this.$el).find(".dropdown-button").dropdown({ hover: false, belowOrigin: true });
    }
}