import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";

import { AppModule } from "./app.module";

const platform : any = platformBrowserDynamic();
platform.bootstrapModule(AppModule);