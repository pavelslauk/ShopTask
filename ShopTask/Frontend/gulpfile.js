const webpackStream = require('webpack-stream');
const named = require('vinyl-named');
const gulp = require("gulp");

gulp.task('build-js', function ()
{
    let options = {
        optimization: {
            minimize: false
        }
    };

    return gulp.src('./js/*.js')        
        .pipe(named())
        .pipe(webpackStream(options))
        .pipe(gulp.dest('build'));

});

gulp.task('build-js-minify', function () 
{
    let options = {
        output: {
            path: __dirname + "/build",
            filename: "[name].min.js"
        },
    };

    return gulp.src('./js/*.js')        
        .pipe(named())
        .pipe(webpackStream(options))
        .pipe(gulp.dest('build'));
});

gulp.task('build', gulp.series('build-js', 'build-js-minify'));

gulp.watch(['orders.js', './js'], gulp.series('build'));