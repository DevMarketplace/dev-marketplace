import Vue = require("vue");
import Component from "vue-class-component";
import {CurrentUser} from "./models/current-user.model";

@Component({
    el: "#account-menu"
})
export default class AccountMenu extends Vue {
    public userAccount: CurrentUser;

    public message: string;

    mounted(): void {
        this.message = "Hello World";
    }
}