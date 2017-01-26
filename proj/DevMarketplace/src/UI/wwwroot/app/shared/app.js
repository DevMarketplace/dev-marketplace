System.register(["vue", "./account-menu"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var Vue, account_menu_1;
    return {
        setters:[
            function (Vue_1) {
                Vue = Vue_1;
            },
            function (account_menu_1_1) {
                account_menu_1 = account_menu_1_1;
            }],
        execute: function() {
            new Vue({
                el: "main"
            });
            new Vue({
                el: "nav",
                components: { AccountMenu: account_menu_1.default }
            });
        }
    }
});
//# sourceMappingURL=app.js.map