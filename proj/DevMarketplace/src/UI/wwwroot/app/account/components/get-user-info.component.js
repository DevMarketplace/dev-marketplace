"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var account_service_1 = require("../services/account.service");
var current_user_model_1 = require("../models/current-user.model");
var ng2_translate_1 = require("ng2-translate");
var GetUserInfoComponent = (function () {
    function GetUserInfoComponent(accountService, translate) {
        this.accountService = accountService;
        this.translate = translate;
        this.user = new current_user_model_1.CurrentUser();
        translate.addLangs(["en"]);
        translate.setDefaultLang("en");
        var browserLang = translate.getBrowserLang();
        translate.use(browserLang.match(/en/) ? browserLang : "en");
    }
    GetUserInfoComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.accountService.getCurrentUser().subscribe(function (userResponse) { _this.user = userResponse; }, function (error) { return console.log(error); });
    };
    GetUserInfoComponent = __decorate([
        core_1.Component({
            selector: "account-user-info",
            templateUrl: "./app/account/templates/get-user-info.component.html",
            providers: [account_service_1.AccountService]
        }), 
        __metadata('design:paramtypes', [account_service_1.AccountService, ng2_translate_1.TranslateService])
    ], GetUserInfoComponent);
    return GetUserInfoComponent;
}());
exports.GetUserInfoComponent = GetUserInfoComponent;
//# sourceMappingURL=get-user-info.component.js.map