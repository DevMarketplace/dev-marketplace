"use strict";
var Observable_1 = require("rxjs/Observable");
var Rx = require("rxjs/Rx");
var AccountService = (function () {
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
exports.AccountService = AccountService;
//# sourceMappingURL=account.service.js.map