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
require("reflect-metadata");
var Vue = require("vue");
var vue_class_component_1 = require("vue-class-component");
var ioc_identifiers_1 = require("../config/ioc.identifiers");
var organization_model_1 = require("../models/organization.model");
var container_1 = require("../config/container");
var CreateOrganization = (function (_super) {
    __extends(CreateOrganization, _super);
    function CreateOrganization() {
        _super.apply(this, arguments);
        this.countries = [];
    }
    CreateOrganization.prototype.created = function () {
        var _this = this;
        this.organization = new organization_model_1.Organization();
        var countrySubscription = this.countryService.getCountries()
            .map(function (res) { return res.data; })
            .subscribe(function (res) {
            _this.countries = res;
            countrySubscription.unsubscribe();
            $.validator.unobtrusive.parse(document);
            $(_this.$el).find("form").valid();
        });
    };
    ;
    CreateOrganization.prototype.data = function () {
        return {
            countries: this.countries,
            organization: this.organization
        };
    };
    ;
    CreateOrganization.prototype.createOrganization = function () {
        $(this.$el).find("form").valid();
        var org = this.organization;
        //let organizationSubscription = this.organizationService
        //    .createOrganization(this.organization)
        //    .map((res: any) => res.data)
        //    .subscribe((orgId: string) =>
        //    {
        //        if (orgId === "") {
        //            console.log(orgId);
        //        }
        //        organizationSubscription.unsubscribe();
        //    });
    };
    ;
    CreateOrganization.prototype.updated = function () {
        if ($.fn.material_select !== "undefined") {
            $("select").material_select();
        }
    };
    ;
    __decorate([
        container_1.injectLazy(ioc_identifiers_1.default.IAccountService), 
        __metadata('design:type', Object)
    ], CreateOrganization.prototype, "organizationService", void 0);
    __decorate([
        container_1.injectLazy(ioc_identifiers_1.default.ICountryService), 
        __metadata('design:type', Object)
    ], CreateOrganization.prototype, "countryService", void 0);
    CreateOrganization = __decorate([
        vue_class_component_1.default({
            template: "#create-organization-component"
        }), 
        __metadata('design:paramtypes', [])
    ], CreateOrganization);
    return CreateOrganization;
}(Vue));
exports.CreateOrganization = CreateOrganization;
//# sourceMappingURL=create-organization.component.js.map