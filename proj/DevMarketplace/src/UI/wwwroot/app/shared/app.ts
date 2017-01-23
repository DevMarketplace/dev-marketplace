import Vue = require("vue");
import AccountMenu from "./account-menu"

new Vue({
    el: "#app",
    render: h => h(AccountMenu, {})
});