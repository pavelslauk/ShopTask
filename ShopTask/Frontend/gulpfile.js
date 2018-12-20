const webpackStream = require('webpack-stream');
const named = require('vinyl-named');
const gulp = require("gulp");

gulp.task('build-js', function ()
{
    let options = {
        output:{
            path: __dirname + '/build',
            filename: "[name].js"
        },
        resolve: {
            extensions: ['.ts', '.js']
        },
        module:{
            rules:[
            {
               test: /\.ts$/,
               use: [
                {
                    loader: 'awesome-typescript-loader',
                    options: { configFileName: __dirname + '/tsconfig.json' }
                },
                'angular2-template-loader'
                ]
            },
            {
                test: /\.html$/,
                loader: 'html-loader'
            }]
        },
        optimization: {
            minimize: false
        }
    };

    return gulp.src('./app/*.ts')        
        .pipe(named())
        .pipe(webpackStream(options))
        .pipe(gulp.dest('build'));

});

gulp.task('build-js-minify', function () 
{
    let options = {
        output:{
            path: __dirname + '/build',
            filename: "[name].min.js"
        },
        resolve: {
            extensions: ['.ts', '.js']
        },
        module:{
            rules:[
            {
               test: /\.ts$/,
               use: [
                {
                    loader: 'awesome-typescript-loader',
                    options: { configFileName: __dirname + '/tsconfig.json' }
                },
                'angular2-template-loader'
                ]
            },
            {
                test: /\.html$/,
                loader: 'html-loader'
            }]
        }
    };

    return gulp.src('./app/*.ts')        
        .pipe(named())
        .pipe(webpackStream(options))
        .pipe(gulp.dest('build'));
});

gulp.task('build', gulp.series('build-js', 'build-js-minify'));

gulp.watch('./app', gulp.series('build-js'));