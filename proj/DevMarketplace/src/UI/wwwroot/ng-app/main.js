System.register(['vue'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var Vue;
    var app;
    return {
        setters:[
            function (Vue_1) {
                Vue = Vue_1;
            }],
        execute: function() {
            app = require('./app.vue').default;
            new Vue({
                el: '#app',
                components: { app: app },
                render: function (h) { return h('app'); }
            });
        }
    }
});
//# sourceMappingURL=main.js.map