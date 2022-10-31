import { Box, Link } from '@mui/material';
import { ADMIN_PATHS, PATHS } from '@router/paths';
import { RouterLink } from '@router/utils';
import HomeIcon from '@mui/icons-material/Home';
import { useLocation } from 'react-router-dom';
import { useEffect, useState } from 'react';

export function Header({ children }: any) {
  const { pathname } = useLocation();
  const [homePath, sethomePath] = useState(PATHS.Home);

  useEffect(() => {
    if (pathname.includes('admin')) {
      sethomePath(ADMIN_PATHS.Home);
    } else {
      sethomePath(PATHS.Home);
    }
  }, [pathname]);

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
    </Box>
  );
}
