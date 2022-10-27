import { Stack, Button, Container, Typography, Box } from '@mui/material';
import { PATHS } from '@router/paths';
import { RouterLink } from '@router/utils';
import { logout, selectUser } from '@store/reducers/authSlice';
import { useAppDispatch, useAppSelector } from '../../../store/hooks';

export function Home() {
  const dispatch = useAppDispatch();
  const user = useAppSelector(selectUser);

  const handleLogout = () => {
    dispatch(logout());
  };

  const availableButtons = user?.userId ? (
    <>
      <Button component={RouterLink} to={PATHS.Profile} variant="contained">
        Profile
      </Button>
      <Button sx={{ ml: 2 }} onClick={handleLogout} variant="contained">
        Log out
      </Button>
    </>
  ) : (
    <Button component={RouterLink} to={PATHS.SignIn} variant="contained">
      Sign In
    </Button>
  );

  return (
    <Container maxWidth="md">
      <Stack spacing={2} sx={{ mt: 6, alignItems: 'center' }}>
        <Typography variant="h2">Home Page</Typography>
        {user?.userId && <Typography variant="h4">Welcome, {user.userName}</Typography>}
        <Box display="flex">{availableButtons}</Box>
      </Stack>
    </Container>
  );
}
