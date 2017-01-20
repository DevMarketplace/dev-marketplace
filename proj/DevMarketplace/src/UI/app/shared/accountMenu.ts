import { Vue } from "av-ts";
import {CurrentUser} from "./models/current-user.model";
import Currentusermodel = require("./models/current-user.model");

class AccountMenu extends Vue {
    constructor(private currentUser: Currentusermodel.CurrentUser) {
         super({
             el: "#account-menu"
         });
    }
}