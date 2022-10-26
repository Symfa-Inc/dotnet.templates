import { selectUser } from '@features/auth/authSlice';
import { PATHS } from '@router/paths';
import { useAppSelector } from '@store/hooks';
import { Navigate, useLocation } from 'react-router-dom';

export function RequireAuth({ children }: { children: JSX.Element }) {
  const user = useAppSelector(selectUser);
  const location = useLocation();

  if (!user?.userName) {
    // Redirect them to the /login page, but save the current location they were
    // trying to go to when they were redirected. This allows us to send them
    // along to that page after they login, which is a nicer user experience
    // than dropping them off on the home page.
    return <Navigate to={PATHS.SignIn} state={{ from: location }} replace />;
  }

  return children;
}
