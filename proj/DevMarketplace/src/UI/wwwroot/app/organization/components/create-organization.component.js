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
var company_service_1 = require("../services/company.service");
var country_service_1 = require("../services/country.service");
var organization_model_1 = require("../models/organization.model");
var CrateOrganizationComponent = (function () {
    function CrateOrganizationComponent(companyService, countryService) {
        var _this = this;
        this.companyService = companyService;
        this.countryService = countryService;
        this.organizationModel = new organization_model_1.Organization();
        this.countryService.getCountries().subscribe(function (countriesResponse) {
            _this.countries = countriesResponse.data;
        }, function (error) { return console.log(error); });
    }
    CrateOrganizationComponent.prototype.createOrganization = function () {
        this.companyService.createOrganization(this.organizationModel);
    };
    CrateOrganizationComponent = __decorate([
        core_1.Component({
            selector: "create-organization",
            templateUrl: "/angular/template?name=CreateOrganizationPartial",
            providers: [company_service_1.CompanyService, country_service_1.CountryService]
        }), 
        __metadata('design:paramtypes', [company_service_1.CompanyService, country_service_1.CountryService])
    ], CrateOrganizationComponent);
    return CrateOrganizationComponent;
}());
exports.CrateOrganizationComponent = CrateOrganizationComponent;
//# sourceMappingURL=create-organization.component.js.map