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
        "inversify-inject-decorators": "npm:inversify-inject-decorators/lib/index.js",
        "reflect-metadata": "npm:reflect-metadata/Reflect",
        "vue-property-decorator": "npm:vue-property-decorator/lib/vue-property-decorator"
    },
    packages: {
        app: {
            main: "./app.js",
            defaultExtension: "js"
        }
    }
});