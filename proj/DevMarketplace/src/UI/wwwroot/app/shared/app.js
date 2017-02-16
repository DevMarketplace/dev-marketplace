"use strict";
var Vue = require("vue");
require("reflect-metadata");
var account_menu_1 = require("./account-menu");
var app = new Vue({
    el: "#nav-desktop",
    components: { AccountMenu: account_menu_1.default }
});
//# sourceMappingURL=app.js.map