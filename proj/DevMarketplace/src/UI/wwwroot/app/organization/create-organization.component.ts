import "reflect-metadata";
import Vue = require("vue");
import Component from "vue-class-component";
import serviceIdentifier from "../config/ioc.identifiers";
import { Subscription } from "rxjs/Subscription";
import { injectLazy } from "../config/container";
import { IOrganizationService } from "../services/organization.service";
import { ICountryService } from "../services/country.service";
import { Country } from "../models/country.model";
declare var $: any;

@Component({
    template: "#create-organization"
})
export class CreateOrganization extends Vue {

    @injectLazy(serviceIdentifier.IAccountService)
    private organizationService: IOrganizationService;

    @injectLazy(serviceIdentifier.ICountryService)
    private countryService: ICountryService;

    private countries: Country[];

    created(): void {
        this.countries = [];
    }

    mounted(): void {
        let countrySubscription = this.countryService.getCountries()
            .map((res: any) => res.data)
            .subscribe((res: Country[]) => {
                this.countries = res;
                countrySubscription.unsubscribe();
            });
    }

    updated(): any {
    }

    data(): any {
        return {
            countries: this.countries ? this.countries : []
        }
    }

    beforeDestroy(): void {
        
    }
}