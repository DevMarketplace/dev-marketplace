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
var ioc_identifiers_1 = require("../config/ioc.identifiers");
var Observable_1 = require("rxjs/Observable");
var axios_1 = require("axios");
require("reflect-metadata");
var inversify_1 = require("inversify");
var CountryService = (function () {
    function CountryService(appConfig) {
        var _this = this;
        this.appConfig = appConfig;
        this.http = axios_1.default.create();
        this.configurationAwait = appConfig.load();
        var configSubscription = this.configurationAwait.subscribe(function (result) {
            _this.apiAddress = appConfig.getConfig("apiAddress");
            _this.getCountriesUrl = _this.apiAddress + "/api/v1/country";
            configSubscription.unsubscribe();
        });
    }
    CountryService.prototype.getCountries = function () {
        var _this = this;
        return this.configurationAwait.flatMap(function (result) {
            var options = {
                url: _this.getCountriesUrl,
                method: "GET",
                headers: { "Content-Type": "application/json" }
            };
            return Observable_1.Observable.from(axios_1.default(options))
                .map(function (res) { return res.data; })
                .catch(function (error) { return Observable_1.Observable.throw(error || "Server error"); });
        });
    };
    CountryService = __decorate([
        inversify_1.injectable(),
        __param(0, inversify_1.inject(ioc_identifiers_1.default.IAppConfig)), 
        __metadata('design:paramtypes', [Object])
    ], CountryService);
    return CountryService;
}());
exports.CountryService = CountryService;
//# sourceMappingURL=country.service.js.map