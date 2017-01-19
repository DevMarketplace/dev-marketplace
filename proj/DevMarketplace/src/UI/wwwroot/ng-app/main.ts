declare var require: any;

import Vue = require('vue')
var app = require('./app.vue').default;

new Vue({
    el: '#app',
    components: { app },
    render: h => h('app')
})