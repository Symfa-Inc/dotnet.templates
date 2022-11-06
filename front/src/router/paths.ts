export const PATHS = {
  SignUp: '/signup',
  Login: '/login',
  Home: '/',
  Profile: '/profile',
  ProviderAuthCallBack: '/authCallback',
  PasswordReset: '/passwordReset',
  AdminBase: '/admin',
  Recovery: '/recovery',
};

export const ADMIN_PATHS = {
  Login: `${PATHS.AdminBase}/login`,
  Home: `${PATHS.AdminBase}/home`,
  Profile: `${PATHS.AdminBase}/profile`,
  PasswordReset: `${PATHS.AdminBase}/passwordReset`,
};
