const path = require('path'); // eslint-disable-line no-unused-vars
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');

module.exports = {
  entry: {
    App: [
      './src/js/app.jsx'
    ],
    Public: './src/js/public.jsx',
    vendor: [
      'jquery',
      'react',
      'react-dom',
      'reflux',
      'react-router',
      'lodash',
      'react-bootstrap',
      'react-gravatar',
      'bootstrap-select',
      'griddle-react',
      'classnames',
      'toastr',
      'axios'
    ]
  },
  output: {
    filename: '[name].js',
    chunkFilename: '[id].chunk.js',
    publicPath: '/'
  },
  devtool: 'source-map',
  devServer: {
    historyApiFallback: true,
    hot: true,
    inline: true,
    progress: true,
    proxy: {
      '*': {
        target: 'http://localhost:63440/'
      }
    }
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
        loader: 'style!css?importLoaders=1!postcss'
      }, {
        test: /\.less$/,
        loader: ExtractTextPlugin.extract('style', 'css!postcss!less')
      }, {
        test: /(webfont|)\.(otf|eot|ttf|woff|woff2|svg)(\?.+|)$/,
        loader: 'url-loader?limit=8192'
      }, {
        test: /\.(jpe?g|png|gif|svg)$/i,
        loaders: [
          'file?hash=sha512&digest=hex&name=dist/[hash].[ext]',
          'image-webpack?bypassOnDebug&optimizationLevel=7&interlaced=false'
        ]
      }, {
        test: /\.(jsx|js)/,
        exclude: /(node_modules|bower_components)/,
        loader: 'babel'
      }],
    noParse: /\.min\.js/
  },
  resolve: {
    root: path.resolve(path.dirname(), './src'),
    extensions: ['', '.js', '.jsx'],
    // Tell webpack to look for required files in bower and node
    modulesDirectories: ['bower_components', 'node_modules'],
    alias: {
      Notification: 'js/components/UI/Notification',
      'react-wizard': 'js/components/lib/react-wizard.js'
    }
  },
  plugins: [
    new webpack.optimize.CommonsChunkPlugin('init.js'),
    new ExtractTextPlugin('cent11-2.0.css')
  ],
  eslint: {
    configFile: './.eslintrc'
  }
};
