import { Header } from '@components/index';
import { Box } from '@mui/material';
import { productCategories, productItems } from '@utils/mockDatabase';
import GlobalModal from '@components/modal/GlobalModal';
import { useState } from 'react';
import { ProductsTable } from './components/table/ProductsTable';
import { SideBar } from './components/sideBar/SideBar';

export function AdminHome() {
  const productList = productCategories.map((category) => {
    const products = productItems.filter((item) => item.category === category.id);
    return products;
  });
  const [open, setOpen] = useState(false);
  const [product, setProduct] = useState({});

  const openModal = (item: any) => {
    setOpen(true);
    setProduct(item);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const openAddProductModal = () => {
    setOpen(true);
    setProduct({});
  };

  return (
    <>
      <Header />
      <Box sx={{ display: 'flex', gap: '1rem' }}>
        <SideBar items={productCategories} openAddProductModal={openAddProductModal} />
        <ProductsTable list={productList[0]} openModal={openModal} />
      </Box>
      <GlobalModal open={open} handleClose={handleClose} product={product} />
    </>
  );
}
