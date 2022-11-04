import { selectUser } from '@store/reducers/authSlice';
import { PATHS, ADMIN_PATHS } from '@router/paths';
import { useAppSelector } from '@store/hooks';
import { Navigate, useLocation } from 'react-router-dom';
import { useEffect, useState } from 'react';

export function RequireAuth({ children }: { children: JSX.Element }) {
  const user = useAppSelector(selectUser);
  const [path, setPath] = useState(PATHS.Login);
  const location = useLocation();
  console.log('user is', user);
  console.log('location', location);
  console.log('path', path);
  useEffect(() => {
    if (location.pathname.includes('admin')) {
      console.log('first if');
      setPath(ADMIN_PATHS.Login);
    } else {
      setPath(PATHS.Login);
    }
  }, [location]);

  if (!user?.userName) {
    // Redirect them to the /login page, but save the current location they were
    // trying to go to when they were redirected. This allows us to send them
    // along to that page after they login, which is a nicer user experience
    // than dropping them off on the home page.
    return <Navigate to={path} state={{ from: location }} replace />;
  }

  return children;
}
