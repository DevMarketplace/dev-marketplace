System.config({
    defaultJSExtensions: true,
    transpiler: "typescript",
    typescriptOptions: {
        emitDecoratorMetadata: true
    },
    paths: {
      "npm:": "./../node_modules/"
    },
    map: {
        accountMenu: "/app/shared",
        "vue": "npm:vue/dist/vue.min.js",
        "av-ts": "npm:av-ts/dist/index.js",
        'rxjs': 'npm:rxjs'
    },
    packages: {
        accountMenu: {
            main: "./accountMenu.js",
            defaultExtension: "js"
        }
    }
});