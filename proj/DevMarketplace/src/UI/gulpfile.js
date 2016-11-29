/// <binding BeforeBuild='clean, copy-all' AfterBuild='sass, min:js, min:css' Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    fs = require("fs"),
    sass = require("gulp-sass");

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

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task('sass', function () {
    return gulp.src(paths.webroot + 'sass/**/*.scss')
      .pipe(sass().on('error', sass.logError))
      .pipe(gulp.dest(paths.webroot + 'css'));
});

gulp.task('sass:watch', function () {
    gulp.watch(paths.webroot + 'sass/**/*.scss', ['sass']);
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
    return gulp.src(paths.nodeModules + 'systemjs/dist/**/*.*', {
        base: paths.nodeModules + 'systemjs/dist/'
    }).pipe(gulp.dest(paths.packageLib + 'systemjs/'));
});
gulp.task("copy-angular2", function () {
    return gulp.src([paths.nodeModules + '@angular/**/'])
        .pipe(gulp.dest(paths.packageLib + '@angular/'));
});
gulp.task("copy-reflect-metadata", function () {
    return gulp.src(paths.nodeModules + 'reflect-metadata/Reflect.js')
        .pipe(gulp.dest(paths.packageLib + 'reflect-metadata/'));
});
gulp.task("copy-rxjs", function () {
    return gulp.src([paths.nodeModules + 'rxjs/**/']).pipe(gulp.dest(paths.packageLib + 'rxjs/'));
});
gulp.task("copy-ng2-translate", function() {
    return gulp.src([paths.nodeModules + 'ng2-translate/**/']).pipe(gulp.dest(paths.packageLib + 'ng2-translate/'));
});
gulp.task("copy-corejs", function () {
    return gulp.src(paths.nodeModules + 'core-js/client/*.*', {
        base: paths.nodeModules + 'core-js/client/'
    }).pipe(gulp.dest(paths.packageLib + 'core-js/'));
});

gulp.task("copy-angular-in-memory-web-api", function() {
    return gulp.src(paths.nodeModules + 'angular-in-memory-web-api/bundles/*.*', {
        base: paths.nodeModules + 'angular-in-memory-web-api/bundles'
    }).pipe(gulp.dest(paths.packageLib + 'angular-in-memory-web-api/'));
});

gulp.task("copy-zonejs", function () {
    return gulp.src(paths.nodeModules + 'zone.js/dist/*.*', {
        base: paths.nodeModules + 'zone.js/dist/'
    }).pipe(gulp.dest(paths.packageLib + 'zone.js/'));
});

gulp.task("copy-all", ["copy-rxjs", 'copy-angular2', 'copy-systemjs', 'copy-reflect-metadata', 'copy-corejs', 'copy-angular-in-memory-web-api', 'copy-zonejs', 'copy-ng2-translate']);
