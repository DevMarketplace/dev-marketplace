import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpModule, JsonpModule } from "@angular/http";
import { GetUserInfoComponent } from "./account/components/get-user-info.component";
import { CrateOrganizationComponent } from "./organization/components/create-organization.component"

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        JsonpModule
    ],
    declarations: [
        GetUserInfoComponent,
        CrateOrganizationComponent
    ],
    bootstrap: [GetUserInfoComponent, CrateOrganizationComponent ]
})
export class AppModule { }