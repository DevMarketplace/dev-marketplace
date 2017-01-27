import { Subscription } from "rxjs/Subscription
import { CurrentUser } from "../models/current-user.model";
import { AccountService } from "../services/account.service";
import { inject } from "inversify";
import "reflect-metadata";
import Component from "vue-class-component";
import * as Vue from "vue";
declare var $: any;

@Component({
    template: "#account-menu",
    // props: {
    //     //user: CurrentUser
    // }
})

export default class AccountMenu extends Vue {
    //@inject(CurrentUser)
    public user: CurrentUser;

    //@inject(AccountService)
    private accountService: AccountService;

    created(): void {
        const subscription : Subscription = this.accountService.getCurrentUser().subscribe(
            (userResponse: CurrentUser) => { this.user = userResponse; },
            (error: any) => console.log(<any>error));

        subscription.unsubscribe();
    }

    mounted(): void {
        $(this.$el).find(".dropdown-button").dropdown({ hover: false, belowOrigin: true });
    }
}