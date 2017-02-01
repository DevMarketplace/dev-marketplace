import Vue = require("vue");
import "reflect-metadata";
import { AccountMenu } from "./account-menu";

// new Vue({
//     el: "main"
// });

var app : Vue = new Vue ({
    el: "#app",
    components: { AccountMenu }
});