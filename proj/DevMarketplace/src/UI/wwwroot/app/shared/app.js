"use strict";
var Vue = require("vue");
require("reflect-metadata");
var account_menu_1 = require("./account-menu");
// new Vue({
//     el: "main"
// });
var app = new Vue({
    el: "#app",
    components: { AccountMenu: account_menu_1.AccountMenu }
});
//# sourceMappingURL=app.js.map