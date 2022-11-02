import { Typography, Grid } from '@mui/material';
import { productItems } from '@utils/mockDatabase';
import { GlobalModal, ItemCard, Header } from '@components/index';
import { useState } from 'react';
import { Mode } from '@enums/index';

export function Home() {
  const [open, setOpen] = useState(false);
  const [product, setProduct] = useState({});

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

      <Grid container spacing={2}>
        {productItems.map((item) => (
          <Grid item xs={12} sm={6} md={4} key={item.name} display="flex" justifyContent="center">
            <ItemCard key={item.name} {...item} showDetail={showDetail} />
          </Grid>
        ))}
      </Grid>
      <GlobalModal open={open} handleClose={handleClose} product={product} mode={Mode.Show} />
    </>
  );
}
