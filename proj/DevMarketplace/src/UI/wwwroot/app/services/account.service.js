System.register(["rxjs/Observable", "rxjs/Rx"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var Observable_1, Rx;
    var AccountService;
    return {
        setters:[
            function (Observable_1_1) {
                Observable_1 = Observable_1_1;
            },
            function (Rx_1) {
                Rx = Rx_1;
            }],
        execute: function() {
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
                    return Rx.Observable
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