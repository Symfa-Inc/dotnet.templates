import { Header, GlobalModal } from '@components/index';
import { Box } from '@mui/material';
import { productCategories, productItems } from '@utils/mockDatabase';
import { useEffect, useState } from 'react';
import { addProduct, fetchProducts } from '@store/reducers/productSlice';
import { useAppDispatch, useAppSelector } from '@store/hooks';
import { ProductsTable } from './components/table/ProductsTable';
import { SideBar } from './components/sideBar/SideBar';

export function AdminHome() {
  const dispatch = useAppDispatch();
  const store = useAppSelector((state) => state.product);
  console.log('store', store);

  useEffect(() => {
    dispatch(fetchProducts());
  }, [dispatch]);

  const productList = productCategories.map((category) => {
    const products = productItems.filter((item) => item.category === category.id);
    return products;
  });
  const [open, setOpen] = useState(false);
  const [product, setProduct] = useState({});
  const [mode, setMode] = useState('');

  const openModal = (item: any) => {
    setOpen(true);
    setMode('edit');
    setProduct(item);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const openAddProductModal = () => {
    setOpen(true);
    setMode('add');
    setProduct({});
  };

  const handleAddProduct = (newProduct: any) => {
    dispatch(addProduct(newProduct));
    setOpen(false);
  };

  return (
    <>
      <Header />
      <Box sx={{ display: 'flex', gap: '2rem', width: '100%' }}>
        <SideBar items={productCategories} openAddProductModal={openAddProductModal} />
        <ProductsTable list={productList[0]} openModal={openModal} />
      </Box>
      <GlobalModal
        open={open}
        handleClose={handleClose}
        product={product}
        mode={mode}
        handleSubmit={handleAddProduct}
      />
    </>
  );
}
