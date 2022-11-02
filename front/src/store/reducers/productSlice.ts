import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { apiHttpService } from '@services/apiHttp.service';

interface ProductState {
  products: any[];
  status: 'idle' | 'loading' | 'failed';
  error: string | null;
}

const initialState: ProductState = {
  products: [],
  status: 'idle',
  error: null,
};

export const fetchProducts = createAsyncThunk('products/fetchProducts', async () => {
  const response = await apiHttpService.get('/product');
  console.log('response', response);
  return response.data;
});

export const addProduct = createAsyncThunk('products/addProduct', async (product: any) => {
  const response = await apiHttpService.post('/product', product);
  console.log('response', response);
  return response.data;
});

export const editProduct = createAsyncThunk('products/editProduct', async (product: any) => {
  const response = await apiHttpService.put(`/product/${product.id}`, product);
  console.log('response', response);
  return response.data;
});

export const deleteProduct = createAsyncThunk('products/deleteProduct', async (id: number) => {
  const response = await apiHttpService.delete(`/product?productId=${id}`);
  console.log('response for delete', response);
  return response.data;
});

export const productSlice = createSlice({
  name: 'products',
  initialState,
  reducers: {
    resetErrorState: (state) => {
      state.status = 'idle';
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchProducts.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(fetchProducts.fulfilled, (state, action) => {
        state.status = 'idle';
        state.products = action.payload;
      })
      .addCase(fetchProducts.rejected, (state, action) => {
        state.status = 'failed';
        state.error = (action.error.message as string) || '';
      })
      .addCase(addProduct.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(addProduct.fulfilled, (state, action) => {
        console.log('action', action);
        state.status = 'idle';
        state.products.push(action.payload);
      })
      .addCase(addProduct.rejected, (state, action) => {
        state.status = 'failed';
        state.error = (action.error.message as string) || '';
      })
      .addCase(editProduct.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(editProduct.fulfilled, (state, action) => {
        console.log('action', action);
        state.status = 'idle';
        const index = state.products.findIndex((product) => product.id === action.payload.id);
        state.products[index] = action.payload;
      })
      .addCase(editProduct.rejected, (state, action) => {
        state.status = 'failed';
        state.error = (action.error.message as string) || '';
      })
      .addCase(deleteProduct.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(deleteProduct.fulfilled, (state, action) => {
        console.log('action', action);
        state.status = 'idle';
        state.products = state.products.filter((product) => product.id !== action.meta?.arg);
      })
      .addCase(deleteProduct.rejected, (state, action) => {
        state.status = 'failed';
        state.error = (action.error.message as string) || '';
      });
  },
});

export const { resetErrorState } = productSlice.actions;

export default productSlice.reducer;
