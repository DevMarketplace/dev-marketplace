import "reflect-metadata";
import { Subscription } from "rxjs/Subscription";
import { ICurrentUser } from "../models/current-user.model";
import { IAccountService } from "../services/account.service";
import Component from "vue-class-component";
import serviceIdentifier from "../config/ioc.identifiers";
import Vue = require("vue");
import { injectLazy } from "../config/container";
import { Guid } from "../helpers/guid.helper"
declare var $: any;

@Component({
    template: "#account-menu"
})
export default class AccountMenu extends Vue {

    @injectLazy(serviceIdentifier.IAccountService)
    private accountService: IAccountService;

    private email: string;

    private authenticated: boolean = false;

    private accountSub: Subscription;

    created(): void {
        this.accountSub = this.accountService.getCurrentUser()
            .subscribe(
                (userResponse: ICurrentUser) => {
                    this.email = userResponse.email;
                    this.authenticated = userResponse.authenticated;
                },
                (error: any) => console.log(<any>error));
    }

    updated(): any {
        $(".button-collapse").sideNav("destroy");
        $(".button-collapse").sideNav({ menuWidth: 320 });
        let uniqueId : string = Guid.newGuid();
        $(this.$el).find(".dropdown-button").attr("data-activates", uniqueId);
        $(this.$el).find(".dropdown-content").attr("id", uniqueId);
        $(this.$el).find(".dropdown-button").dropdown({ hover: true, belowOrigin: true });
    }

    data(): any {
        return {
            email: this.email,
            authenticated: this.authenticated
        };
    }

    beforeDestroy(): void {
        this.accountSub.unsubscribe();
    }
}