(function (global) {
    System.config({
        paths: {
            // paths serve as alias
            'npm:': '/npm/'
        },
        // map tells the System loader where to look for things
        map: {
            // our app is within the app folder
            app: '/app',
            registration: '/app/registration',
            // other libraries
            'rxjs': 'npm:rxjs',
            'angular-in-memory-web-api': 'npm:angular-in-memory-web-api/in-memory-web-api.umd.js'
        },
        // packages tells the System loader how to load when no filename and/or no extension
        packages: {
            app: {
                main: './main.js',
                defaultExtension: 'js'
            },
            registration: {
                main: './registration.main.js',
                defaultExtension: 'js'
            },
            rxjs: {
                defaultExtension: 'js'
            }
        }
    });
})(this);