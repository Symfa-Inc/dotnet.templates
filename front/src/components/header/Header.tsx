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
import { COLORS } from '@styles/colorPalette';

export function Header({ children }: any) {
  const dispatch = useAppDispatch();
  const user = useAppSelector(selectUser);
  const { pathname } = useLocation();
  const [homePath, setHomePath] = useState(PATHS.Home);
  const [loginPath, setLoginPath] = useState(PATHS.Login);
  const [profilePath, setProfilePath] = useState(PATHS.Profile);

  useEffect(() => {
    if (pathname.includes('admin')) {
      setHomePath(ADMIN_PATHS.Home);
      setLoginPath(ADMIN_PATHS.Login);
      setProfilePath(ADMIN_PATHS.Profile);
    } else {
      setHomePath(PATHS.Home);
      setLoginPath(PATHS.Login);
      setProfilePath(PATHS.Profile);
    }
  }, [pathname]);

  const handleLogout = () => {
    dispatch(logout());
  };

  const availableButtons = user?.userId ? (
    <Box sx={{ display: 'flex', gap: '1rem', marginLeft: 'auto' }}>
      <Stack direction="row" spacing={1} alignItems="center">
        <Typography variant="body1" sx={{ display: 'flex', alignItems: 'center' }}>
          {user?.userName}
        </Typography>
        <Link component={RouterLink} to={profilePath} underline="none">
          <Avatar alt={user?.userName} src={user.avatar} sx={{ bgcolor: COLORS.link }} />
        </Link>
      </Stack>
      <IconButton onClick={handleLogout}>
        <LogoutIcon fontSize="large" sx={{ color: COLORS.link }} />
      </IconButton>
    </Box>
  ) : (
    <>
      {!pathname.includes('login') && (
        <Link component={RouterLink} to={loginPath} sx={{ marginLeft: 'auto' }}>
          <LoginIcon fontSize="large" sx={{ color: COLORS.link }} />
        </Link>
      )}
    </>
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
      {pathname !== ADMIN_PATHS.Login && !pathname.includes('password') && (
        <Link
          component={RouterLink}
          to={homePath}
          variant="h5"
          underline="none"
          sx={{ display: 'flex', alignItems: 'center' }}
        >
          <HomeIcon fontSize="large" sx={{ color: COLORS.link }} />
        </Link>
      )}
      {children}
      {availableButtons}
    </Box>
  );
}
