const webpackStream = require('webpack-stream');
const named = require('vinyl-named');
const gulp = require("gulp");

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

let optionsMinify = {
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

gulp.task('build-order-js', function ()
{   
    return gulp.src('./app/order.ts')        
        .pipe(named())
        .pipe(webpackStream(options))
        .pipe(gulp.dest('build'));

});

gulp.task('build-order-js-minify', function () 
{
    return gulp.src('./app/order.ts')        
        .pipe(named())
        .pipe(webpackStream(optionsMinify))
        .pipe(gulp.dest('build'));
});

gulp.task('build-products-js', function ()
{   
    return gulp.src('./app/products.ts')        
        .pipe(named())
        .pipe(webpackStream(options))
        .pipe(gulp.dest('build'));

});

gulp.task('build-products-js-minify', function () 
{
    return gulp.src('./app/products.ts')        
        .pipe(named())
        .pipe(webpackStream(optionsMinify))
        .pipe(gulp.dest('build'));
});

gulp.task('build-order', gulp.series('build-order-js', 'build-order-js-minify'));

gulp.task('build-products', gulp.series('build-products-js', 'build-products-js-minify'));

//gulp.watch('./app/order', gulp.series('build-order-js'));

gulp.watch('./app/products', gulp.series('build-products-js'));