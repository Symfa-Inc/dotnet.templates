import { restoreSession } from '@store/reducers/authSlice';
import { useAppDispatch } from '@store/hooks';
import { Outlet } from 'react-router-dom';

export const Layout: React.FC<unknown> = () => {
  const dispatch = useAppDispatch();
  dispatch(restoreSession());

  return <Outlet />;
};
