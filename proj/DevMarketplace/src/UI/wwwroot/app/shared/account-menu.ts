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
    data: {
        email: String
    }
})

export class AccountMenu extends Vue {

    @injectLazy(serviceIdentifier.ICurrentUser)
    public user: ICurrentUser;

    @injectLazy(serviceIdentifier.IAccountService)
    public accountService: IAccountService;

    public email: string;

    public authenticated: boolean;

    private accountSub: Subscription;

    created(): void {
        this.authenticated = false;
        this.accountSub = this.accountService.getCurrentUser().subscribe(
            (userResponse: ICurrentUser) => {
                this.user = userResponse;
                this.email = userResponse.email;
                this.authenticated = userResponse.authenticated;
            },
            (error: any) => console.log(<any>error));
    }

    mounted(): void {
        $(this.$el).find(".dropdown-button").dropdown({ hover: false, belowOrigin: true });
    }

    beforeDestroy(): void {
        this.accountSub.unsubscribe();
    }
}