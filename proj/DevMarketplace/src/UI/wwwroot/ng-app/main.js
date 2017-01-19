"use strict";
var Vue = require('vue');
var app = require('./app.vue').default;
new Vue({
    el: '#app',
    components: { app: app },
    render: function (h) { return h('app'); }
});
//# sourceMappingURL=main.js.map