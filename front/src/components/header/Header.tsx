import { Avatar, Box, IconButton, Link, Stack, Typography } from '@mui/material';
import { ADMIN_PATHS, PATHS } from '@router/paths';
import { RouterLink } from '@router/utils';
import HomeIcon from '@mui/icons-material/Home';
import { useLocation } from 'react-router-dom';
import { useEffect, useState } from 'react';
import LogoutIcon from '@mui/icons-material/Logout';
import LoginIcon from '@mui/icons-material/Login';
import { useAppDispatch, useAppSelector } from '@store/hooks';
import { logout, selectUser } from '@store/reducers/authSlice';

export function Header({ children }: any) {
  const dispatch = useAppDispatch();
  const user = useAppSelector(selectUser);
  const { pathname } = useLocation();
  const [homePath, setHomePath] = useState(PATHS.Home);
  const [loginPath, setLoginPath] = useState(PATHS.Login);

  useEffect(() => {
    if (pathname.includes('admin')) {
      setHomePath(ADMIN_PATHS.Home);
      setLoginPath(ADMIN_PATHS.Login);
    } else {
      setHomePath(PATHS.Home);
      setLoginPath(PATHS.Login);
    }
  }, [pathname]);

  const handleLogout = () => {
    dispatch(logout());
  };

  const availableButtons = user?.userId ? (
    <Box sx={{ display: 'flex', gap: '1rem' }}>
      <Stack direction="row" spacing={1} alignItems="center">
        <Typography variant="body1" sx={{ display: 'flex', alignItems: 'center' }}>
          {user?.userName}
        </Typography>
        <Link component={RouterLink} to={PATHS.Profile}>
          <Avatar alt={user?.userName} src="" />
        </Link>
      </Stack>
      <IconButton onClick={handleLogout}>
        <LogoutIcon sx={{ color: '#1976d2' }} fontSize="large" />
      </IconButton>
    </Box>
  ) : (
    <Link component={RouterLink} to={loginPath}>
      <LoginIcon fontSize="large" />
    </Link>
  );

  return (
    <Box
      sx={{
        width: '100%',
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        gap: '1rem',
        mb: '1rem',
        padding: '1rem',
        height: '5rem',
      }}
    >
      <Link
        component={RouterLink}
        to={homePath}
        variant="h5"
        underline="none"
        sx={{ display: 'flex', alignItems: 'center' }}
      >
        <HomeIcon fontSize="large" />
      </Link>
      {children}
      {availableButtons}
    </Box>
  );
}
