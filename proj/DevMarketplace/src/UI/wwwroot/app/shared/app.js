System.register(["vue", "./account-menu", "inversify", "../models/current-user.model", "../services/account.service"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var Vue, account_menu_1, inversify_1, current_user_model_1, account_service_1;
    var container;
    return {
        setters:[
            function (Vue_1) {
                Vue = Vue_1;
            },
            function (account_menu_1_1) {
                account_menu_1 = account_menu_1_1;
            },
            function (inversify_1_1) {
                inversify_1 = inversify_1_1;
            },
            function (current_user_model_1_1) {
                current_user_model_1 = current_user_model_1_1;
            },
            function (account_service_1_1) {
                account_service_1 = account_service_1_1;
            }],
        execute: function() {
            container = new inversify_1.Container();
            container.bind("ICurrentUser").to(current_user_model_1.CurrentUser);
            container.bind("ICurrentUser").to(account_service_1.AccountService);
            // new Vue({
            //     el: "main"
            // });
            new Vue;
            Vue({
                el: "nav",
                components: { AccountMenu: account_menu_1.default }
            });
        }
    }
});
//# sourceMappingURL=app.js.map