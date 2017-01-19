import { Vue } from "av-ts";
import {CurrentUser} from "./models/current-user.model";

class AccountMenu extends Vue {
    constructor(private currentUser: CurrentUser) {
         super({
             el: "#account-menu"
         });
    }
}