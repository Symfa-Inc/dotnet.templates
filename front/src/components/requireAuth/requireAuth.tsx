import { selectUser } from '@store/reducers/authSlice';
import { PATHS, ADMIN_PATHS } from '@router/paths';
import { useAppSelector } from '@store/hooks';
import { Navigate, useLocation } from 'react-router-dom';

export function RequireAuth({ children }: { children: JSX.Element }) {
  const user = useAppSelector(selectUser);
  const location = useLocation();

  if (!user?.userName) {
    if (location.pathname.includes('admin')) {
      return <Navigate to={ADMIN_PATHS.Login} state={{ from: location }} replace />;
    }
    return <Navigate to={PATHS.Login} state={{ from: location }} replace />;
  }
  return children;
}
