import Vue = require("vue");
import Component from "vue-class-component";
import {CurrentUser} from "../models/current-user.model";
import { AccountService } from "../services/account.service";
declare var $: any;

@Component({
    template: "#account-menu",
    props: {
        message: String
    }
})
export default class AccountMenu extends Vue {
    public user: CurrentUser;

    private accountService: AccountService;

    created(): void {
        let subscription = this.accountService.getCurrentUser().subscribe(
            (userResponse: CurrentUser) => { this.user = userResponse; },
            (error: any) => console.log(<any>error));

        subscription.unsubscribe();
    }

    mounted(): void {
        $(this.$el).find(".dropdown-button").dropdown({ hover: false, belowOrigin: true });
    }
}