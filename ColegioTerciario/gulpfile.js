/// <vs BeforeBuild='css, scss, plugins' />
var gulp = require('gulp');
var concat = require('gulp-concat');
var sass = require('gulp-sass');

var plugins = [
    'src/plugins/jquery-2.1.1.min.js',
    'src/plugins/jquery.dataTables.min.js',
    'src/plugins/bootstrap.min.js',
    'src/plugins/dataTables.bootstrap.js',
    'src/plugins/bootstrap-switch.min.js',
    'src/plugins/moment.min.js',
    'src/plugins/moment_es.js',

    'src/plugins/**/*.js'
];

var theme_js = [
    'src/js/metronic.js',
    'src/js/layout.js',
    'src/js/bootbox.js',
    'src/js/bootstrap-editable.js',
    'src/js/bootstrap-switch.min.js',
    'src/js/select2_locale_es.js',
    'src/js/cent11.js',
    'src/js/**/*.js'
];

var theme_css = [
    'src/css/bootstrap.min.css',
    'src/css/bootstrap-editable.css',
    'src/css/bootstrap-switch.css',
    'src/css/select2.css',
    'src/css/select2-bootstrap.css',
    'src/plugins/dataTables.bootstrap.css',
    'src/css/font-awesome.min.css',
    'src/css/simple-line-icons.css',
    'src/css/login2.css',
    'src/css/components-md.css',
    'src/css/plugins-md.css',
    'src/css/layout.css',
    'src/css/default.css',
    'src/css/Site.css'
];

gulp.task('css', function () {
    return gulp.src(theme_css)
        .pipe(concat('cent11.concat.css'))
        .pipe(gulp.dest('./Content'));
});


gulp.task('plugins', function () {
    return gulp.src(plugins)
        .pipe(concat('plugins.concat.js'))
        .pipe(gulp.dest('./Scripts'));
});

gulp.task('js', ['plugins'], function () {
    return gulp.src(theme_js)
        .pipe(concat('colegio_terciario.concat.js'))
        .pipe(gulp.dest('./Scripts'));
});

gulp.task('default', function () {
    // place code for your default task here
});