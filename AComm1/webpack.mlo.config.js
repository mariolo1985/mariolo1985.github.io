var webpack = require('webpack');

module.exports = {
    entry: {
        login: [
            './wwwroot/app/login.js'
        ],
        catalog: [
            './wwwroot/app/catalog.js'
        ],
        coursecatalog:[
            './wwwroot/app/coursecatalog.js'
        ]
    },
    output: {
        path: __dirname + '/wwwroot/dist',
        filename: "[name].min.js"
    },
    module: {
        loaders: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                loader: "babel-loader"
            }
        ]
    },
    plugins: [
        new webpack.DefinePlugin({ 'process.env': { NODE_ENV: JSON.stringify('production') } })
    ]

};