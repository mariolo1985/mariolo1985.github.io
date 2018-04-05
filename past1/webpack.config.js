const { resolve } = require('path');

const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const OpenBrowserPlugin = require('open-browser-webpack-plugin');

const config = {
	devtool: 'cheap-module-eval-source-map',

	entry: [
		'react-hot-loader/patch',
		'webpack-dev-server/client?http://localhost:8080',
		'webpack/hot/only-dev-server',
		'./main.js',
		'./assets/scss/main.scss',
	],

	output: {
		filename: 'bundle.js',
		path: resolve(__dirname, 'dist'),
		publicPath: '',
	},

	context: resolve(__dirname, 'wwwroot/app'),

	devServer: {
		hot: true,
		contentBase: resolve(__dirname, 'wwwroot/build'),
		publicPath: '/',
		historyApiFallback: true,
	},

	module: {
		rules: [
			{
				enforce: "pre",
				test: /\.js$/,
				exclude: /node_modules/,
				loader: "eslint-loader"
			},
			{
				test: /\.js$/,
				loaders: [
					'babel-loader',
				],
				exclude: /node_modules/,
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

	plugins: [
		new webpack.LoaderOptionsPlugin({
			test: /\.js$/,
			options: {
				eslint: {
					configFile: resolve(__dirname, '.eslintrc'),
					cache: false,
				}
			},
		}),
		new webpack.optimize.ModuleConcatenationPlugin(),
		new ExtractTextPlugin({ filename: './styles/[name].css', disable: false, allChunks: false }),
		new CopyWebpackPlugin([{ from: 'vendors', to: 'vendors' }]),
		new OpenBrowserPlugin({ url: 'http://localhost:8080' }),
		new webpack.HotModuleReplacementPlugin(),
		new webpack.ProvidePlugin({
			$: "jquery",
			jQuery: "jquery"
		})
	],
};

module.exports = config;
