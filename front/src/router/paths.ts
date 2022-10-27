export const PATHS = {
  SignUp: '/signup',
  SignIn: '/signin',
  Home: '/',
  Profile: '/profile',
  ProviderAuthCallBack: '/authCallback',
  AdminBase: '/admin',
};

export const ADMIN_PATHS = {
  Home: PATHS.AdminBase,
  Login: `${PATHS.AdminBase}/login`,
  Profile: `${PATHS.AdminBase}/profile`,
  Edit: `${PATHS.AdminBase}/edit`,
};
