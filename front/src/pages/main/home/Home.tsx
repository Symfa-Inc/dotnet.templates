import { Button, Typography, Grid, Container, Box } from '@mui/material';
import { PATHS } from '@router/paths';
import { RouterLink } from '@router/utils';
import { logout, selectUser } from '@store/reducers/authSlice';
import { useAppDispatch, useAppSelector } from '@store/hooks';
import { ItemCard } from '@components/itemCard/ItemCard';
import { items } from '@utils/mockDatabase';
import { Header } from '@components/header/Header';

export function Home() {
  const dispatch = useAppDispatch();
  const user = useAppSelector(selectUser);

  const handleLogout = () => {
    dispatch(logout());
  };

  const availableButtons = user?.userId ? (
    <Box>
      <Button component={RouterLink} to={PATHS.Profile} variant="contained">
        Profile
      </Button>
      <Button sx={{ ml: 2 }} onClick={handleLogout} variant="contained">
        Log out
      </Button>
    </Box>
  ) : (
    <Button component={RouterLink} to={PATHS.Login} variant="contained" sx={{ marginLeft: 'auto' }}>
      Login
    </Button>
  );

  return (
    <Container maxWidth="lg">
      <Header>
        {user?.userId && <Typography variant="h5">Welcome, {user.userName}</Typography>}
        {availableButtons}
      </Header>

      <Typography variant="h2" textAlign="center" mt="1rem" mb="2rem">
        Home Page
      </Typography>

      <Grid container spacing={2} maxWidth="1200px">
        {items.map((item) => (
          <Grid item xs={12} sm={6} md={4} key={item.title} display="flex" justifyContent="center">
            <ItemCard key={item.title} {...item} />
          </Grid>
        ))}
      </Grid>
    </Container>
  );
}
