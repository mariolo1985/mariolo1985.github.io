const { resolve } = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');

const config = {
	devtool: 'cheap-module-source-map',

	entry: {
		bundle: './main.js',
		'main': './assets/scss/main.scss',
	},
	context: resolve(__dirname, 'wwwroot/app'),

	output: {
		filename: '[name].js',
		path: resolve(__dirname, 'wwwroot/dist'),
		publicPath: '',
	},

	plugins: [
		new webpack.DefinePlugin({ 'process.env': { NODE_ENV: JSON.stringify('development') } }),
		new ExtractTextPlugin({ filename: './styles/[name].css' }),
		new CopyWebpackPlugin([{ from: './vendors', to: 'vendors' }]),
		new webpack.ProvidePlugin({
			$: "jquery",
			jQuery: "jquery"
		})
	],

	module: {
		loaders: [
			{
				test: /\.js?$/,
				loader: 'babel-loader',
			},
			{
				test: /\.scss/,
				enforce: "pre",
				loader: 'import-glob-loader'
			},
			{
				test: /\.scss$/,
				exclude: /node_modules/,
				use: ExtractTextPlugin.extract({
					fallback: 'style-loader',
					use: [
						'css-loader',
						'resolve-url-loader',
						'sass-loader?sourceMap'
					],
					publicPath: '../'
				}),
			},
			{
				test: /\.(png|jpg|gif)$/,
				loader: 'url-loader',
				options: {
					limit: 15000,
					name: 'images/[name].[ext]'
				}
			},
			{
				test: /\.eot(\?v=\d+.\d+.\d+)?$/,
				use: {
					loader: 'url-loader',
					options: {
						limit: 10000,
						name: 'fonts/[name].[ext]'
					}
				}
			},
			{
				test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
				use: {
					loader: 'url-loader',
					options: {
						limit: 10000,
						name: 'fonts/[name].[ext]',
						mimetype: 'application/font-woff'
					}
				}
			},
			{
				test: /\.[ot]tf(\?v=\d+.\d+.\d+)?$/,
				use: {
					loader: 'url-loader',
					options: {
						limit: 10000,
						name: 'fonts/[name].[ext]',
						mimetype: 'application/octet-stream'
					}
				}
			},
			{
				test: /\.svg(\?v=\d+\.\d+\.\d+)?$/,
				use: {
					loader: 'url-loader',
					options: {
						limit: 10000,
						name: 'images/[name].[ext]',
						mimetype: 'image/svg+xml'
					}
				}
			},
		]
	},
};

module.exports = config;
