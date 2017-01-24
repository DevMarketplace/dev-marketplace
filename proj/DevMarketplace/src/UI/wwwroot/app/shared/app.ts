import Vue = require("vue");
import AccountMenu from "./account-menu";

new Vue({
    el: "main"
});

new Vue({
    el: "nav",
    components: { AccountMenu }
});