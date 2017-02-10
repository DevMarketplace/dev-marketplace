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
    template: "#account-menu"
})

export default class AccountMenu extends Vue {

    @injectLazy(serviceIdentifier.ICurrentUser)
    private user: ICurrentUser;

    @injectLazy(serviceIdentifier.IAccountService)
    private accountService: IAccountService;

    private accountSub: Subscription;

    created(): void {
        this.accountSub = this.accountService.getCurrentUser().subscribe(
            (userResponse: ICurrentUser) => {
                this.user = userResponse;
            },
            (error: any) => console.log(<any>error));
    }

    mounted(): void {
        $(this.$el).find(".dropdown-button").dropdown({ hover: false, belowOrigin: true });
    }

    beforeDestroy(): void {
        this.accountSub.unsubscribe();
    }

    public userEmail() : string {
        return this.user.email;
    }

    public userAuthenticated() : boolean {
        return this.user.authenticated;
    }
}