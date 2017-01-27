System.register(["rxjs/Observable", "reflect-metadata"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var Observable_1;
    var AccountService;
    return {
        setters:[
            function (Observable_1_1) {
                Observable_1 = Observable_1_1;
            },
            function (_1) {}],
        execute: function() {
            //@injectable()
            AccountService = (function () {
                function AccountService(http) {
                    this.http = http;
                    this.currentUserUrl = "/account/getcurrentuser";
                }
                AccountService.prototype.getCurrentUser = function () {
                    var options = {
                        url: this.currentUserUrl,
                        method: "POST",
                        headers: { "Content-Type": "application/json" },
                        withCredentials: true
                    };
                    return Observable_1.Observable
                        .from(this.http(options))
                        .map(function (res) { return res.data; })
                        .catch(function (error) { return Observable_1.Observable.throw(error || "Server error"); });
                };
                return AccountService;
            }());
            exports_1("AccountService", AccountService);
        }
    }
});
//# sourceMappingURL=account.service.js.map