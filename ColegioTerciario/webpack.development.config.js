/*global require, module, __dirname*/
var path = require('path'); // eslint-disable-line no-unused-vars
var webpack = require('webpack');
var ExtractTextPlugin = require('extract-text-webpack-plugin');

const sassLoaders = [
    'css-loader',
    //'autoprefixer-loader?browsers=last 2 version",
    'sass-loader' //&includePaths[]=' //+ path.resolve(__dirname, './src') + '&' +
    //'includePaths[]=' + path.resolve(__dirname, 'node_modules/material-design-lite/src')

];

module.exports = {
    entry: {
        App: './src/js/app.jsx',
        vendor: [
          'jquery',
          'react',
          'reflux',
          'react-router',
          'lodash',
          'react-bootstrap',
          'react-bootstrap-table',
          'griddle-react',
          'material-ui'
        ]
    },
    output: {
        path: './Scripts/dev',
        filename: '[name].js'
    },
    devtool: 'source-map',
    devServer: {
      historyApiFallback: true,
      hot: true,
      inline: true,
      progress: true
    },
    module: {
        preLoaders: [{
            test: /\.jsx$/,
            loader: 'eslint-loader',
            exclude: /(node_modules|bower_components)/
        }],
        loaders: [
            {
                test: /\.css$/,
                //loader: 'style!css?importLoaders=1!postcss'
                loader: ExtractTextPlugin.extract('style', 'css?importLoaders=1!postcss')
            }, {
                test: /\.scss$/,
                loader: ExtractTextPlugin.extract('style', sassLoaders.join('!')),
                include: [
                    path.resolve(__dirname, './src'),
                    path.resolve(__dirname, 'node_modules/material-design-lite/src')
                ]
            }, {
                test: /\.less$/,
                loader: 'style!css!less'
            }, {
                test: /(webfont|)\.(otf|eot|svg|ttf|woff|woff2)(\?.+|)$/,
                loader: 'url-loader?limit=8192'
            },/* {
                test: /\.(jpe?g|png|gif)$/i,
                loaders: [
                    'file?hash=sha512&digest=hex&name=dist/[hash].[ext]',
                    'image-webpack?bypassOnDebug&optimizationLevel=7&interlaced=false'
                ]
            },*/ {
                test: /\.jsx/,
                exclude: /(node_modules|bower_components)/,
                loader: 'babel'
            }],
        noParse: /\.min\.js/
    },
    resolve: {
      extensions: ['', '.js', '.jsx'],
      // Tell webpack to look for required files in bower and node
      modulesDirectories: ['bower_components', 'node_modules'],
    },
    plugins: [
        new webpack.optimize.CommonsChunkPlugin('vendor', 'vendor.js'),
        new ExtractTextPlugin('cent11-2.0.css')
    ]/*,
    eslint: {
        configFile: './.eslintrc'
    }*/
};
