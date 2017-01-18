module.exports = {
    entry: { app: './app/main.ts', },
    output: { filename: './js/main.js' },

    // resolve TypeScript and Vue file
    resolve: {
        extensions: ['', '.ts', '.vue', '.js']
    },

    module: {
        loaders: [
            { test: /\.vue$/, loader: 'vue-loader' },
            { test: /\.ts$/, loader: 'vue-ts' }
        ],
    },
    vue: {
        // instruct vue-loader to load TypeScript
        loaders: { js: 'vue-ts-loader', },
        // make TS' generated code cooperate with vue-loader
        esModule: true
    },
}