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
require("./rxjs-operators");
require("rxjs/add/operator/take");
require("reflect-metadata");
require("rxjs/add/observable/concat");
require("rxjs/add/observable/of");
var Observable_1 = require("rxjs/Observable");
var inversify_1 = require("inversify");
var axios_1 = require("axios");
var AppConfig = (function () {
    function AppConfig() {
        this.config = null;
        this.env = null;
        this.http = axios_1.default.create();
    }
    /**
     * Use to get the data found in the second file (config file)
     */
    AppConfig.prototype.getConfig = function (key) {
        return this.config[key];
    };
    /**
     * Use to get the data found in the first file (env file)
     */
    AppConfig.prototype.getEnv = function (key) {
        return this.env[key];
    };
    /**
     * This method:
     *   a) Loads "env.json" to get the current working environment (e.g.: "production", "development")
     *   b) Loads "config.[env].json" to get all env"s variables (e.g.: "config.development.json")
     */
    AppConfig.prototype.load = function () {
        var _this = this;
        var request = null;
        return Observable_1.Observable
            .from(this.http.get("/app/config/env.json"))
            .catch(function (error) {
            console.log("Configuration file \"env.json\" could not be read");
            return Observable_1.Observable.throw(error.message || "Server error");
        })
            .flatMap(function (envResponse) {
            _this.env = envResponse.data;
            switch (envResponse.data.env) {
                case "production":
                    {
                        request = Observable_1.Observable.from(_this.http.get("/app/config/config." + envResponse.data.env + ".json"));
                    }
                    break;
                case "development":
                    {
                        request = Observable_1.Observable.from(_this.http.get("/app/config/config." + envResponse.data.env + ".json"));
                    }
                    break;
                case "default":
                    {
                        console.error("Environment file is not set or invalid");
                    }
                    break;
            }
            if (request) {
                return request;
            }
            return Observable_1.Observable.throw("Error reading " + envResponse.data.env + ".json");
        })
            .catch(function (error) {
            return Observable_1.Observable.throw(error.message || "Server error");
        })
            .flatMap(function (envResponse) {
            _this.config = envResponse.data;
            return Observable_1.Observable.of(true);
        });
    };
    AppConfig = __decorate([
        inversify_1.injectable(), 
        __metadata('design:paramtypes', [])
    ], AppConfig);
    return AppConfig;
}());
exports.AppConfig = AppConfig;
