import { Header, GlobalModal } from '@components/index';
import { Box } from '@mui/material';
import { productCategories } from '@utils/mockDatabase';
import { useEffect, useState } from 'react';
import { addProduct, fetchProducts } from '@store/reducers/productSlice';
import { useAppDispatch, useAppSelector } from '@store/hooks';
import { Mode } from '@enums/index';
import { ProductsTable } from './components/table/ProductsTable';
import { SideBar } from './components/sideBar/SideBar';

export function AdminHome() {
  const dispatch = useAppDispatch();
  const store = useAppSelector((state) => state.product);
  console.log('store', store);

  useEffect(() => {
    dispatch(fetchProducts());
  }, [dispatch]);

  const productList = store?.products.map((item) => {
    const product = {
      id: item.id,
      name: item.name,
      description:
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore.',
    };
    return product;
  });
  const [open, setOpen] = useState(false);
  const [product, setProduct] = useState({});
  const [mode, setMode] = useState('');

  const openModal = (item: any) => {
    setOpen(true);
    setMode(Mode.Edit);
    setProduct(item);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const openAddProductModal = () => {
    setOpen(true);
    setMode(Mode.Add);
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
        <ProductsTable list={productList} openModal={openModal} />
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
