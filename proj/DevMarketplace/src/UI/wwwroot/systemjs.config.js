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
        "vue": "npm:vue/dist/vue.js",
        "vue-class-component": "npm:vue-class-component/dist/vue-class-component.js",
        "rxjs": "npm:rxjs",
        "axios": "npm:axios/dist/axios",
        "inversify": "npm:inversify/lib/inversify",
        "inversify-inject-decorators": "npm:inversify-inject-decorators",
        "reflect-metadata": "npm:reflect-metadata/Reflect"
    },
    packages: {
        app: {
            main: "./app.js",
            defaultExtension: "js"
        }
    }
});