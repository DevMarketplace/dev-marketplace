import Vue = require("vue");
import "reflect-metadata";
import AccountMenu from "./account-menu";

var app : Vue = new Vue ({
    el: "#nav-desktop",
    components: { AccountMenu }
});