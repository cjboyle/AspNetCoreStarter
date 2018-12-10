const path = require('path');
const merge = require('webpack-merge');
const common = require('./webpack.config.js');

module.exports = {
    mode: 'production',
    devtool: 'source-map',
    output: {
        filename: '[name].bundle.min.js',
        path: path.resolve(__dirname, '../wwwroot/dist'),
        publicPath: '/dist/'
    }
};
