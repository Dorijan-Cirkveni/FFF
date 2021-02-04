module.exports = {
    context: __dirname,
    entry: "./src/app.js",
    output: {
            path: __dirname + "/dist",
            filename: "bundle.js"
    },
    watch: true,
    mode: 'development',

    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /(node_modules)/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: [  '@babel/preset-env',
                                    '@babel/preset-react',
                                        {
                                      'plugins': ['@babel/plugin-proposal-class-properties']
                            }]
                    }
                }
            },
            {
                test: /\.(png|jpe?g|gif)$/i,
                use: [
                    {
                        loader: 'file-loader',
                    },
                ],
            },
        ]
    }
    
}