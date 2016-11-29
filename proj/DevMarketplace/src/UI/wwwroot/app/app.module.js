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
var platform_browser_1 = require("@angular/platform-browser");
var http_1 = require("@angular/http");
var app_component_1 = require("./app.component");
var http_2 = require("@angular/http");
var ng2_translate_1 = require("ng2-translate");
var ng2_translate_2 = require("ng2-translate");
var ng2_translate_3 = require("ng2-translate");
var get_user_info_component_1 = require("./account/components/get-user-info.component");
var AppModule = (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [
                platform_browser_1.BrowserModule,
                http_2.HttpModule,
                http_2.JsonpModule,
                ng2_translate_1.TranslateModule.forRoot({
                    provide: ng2_translate_2.TranslateLoader,
                    useFactory: function (http) { return new ng2_translate_3.TranslateStaticLoader(http, "/app/localization", ".json"); },
                    deps: [http_1.Http]
                })
            ],
            declarations: [
                app_component_1.AppComponent,
                get_user_info_component_1.GetUserInfoComponent
            ],
            bootstrap: [app_component_1.AppComponent, get_user_info_component_1.GetUserInfoComponent]
        }), 
        __metadata('design:paramtypes', [])
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map