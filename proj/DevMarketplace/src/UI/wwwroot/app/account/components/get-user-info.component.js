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
var GetUserInfoComponent = (function () {
    function GetUserInfoComponent(accountService, elementRef) {
        this.accountService = accountService;
        this.elementRef = elementRef;
        this.user = new current_user_model_1.CurrentUser();
    }
    GetUserInfoComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.accountService.getCurrentUser().subscribe(function (userResponse) { _this.user = userResponse; }, function (error) { return console.log(error); });
    };
    GetUserInfoComponent.prototype.ngAfterViewChecked = function () {
        $(this.elementRef.nativeElement).find(".dropdown-button").dropdown({ hover: false, belowOrigin: true });
    };
    GetUserInfoComponent = __decorate([
        core_1.Component({
            selector: "account-user-info",
            templateUrl: "/angular/template?name=UserAccountMenuPartial",
            providers: [account_service_1.AccountService]
        }), 
        __metadata('design:paramtypes', [account_service_1.AccountService, core_1.ElementRef])
    ], GetUserInfoComponent);
    return GetUserInfoComponent;
}());
exports.GetUserInfoComponent = GetUserInfoComponent;
//# sourceMappingURL=get-user-info.component.js.map