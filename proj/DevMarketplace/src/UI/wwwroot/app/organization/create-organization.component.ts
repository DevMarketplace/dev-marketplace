import "reflect-metadata";
import Vue = require("vue");
import Component from "vue-class-component";
import serviceIdentifier from "../config/ioc.identifiers.ts";
import { Organization } from "../models/organization.model.ts";
import { injectLazy } from "../config/container.ts";
import { Subscription } from "rxjs/Subscription";
import { IOrganizationService } from "../services/organization.service.ts";
import { ICountryService } from "../services/country.service.ts";
import { Country } from "../models/country.model.ts";
declare var $: any;

@Component({
    template: "#create-organization-component"
})
export class CreateOrganization extends Vue {

    @injectLazy(serviceIdentifier.IOrganizationService)
    private organizationService: IOrganizationService;

    @injectLazy(serviceIdentifier.ICountryService)
    private countryService: ICountryService;

    organization: Organization;

    countries: Country[] = [];

    created(): void {
        this.organization = new Organization();
        let countrySubscription : Subscription = this.countryService.getCountries()
            .map((res: any) => res.data)
            .subscribe((res: Country[]) => {
                this.countries = res;
                countrySubscription.unsubscribe();
                $.validator.unobtrusive.parse($(this.$el).find("form"));
                $(this.$el).find("form").valid();
            });
    };

    data(): any {
        return {
            countries: this.countries,
            organization: this.organization
        };
    };

    createOrganization(): void {
        $(this.$el).find("form").valid();
        let organizationSubscription : Subscription = this.organizationService
            .createOrganization(this.organization)
            .map((res: any) => res.data)
            .subscribe((orgId: string) => {
                if (orgId === "") {
                    console.log(orgId);
                }
                organizationSubscription.unsubscribe();
            });
    };

    updated(): any {
        // if ($.fn.material_select !== "undefined") {
        //    $("select").material_select();
        // }
    };
}