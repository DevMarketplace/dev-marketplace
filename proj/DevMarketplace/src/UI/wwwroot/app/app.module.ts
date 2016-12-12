import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpModule, JsonpModule } from "@angular/http";
import { GetUserInfoComponent } from "./account/components/get-user-info.component";

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        JsonpModule
    ],
    declarations: [
        GetUserInfoComponent
    ],
    bootstrap: [GetUserInfoComponent ]
})
export class AppModule { }