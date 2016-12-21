import { NgModule, APP_INITIALIZER } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpModule, JsonpModule } from "@angular/http";
import { GetUserInfoComponent } from "./account/components/get-user-info.component";
import { CrateOrganizationComponent } from "./organization/components/create-organization.component";
import { AppConfig } from "./app.config";

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        JsonpModule
    ],
    providers: [
        AppConfig,
        { provide: APP_INITIALIZER, useFactory: (config: AppConfig) => () => config.load(), deps: [AppConfig], multi: true }
    ],
    declarations: [
        GetUserInfoComponent,
        CrateOrganizationComponent
    ],
    bootstrap: [GetUserInfoComponent, CrateOrganizationComponent ]
})
export class AppModule { }