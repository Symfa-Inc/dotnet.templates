export const PATHS = {
  SignUp: '/signup',
  Login: '/login',
  Home: '/',
  Profile: '/profile',
  ProviderAuthCallBack: '/authCallback',
  PasswordReset: '/passwordReset',
  AdminBase: '/admin',
};

export const ADMIN_PATHS = {
  Home: PATHS.AdminBase,
  Login: `${PATHS.AdminBase}/login`,
  Profile: `${PATHS.AdminBase}/profile`,
  Edit: `${PATHS.AdminBase}/edit`,
};
