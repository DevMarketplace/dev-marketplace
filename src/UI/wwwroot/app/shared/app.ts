import Vue = require("vue");
import { AccountMenu } from "./account-menu.component";
import { CreateOrganization } from "../organization/create-organization.component";

var app : Vue = new Vue({
    el: "#main-menu",
    components: { AccountMenu }
});

if (document.querySelector("#create-organization-app") != null) {
    var createOrganizationApp: Vue = new Vue({
        el: "#create-organization-app",
        components: { CreateOrganization }
    });
}