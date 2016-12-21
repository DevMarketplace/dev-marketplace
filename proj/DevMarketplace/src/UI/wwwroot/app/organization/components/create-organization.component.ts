import { Component } from "@angular/core";
import { CompanyService } from "../services/company.service";
import { Organization } from "../models/organization.model";

@Component({
    selector: "create-organization"
    ,templateUrl: "/angular/template?name=CreateOrganizationPartial"
    ,providers: [CompanyService]
})

export class CrateOrganizationComponent {
    private organizationModel : Organization = new Organization();
    constructor(private companyService: CompanyService) { }

    public createOrganization() : void {
        this.companyService.createOrganization(this.organizationModel);
    }
}