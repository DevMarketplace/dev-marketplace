"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
require("reflect-metadata");
var inversify_1 = require("inversify");
var CurrentUser = (function () {
    function CurrentUser() {
    }
    return CurrentUser;
}());
CurrentUser = __decorate([
    inversify_1.injectable()
], CurrentUser);
exports.CurrentUser = CurrentUser;
//# sourceMappingURL=current-user.model.js.map