import { Button, Typography, Grid, Box } from '@mui/material';
import { PATHS } from '@router/paths';
import { RouterLink } from '@router/utils';
import { logout, selectUser } from '@store/reducers/authSlice';
import { useAppDispatch, useAppSelector } from '@store/hooks';
import { productItems } from '@utils/mockDatabase';
import { GlobalModal, ItemCard, Header } from '@components/index';
import { useState } from 'react';

export function Home() {
  const dispatch = useAppDispatch();
  const user = useAppSelector(selectUser);
  const [open, setOpen] = useState(false);
  const [product, setProduct] = useState({});

  const handleLogout = () => {
    dispatch(logout());
  };

  const showDetail = (item: any) => {
    setOpen(true);
    setProduct(item);
  };

  const handleClose = () => {
    setOpen(false);
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
    <>
      <Header>
        {user?.userId && <Typography variant="h5">Welcome, {user.userName}</Typography>}
        {availableButtons}
      </Header>

      <Typography variant="h2" textAlign="center" mt="1rem" mb="2rem">
        Home Page
      </Typography>

      <Grid container spacing={2}>
        {productItems.map((item) => (
          <Grid item xs={12} sm={6} md={4} key={item.name} display="flex" justifyContent="center">
            <ItemCard key={item.name} {...item} showDetail={showDetail} />
          </Grid>
        ))}
      </Grid>
      <GlobalModal open={open} handleClose={handleClose} product={product} mode="show" />
    </>
  );
}
