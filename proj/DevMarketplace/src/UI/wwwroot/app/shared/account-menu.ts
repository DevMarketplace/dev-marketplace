import { Subscription } from "rxjs/Subscription";
import { ICurrentUser } from "../models/current-user.model";
import { IAccountService } from "../services/account.service";
import "reflect-metadata";
import Component from "vue-class-component";
import serviceIdentifier from "../config/ioc.identifiers";
import Vue = require("vue");
import { injectLazy } from "../config/container";
declare var $: any;

@Component({
    template: "#account-menu",
    // props: {
    //     //user: CurrentUser
    // }
})

export class AccountMenu extends Vue {

    @injectLazy(serviceIdentifier.ICurrentUser)
    public user: ICurrentUser;

    @injectLazy(serviceIdentifier.IAccountService)
    public accountService: IAccountService;

    created(): void {
        const subscription : Subscription = this.accountService.getCurrentUser().subscribe(
            (userResponse: ICurrentUser) => { this.user = userResponse; },
            (error: any) => console.log(<any>error));

        subscription.unsubscribe();
    }

    mounted(): void {
        $(this.$el).find(".dropdown-button").dropdown({ hover: false, belowOrigin: true });
    }
}