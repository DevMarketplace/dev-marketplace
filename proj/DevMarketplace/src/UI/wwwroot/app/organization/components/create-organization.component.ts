import { Component } from "@angular/core";
import { CompanyService } from "../services/company.service";
import { CountryService } from "../services/country.service";
import { Organization } from "../models/organization.model";
import { Country } from "../models/country.model";

@Component({
    selector: "create-organization"
    ,templateUrl: "/angular/template?name=CreateOrganizationPartial"
    ,providers: [CompanyService]
})

export class CrateOrganizationComponent {
    private organizationModel: Organization = new Organization();
    private countries: Country[];
    constructor(private companyService: CompanyService, private countryService: CountryService) {
        this.countryService.getCountries().subscribe(
            (countriesResponse: Country[]) => { this.countries = countriesResponse; },
            (error: any) => console.log(<any>error));
    }

    public createOrganization() : void {
        this.companyService.createOrganization(this.organizationModel);
    }
}