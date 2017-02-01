"use strict";
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var inversify_1 = require("inversify");
require("reflect-metadata");
var vue_class_component_1 = require("vue-class-component");
var ioc_identifiers_1 = require("../config/ioc.identifiers");
var Vue = require("vue");
var AccountMenu = (function (_super) {
    __extends(AccountMenu, _super);
    function AccountMenu() {
        _super.apply(this, arguments);
    }
    AccountMenu.prototype.created = function () {
        var _this = this;
        var subscription = this.accountService.getCurrentUser().subscribe(function (userResponse) { _this.user = userResponse; }, function (error) { return console.log(error); });
        subscription.unsubscribe();
    };
    AccountMenu.prototype.mounted = function () {
        $(this.$el).find(".dropdown-button").dropdown({ hover: false, belowOrigin: true });
    };
    __decorate([
        inversify_1.inject(ioc_identifiers_1.default.ICurrentUser), 
        __metadata('design:type', Object)
    ], AccountMenu.prototype, "user", void 0);
    __decorate([
        inversify_1.inject(ioc_identifiers_1.default.IAccountService), 
        __metadata('design:type', Object)
    ], AccountMenu.prototype, "accountService", void 0);
    AccountMenu = __decorate([
        vue_class_component_1.default({
            template: "#account-menu",
        }), 
        __metadata('design:paramtypes', [])
    ], AccountMenu);
    return AccountMenu;
}(Vue));
exports.AccountMenu = AccountMenu;
//# sourceMappingURL=account-menu.js.map