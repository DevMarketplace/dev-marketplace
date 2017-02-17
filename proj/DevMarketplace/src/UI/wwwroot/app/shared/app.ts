import Vue = require("vue");
import AccountMenu from "./account-menu";

var app : Vue = new Vue({
    el: "#main-menu",
    components: { AccountMenu }
});
export default app;