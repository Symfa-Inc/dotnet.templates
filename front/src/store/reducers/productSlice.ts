import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { apiHttpService } from '@services/apiHttp.service';
import { handleError } from '@services/errorParser';

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
  return response.data;
});

export const addProduct = createAsyncThunk('products/addProduct', async (product: any, { rejectWithValue }) => {
  try {
    const response = await apiHttpService.post('/product', product);
    return response.data;
  } catch (err) {
    return handleError(err, rejectWithValue);
  }
});

export const editProduct = createAsyncThunk('products/editProduct', async (product: any, { rejectWithValue }) => {
  try {
    const response = await apiHttpService.put(`/product/${product.id}`, product);
    return response.data;
  } catch (err) {
    return handleError(err, rejectWithValue);
  }
});

export const deleteProduct = createAsyncThunk('products/deleteProduct', async (id: number, { rejectWithValue }) => {
  try {
    const response = await apiHttpService.delete(`/product?productId=${id}`);
    return response.data;
  } catch (err) {
    return handleError(err, rejectWithValue);
  }
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
        state.status = 'idle';
        state.products.push(action.payload);
        state.error = null;
      })
      .addCase(addProduct.rejected, (state, action) => {
        state.status = 'failed';
        state.error = (action.payload as string) || '';
      })
      .addCase(editProduct.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(editProduct.fulfilled, (state, action) => {
        state.status = 'idle';
        const index = state.products.findIndex((product) => product.id === action.payload.id);
        state.products[index] = action.payload;
        state.error = null;
      })
      .addCase(editProduct.rejected, (state, action) => {
        state.status = 'failed';
        state.error = (action.payload as string) || '';
      })
      .addCase(deleteProduct.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(deleteProduct.fulfilled, (state, action) => {
        state.status = 'idle';
        state.products = state.products.filter((product) => product.id !== action.meta?.arg);
        state.error = null;
      })
      .addCase(deleteProduct.rejected, (state, action) => {
        state.status = 'failed';
        state.error = (action.payload as string) || '';
      });
  },
});

export const { resetErrorState } = productSlice.actions;

export const selectAddProductStatus = (state: any) => state.status;
export const selectAddProductError = (state: any) => state.error;

export default productSlice.reducer;
