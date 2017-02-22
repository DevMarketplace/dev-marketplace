"use strict";
var Vue = require("vue");
var account_menu_component_1 = require("./account-menu.component");
var create_organization_component_1 = require("../organization/create-organization.component");
var app = new Vue({
    el: "#main-menu",
    components: { AccountMenu: account_menu_component_1.AccountMenu }
});
if (document.querySelector("#create-organization-app") != null) {
    var createOrganizationApp = new Vue({
        el: "#create-organization-app",
        components: { CreateOrganization: create_organization_component_1.CreateOrganization }
    });
}
//# sourceMappingURL=app.js.map