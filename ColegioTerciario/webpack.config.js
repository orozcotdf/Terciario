/*global require, module, __dirname*/
var path = require('path'); // eslint-disable-line no-unused-vars
var webpack = require('webpack');
var ExtractTextPlugin = require('extract-text-webpack-plugin');

var production = process.env.NODE_ENV === 'production';


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
      'bootstrap-select',
      'griddle-react',
      'material-ui',
      'classnames',
      'toastr',
      'axios'
    ]
  },
  output: {
    path: './Scripts/dist',
    filename: '[name].js'
  },
  module: {
      /*preLoaders: [{
          test: /\.js$/,
          loader: 'eslint',
          exclude: /(node_modules|bower_components)/
      }],*/
    loaders: [
      {
        test: /\.css$/,
        loader: 'style!css?importLoaders=1!postcss'
      }, {
        test: /\.less$/,
        loader: ExtractTextPlugin.extract('style', 'css!less?compress'
          // + 'relativeUrls&'
          //'includePath[]=' + path.resolve(__dirname, 'src/img'))
          )
      }, {
        test: /(webfont|)\.(otf|eot|ttf|woff|woff2|svg)(\?.+|)$/,
        loader: 'url-loader?limit=8192&name=fonts/[hash].[ext]'
      }, {
        test: /\.(jpe?g|png|gif|svg)$/i,
        loaders: [
          'file?hash=sha512&digest=hex&name=img/[hash].[ext]',
          'image-webpack?bypassOnDebug&optimizationLevel=7&interlaced=false'
        ]
      }, {
        test: /\.jsx/,
        exclude: /(node_modules|bower_components)/,
        loader: 'babel'
      }],
    noParse: /\.min\.js/
  },
  resolve: {
    extensions: ['', '.js', '.jsx'],
    // Tell webpack to look for required files in bower and node
    modulesDirectories: ['bower_components', 'node_modules']
  },
  plugins: [
    new webpack.optimize.DedupePlugin(),
    new webpack.optimize.UglifyJsPlugin({minimize: true}),
    new webpack.optimize.CommonsChunkPlugin('vendor', 'vendor.js'),
    new ExtractTextPlugin('cent11-2.0.css')
  ]
};
