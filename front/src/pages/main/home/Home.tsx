import { Typography, Grid } from '@mui/material';
import { productItems } from '@utils/mockDatabase';
import { GlobalModal, ItemCard, Header } from '@components/index';
import { useEffect, useState } from 'react';
import { Mode } from '@enums/index';
import { useAppDispatch, useAppSelector } from '@store/hooks';
import { selectUser } from '@store/reducers/authSlice';
import { fetchProducts } from '@store/reducers/productSlice';

export function Home() {
  const [open, setOpen] = useState(false);
  const [product, setProduct] = useState({});
  const dispatch = useAppDispatch();
  const user = useAppSelector(selectUser);
  const store = useAppSelector((state) => state.product);
  const productListing = store.products;
  console.log('store', store);
  console.log('user', user);

  useEffect(() => {
    dispatch(fetchProducts());
  }, [dispatch]);

  const showDetail = (item: any) => {
    setOpen(true);
    setProduct(item);
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <>
      <Header />

      <Typography variant="h2" textAlign="center" mt="1rem" mb="2rem">
        Home Page
      </Typography>

      <Grid container spacing={2} justifyContent="center">
        {user.userId ? (
          <>
            {productListing?.length > 0 ? (
              productListing.map((item: any) => {
                return (
                  <Grid item xs={12} sm={6} md={4} key={item.id}>
                    <ItemCard {...item} showDetail={showDetail} />
                  </Grid>
                );
              })
            ) : (
              <Typography variant="h5" textAlign="center" mt="4rem" mb="2rem" width="100%">
                Welcome {user.userName}! You have no products in your store yet.
              </Typography>
            )}
          </>
        ) : (
          <>
            {productItems.map((item) => (
              <Grid item xs={12} sm={6} md={4} key={item.id} display="flex" justifyContent="center">
                <ItemCard {...item} showDetail={showDetail} />
              </Grid>
            ))}
          </>
        )}
      </Grid>

      <GlobalModal open={open} handleClose={handleClose} product={product} mode={Mode.Show} />
    </>
  );
}
