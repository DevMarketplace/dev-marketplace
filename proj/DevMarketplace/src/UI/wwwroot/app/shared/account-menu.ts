import Vue = require("vue");
import Component from "vue-class-component";
import {CurrentUser} from "./models/current-user.model";

@Component({
    template: "#account-menu",
    props: {
        message: String
    }
})
export default class AccountMenu extends Vue {
    public userAccount: CurrentUser;

    public message: string;

    mounted(): void {
        this.message = "Hello World";
    }
}