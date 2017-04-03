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
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
require("../rxjs-operators");
require("reflect-metadata");
var ioc_identifiers_1 = require("../config/ioc.identifiers");
var Observable_1 = require("rxjs/Observable");
var inversify_1 = require("inversify");
var axios_1 = require("axios");
var OrganizationService = (function () {
    function OrganizationService(appConfig) {
        var _this = this;
        this.appConfig = appConfig;
        this.http = axios_1.default.create();
        this.configurationAwait = appConfig.load();
        this.configurationAwait = this.configurationAwait.map(function (result) {
            if (result) {
                _this.apiAddress = appConfig.getConfig("apiAddress");
                _this.organizationApiUrl = _this.apiAddress + "/api/v1/organization";
            }
            return result;
        });
    }
    OrganizationService.prototype.createOrganization = function (organization) {
        var _this = this;
        return this.configurationAwait.flatMap(function (result) {
            if (!result) {
                console.error("Configuration load error!");
                return null;
            }
            var options = {
                url: _this.organizationApiUrl,
                method: "POST",
                headers: { "Content-Type": "application/json" },
                data: organization
            };
            return Observable_1.Observable.from(axios_1.default(options))
                .map(function (res) { return res.data; })
                .catch(function (error) { return Observable_1.Observable.throw(error || "Server error"); });
        });
    };
    OrganizationService = __decorate([
        inversify_1.injectable(),
        __param(0, inversify_1.inject(ioc_identifiers_1.default.IAppConfig)), 
        __metadata('design:paramtypes', [Object])
    ], OrganizationService);
    return OrganizationService;
}());
exports.OrganizationService = OrganizationService;
//# sourceMappingURL=organization.service.js.map