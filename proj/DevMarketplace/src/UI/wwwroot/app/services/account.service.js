"use strict";
require("../../rxjs-operators");
var Observable_1 = require("rxjs/Observable");
var AccountService = (function () {
    function AccountService(http) {
        this.http = http;
        this.currentUserUrl = "/account/getcurrentuser";
    }
    AccountService.prototype.getCurrentUser = function () {
        var headers = new Headers({ "Content-Type": "application/json" });
        var options = new RequestOptions({ headers: headers, withCredentials: true });
        return this.http.post(this.currentUserUrl, "", options)
            .map(function (res) { return res.json(); })
            .catch(function (error) { return Observable_1.Observable.throw(error || "Server error"); });
    };
    return AccountService;
}());
exports.AccountService = AccountService;
//# sourceMappingURL=account.service.js.map