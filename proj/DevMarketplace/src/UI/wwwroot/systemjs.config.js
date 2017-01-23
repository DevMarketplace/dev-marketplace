System.config({
    defaultJSExtensions: true,
    transpiler: "typescript",
    typescriptOptions: {
        emitDecoratorMetadata: true
    },
    paths: {
      "npm:": "/npm/"
    },
    map: {
        app: "/app/shared",
        "vue": "npm:vue/dist/vue.min.js",
        "vue-class-component": "npm:vue-class-component/dist/vue-class-component.js",
        'rxjs': 'npm:rxjs'
    },
    packages: {
        app: {
            main: "./app.js",
            defaultExtension: "js"
        }
    }
});