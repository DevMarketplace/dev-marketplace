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
    source = require("vinyl-source-stream"),
    sourcemaps = require("gulp-sourcemaps"),
    buffer = require("vinyl-buffer"),
    browserify = require("browserify");

var paths = {
    webroot: "./wwwroot/",
    nodeModules: "./node_modules/"
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
    return gulp.src(["!" + paths.nodeModules + "rxjs/src/**/*.*", paths.nodeModules + "rxjs/**/*.*"]).pipe(gulp.dest(paths.packageLib + "rxjs/"));
});

gulp.task("copy-axios", function () {
    return gulp.src([paths.nodeModules + "axios/**/*.*"]).pipe(gulp.dest(paths.packageLib + "axios/"));
});

gulp.task("copy-vue", function() {
    return gulp.src([paths.nodeModules + "vue/**/*.*"]).pipe(gulp.dest(paths.packageLib + "vue/"));
});

gulp.task("copy-inversify", function () {
    return gulp.src([paths.nodeModules + "inversify/**/*.*"]).pipe(gulp.dest(paths.packageLib + "inversify/"));
});

gulp.task("copy-reflect-metadata", function() {
    return gulp.src([paths.nodeModules + "reflect-metadata/**/*.*"]).pipe(gulp.dest(paths.packageLib + "reflect-metadata/"));
});

gulp.task("copy-inversify-inject-decorators", function () {
    return gulp.src([paths.nodeModules + "inversify-inject-decorators/**/*.*"]).pipe(gulp.dest(paths.packageLib + "inversify-inject-decorators/"));
});


gulp.task("copy-all", ["copy-rxjs", "copy-axios", "copy-systemjs", "copy-vue-class-component", "copy-vue", "copy-inversify", "copy-inversify-inject-decorators", "copy-reflect-metadata"]);

