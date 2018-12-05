const webpackStream = require('webpack-stream');
const named = require('vinyl-named');
const gulp = require("gulp");
const isDevelopment = !process.env.NODE_ENV || process.env.NODE_ENV == 'development'

gulp.task('build', function () 
{
    let options = {
        watch: isDevelopment,
        devtool: isDevelopment ? 'cheap-module-inline-source-map' : null,
    };

    return gulp.src('./js/*.js')        
        .pipe(named())
        .pipe(webpackStream(options))
        .pipe(gulp.dest('Build'));

});