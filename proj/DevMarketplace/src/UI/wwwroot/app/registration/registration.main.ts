import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";
import { RegistrationModule } from "./registration.module";

const platform: any = platformBrowserDynamic();
platform.bootstrapModule(RegistrationModule);