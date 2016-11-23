import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { GetUserInfoComponent } from './account/components/get-user-info.component';

@NgModule({
    imports: [BrowserModule],
    declarations: [
        AppComponent,
        GetUserInfoComponent
    ],
    bootstrap: [AppComponent, GetUserInfoComponent ]
})
export class AppModule { }