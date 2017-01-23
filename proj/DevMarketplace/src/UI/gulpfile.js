/// <binding AfterBuild="sass" Clean="clean" ProjectOpened="copy-all" />
"use strict";

var gulp = require("gulp"),
    gutil = require("gulp-util"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    fs = require("fs"),
    path = require("path"),
    sass = require("gulp-sass"),
    //watchify = require("watchify"),
    aliasify = require("aliasify"),
    source = require("vinyl-source-stream"),
    sourcemaps = require("gulp-sourcemaps"),
    buffer = require("vinyl-buffer"),
    browserify = require("browserify");

var paths = {
    webroot: "./wwwroot/",
    nodeModules: "./node_modules/"
};

var aliasifyConfig = {
    aliases: {
        "vue$": "vue/dist/vue.js"
    },
    verbose: true
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";
paths.packageLib = paths.webroot + "npm/";

var jsPaths = [
    process.env.INIT_CWD + "\\App\\shared"
];

function getFolders(dir) {
    return fs.readdirSync(dir)
      .filter(function (file) {
          return fs.statSync(path.join(dir, file)).isDirectory();
      });
}

function compileJS(input, output) {
    // set up the browserify instance on a task basis
    var b = browserify({
        debug: true,
        entries: [input],
        basedir: process.env.INIT_CWD,
        paths: jsPaths
    });

    return b.transform(aliasify, aliasifyConfig)
        .bundle()
      .pipe(source("bundle.js"))
      .pipe(buffer())
      .pipe(sourcemaps.init({ loadMaps: true }))
          .on("error", gutil.log)
      .pipe(gulp.dest(output));
}

//function watchFolder(input, output) {
//    var b = browserify({
//        entries: [input],
//        cache: {},
//        packageCache: {},
//        plugin: [watchify],
//        basedir: process.env.INIT_CWD,
//        paths: jsPaths
//    });

//    function bundle() {
//        b.transform(aliasify, aliasifyConfig)
//            .bundle()
//            .pipe(source("bundle.js"))
//            .pipe(buffer())
//            .pipe(sourcemaps.init({ loadMaps: true }))
//            .on("error", gutil.log)
//            .pipe(sourcemaps.write("./"))
//            .pipe(gulp.dest(output));

//        gutil.log("Bundle rebuilt!");
//    }
//    b.on("update", bundle);
//    bundle();
//}

gulp.task("build-vue:js", function () {
    var folders = getFolders("App");
    gutil.log(folders);
    folders.map(function (folder) {
        compileJS("App//" + folder + "//main.js", "Scripts//app//" + folder);
    });
});

//gulp.task("default", function () {
//    var folders = getFolders("App");
//    gutil.log(folders);
//    folders.map(function (folder) {
//        watchFolder("App//" + folder + "//main.js", "Scripts//app//" + folder);
//    });

//});

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("sass", function () {
    return gulp.src(paths.webroot + "sass/**/*.scss")
      .pipe(sass().on("error", sass.logError))
      .pipe(gulp.dest(paths.webroot + "css"));
});

gulp.task("sass:watch", function () {
    gulp.watch(paths.webroot + "sass/**/*.scss", ["sass"]);
});


gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
      .pipe(concat(paths.concatJsDest))
      .pipe(uglify())
      .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
      .pipe(concat(paths.concatCssDest))
      .pipe(cssmin())
      .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);


gulp.task("copy-systemjs", function () {
    return gulp.src(paths.nodeModules + "systemjs/dist/**/*.*", {
        base: paths.nodeModules + "systemjs/dist/"
    }).pipe(gulp.dest(paths.packageLib + "systemjs/"));
});

gulp.task("copy-vue-class-component", function() {
    return gulp.src([paths.nodeModules + "vue-class-component/**/*.*"]).pipe(gulp.dest(paths.packageLib + "vue-class-component/"));
});

gulp.task("copy-rxjs", function () {
    return gulp.src([paths.nodeModules + "rxjs/**/"]).pipe(gulp.dest(paths.packageLib + "rxjs/"));
});

gulp.task("copy-vue", function() {
    return gulp.src([paths.nodeModules + "vue/**/*.js"]).pipe(gulp.dest(paths.packageLib + "vue/"));
});

gulp.task("copy-all", ["copy-systemjs", "copy-vue-class-component", "copy-vue"]);

