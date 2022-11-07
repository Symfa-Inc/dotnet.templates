This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app), using the [Redux](https://redux.js.org/) and [Redux Toolkit](https://redux-toolkit.js.org/) template.

## Purpose

We are using boilerplates to reduce the time of project initialization. This is example of our React app. We have configured fully automated process of CI/CD. The instruments which we are using for CI/CD are [Jenkins](https://www.jenkins.io) and [Portainer](https://www.portainer.io). So, everything we need to do to set up demo server is to press one button and customers will able to see result instantly! :wink:

## What's Included

1. ### Architecture

   We have extensive experience in development web applications with React. So we know how to structure, scale and support our apps. We are always following up our codebase, using all modern patterns and good practices.

   ### **Folder Structure - Main Folders**

   <ins>Pages</ins>: components that render views in routes. Divided into ADMIN PAGES and MAIN PAGES. These components may have other components that serves them within the same folder <br>
   <ins>Components</ins>: in here you will finde shared components that can be used in multiple PAGES, such as the BaseModal or the OverlaySpinner <br>
   <ins>Router</ins>: the main router and the paths are here <br>
   <ins>Store</ins>: the Redux setup <br>
   <ins>Styles</ins>: global styles for the application <br>

2. ### Typescript

   Typescript have a greate community which is constantly growing, that is why ts is setting up all modern trends and patterns, which makes development process comfortable. It is strictly typed and compiled language, this allows to avoid many errors on precompile stage, less bottleneks also typescript is following OOP principals, but also allows to write code in functional style.

3. ### Eslint

   Eslint analazes code to quickly find problems. ESLint is built into most text editors and you can run ESLint as part of your continuous integration pipeline. Also we are using [Husky](https://www.npmjs.com/package/husky), so developers and customers can be sure that all code that is pushed into repo is valid. :blush:

4. ### Routing

   The routes are being handled with [React Router 6](https://reactrouter.com/en/v6.3.0/getting-started/overview). Within the router folder you will find the MainRouter component setup, so everything what developer need to do is to create new pages, add the path in the path in the PATHS constant and the Route in the Main Router. If you need to protect a route, just wrap it in the RequireAuth component.

5. ### Store

   In this case we are using [Redux](https://redux.js.org) with Redux Toolkit. Redux is flexible and has a large ecosystem of addons. You will find all Redux setup in the STORE folder.

6. ### Styles and colors

   In this case we are using [MUI](https://mui.com/) for the CSS layout and styling. In the STYLE folder you will find some global styles, a Theme applied to the application and a Color Pallete you can change to change the colors globally.

## Available Scripts

In the project directory, you can run:

### `npm start`

Runs the app in the development mode.<br />
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.<br />
You will also see any lint errors in the console.

### `npm test`

Launches the test runner in the interactive watch mode.<br />
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### `npm run build`

Builds the app for production to the `build` folder.<br />
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.<br />
Your app is ready to be deployed!

See the section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.

### `npm run eject`

**Note: this is a one-way operation. Once you `eject`, you can’t go back!**

If you aren’t satisfied with the build tool and configuration choices, you can `eject` at any time. This command will remove the single build dependency from your project.

Instead, it will copy all the configuration files and the transitive dependencies (webpack, Babel, ESLint, etc) right into your project so you have full control over them. All of the commands except `eject` will still work, but they will point to the copied scripts so you can tweak them. At this point you’re on your own.

You don’t have to ever use `eject`. The curated feature set is suitable for small and middle deployments, and you shouldn’t feel obligated to use this feature. However we understand that this tool wouldn’t be useful if you couldn’t customize it when you are ready for it.

### `npm run starts`

This command will start the app using HTTPS.

If you need an own certificate, then:

1. mkcert create-ca - Create a Certificate Authority
2. mkcert create-cert - Create a Certificate

https://www.npmjs.com/package/mkcert

The second step will create a self-signed certificate and key (default names are 'cert.crt' and 'cert.key'). 3. Copy them in the root of the app and `npm run starts2`

## Learn More

You can learn more in the [Create React App documentation](https://facebook.github.io/create-react-app/docs/getting-started).

To learn React, check out the [React documentation](https://reactjs.org/).
