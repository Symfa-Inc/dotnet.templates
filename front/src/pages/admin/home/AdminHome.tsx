import { Header, BaseModal } from '@components/index';
import { Alert, Box, Snackbar } from '@mui/material';
import { productCategories } from '@utils/mockDatabase';
import { useEffect, useState } from 'react';
import { addProduct, fetchProducts, editProduct } from '@store/reducers/productSlice';
import { useAppDispatch, useAppSelector } from '@store/hooks';
import { Mode } from '@enums/index';
import { ProductsTable } from './components/table/ProductsTable';
import { SideBar } from './components/sideBar/SideBar';
import { ProductForm } from './components/productsForm/ProductsForm';

export function AdminHome() {
  const dispatch = useAppDispatch();
  const store = useAppSelector((state) => state.product);
  const [open, setOpen] = useState(false);
  const [mode, setMode] = useState('');
  const [modalOptions, setModalOptions] = useState({ title: '', btnText: '' });
  const [errorText, setErrorText] = useState('');
  const [newProduct, setNewProduct] = useState({});

  // This is just to show more data in the table, you can delete or adapt in production
  const productList = store?.products.map((item) => {
    const product = {
      id: item.id,
      name: item.name,
      description:
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore.',
    };
    return product;
  });

  const handleInputChange = (e: any) => {
    setNewProduct({
      ...newProduct,
      [e.target.name]: e.target.value,
    });
  };

  const openEditModal = (item: any) => {
    setOpen(true);
    setMode(Mode.Edit);
    setModalOptions({ title: 'Edit Product', btnText: 'Edit' });
    setNewProduct(item);
  };

  const openDeleteModal = (item: any) => {
    setOpen(true);
    setMode(Mode.Edit);
    setNewProduct(item);
  };

  const handleClose = () => {
    setErrorText('');
    setOpen(false);
  };

  const openAddProductModal = () => {
    setOpen(true);
    setMode(Mode.Add);
    setModalOptions({ title: 'Add Product', btnText: 'Add' });
    setNewProduct({});
  };

  function handleError(error: any) {
    setErrorText(error);
    setTimeout(() => {
      setErrorText('');
    }, 5000);
  }

  const handleSubmit = async () => {
    if (mode === Mode.Add) {
      try {
        await dispatch(addProduct(newProduct)).unwrap();
        setErrorText('');
        setOpen(false);
      } catch (err: any) {
        return handleError(err);
      }
    }
    if (mode === Mode.Edit) {
      try {
        await dispatch(editProduct(newProduct)).unwrap();
        setErrorText('');
        setOpen(false);
      } catch (err: any) {
        return handleError(err);
      }
    }
  };

  useEffect(() => {
    dispatch(fetchProducts());
  }, [dispatch]);

  return (
    <>
      <Header />
      <Box sx={{ display: 'flex', gap: '2rem', width: '100%' }}>
        <SideBar items={productCategories} openAddProductModal={openAddProductModal} />
        <ProductsTable list={productList} openModal={openEditModal} openDeleteModal={openDeleteModal} />
      </Box>
      <BaseModal
        open={open}
        handleClose={handleClose}
        body={<ProductForm handleInputChange={handleInputChange} newProduct={newProduct} />}
        btnAction={handleSubmit}
        btnText={modalOptions.btnText}
        title={modalOptions.title}
        bodyContainerStyle={{ flexDirection: 'column', gap: '1rem' }}
      />
      {!!errorText && (
        <Snackbar
          open={!!errorText}
          anchorOrigin={{
            vertical: 'top',
            horizontal: 'center',
          }}
        >
          <Alert severity="error" sx={{ width: '100%' }}>
            {errorText}
          </Alert>
        </Snackbar>
      )}
    </>
  );
}
