module.exports = {
  parser: '@typescript-eslint/parser',
  env: {
    browser: true,
    jest: true,
  },
  extends: [
    'airbnb',
    'airbnb/hooks',
    'plugin:@typescript-eslint/recommended', // Uses the recommended rules from the @typescript-eslint/eslint-plugin
    'plugin:react/recommended', // Uses the recommended rules from @eslint-plugin-react
  ],
  parserOptions: {
    ecmaVersion: 2020,
    sourceType: 'module',
    ecmaFeatures: {
      jsx: true,
    },
  },
  plugins: ['import', '@typescript-eslint'],
  rules: {
    'lines-between-class-members': 0,
    'react/jsx-props-no-spreading': 0,
    '@typescript-eslint/consistent-type-assertions': 0,
    '@typescript-eslint/no-non-null-assertion': 0,
    'no-underscore-dangle': 0,
    'react/jsx-filename-extension': [2, { extensions: ['.js', '.jsx', '.ts', '.tsx'] }],
    '@typescript-eslint/no-unused-vars': ['error', { argsIgnorePattern: '^_|^req|^next' }],
    '@typescript-eslint/no-empty-function': 1,
    'react/prop-types': 0,
    'import/prefer-default-export': 0,
    'import/extensions': ['error', 'never', {
      interface: 'always',
      enum: 'always',
      service: 'always',
    }],
    'explicit-module-boundary-types': 0,
    '@typescript-eslint/explicit-module-boundary-types': 0,
    'import/no-unresolved': 0,
    'react/jsx-uses-react': 'off',
    'react/react-in-jsx-scope': 'off',
    'no-param-reassign': 'off',
    'no-shadow': 'off',
    '@typescript-eslint/no-shadow': ['error'],
    'no-restricted-globals': 'off',
    'consistent-return': 'off',
    'max-len': [
      'error',
      {
        code: 140,
        ignorePattern: '^import.*$',
      },
    ],
  },
  settings: {
    react: {
      version: 'detect',
    },
    'import/resolver': {
      alias: [['@', `${__dirname}/src`]],
    },
  },
};
