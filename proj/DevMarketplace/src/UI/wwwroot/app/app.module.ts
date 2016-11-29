import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { Http } from "@angular/http";
import { AppComponent } from "./app.component";
import { HttpModule, JsonpModule } from "@angular/http";
import { TranslateModule } from "ng2-translate";
import { TranslateLoader } from "ng2-translate";
import { TranslateStaticLoader } from "ng2-translate";
import { GetUserInfoComponent } from "./account/components/get-user-info.component";

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        JsonpModule,
        TranslateModule.forRoot({
            provide: TranslateLoader,
            useFactory: (http: Http) => new TranslateStaticLoader(http, "/app/localization", ".json"),
            deps: [Http]
        })
    ],
    declarations: [
        AppComponent,
        GetUserInfoComponent
    ],
    bootstrap: [AppComponent, GetUserInfoComponent ]
})
export class AppModule { }