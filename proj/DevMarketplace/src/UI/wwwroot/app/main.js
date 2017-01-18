"use strict";
var Vue = require('vue');
var App = require('./app.vue').default;
new Vue({
    el: '#app',
    components: { App: App },
    render: function (h) { return h('app'); }
});
//# sourceMappingURL=main.js.map