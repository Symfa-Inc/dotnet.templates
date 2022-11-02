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
      });
  },
});

export const { resetErrorState } = productSlice.actions;

export default productSlice.reducer;
