/* eslint-disable import/extensions */
/* eslint-disable @typescript-eslint/no-var-requires */
const path = require('path');
const {
  compilerOptions: { paths },
} = require('./tsconfig.paths.json');

const config = {
  eslint: {
    mode: 'file',
  },
  webpack: {
    alias: Object.keys(paths).reduce(
      (all, alias) => ({
        ...all,
        [alias.replace('/*', '')]: path.resolve(__dirname, 'src', paths[alias][0].replace('/*', '')),
      }),
      {},
    ),
  },
};

module.exports = config;
