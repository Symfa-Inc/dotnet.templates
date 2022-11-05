import { TextField } from '@mui/material';

export const ProductForm = ({ handleInputChange, newProduct }: any) => {
  return (
    <>
      <TextField
        name="name"
        fullWidth
        id="name"
        label="Name"
        autoFocus
        value={newProduct.name || ''}
        onChange={(event) => handleInputChange(event)}
      />
      <TextField
        name="category"
        fullWidth
        id="category"
        label="Category"
        autoFocus
        value={newProduct.category || ''}
        onChange={(event) => handleInputChange(event)}
      />
      <TextField
        multiline
        name="description"
        fullWidth
        id="description"
        label="Description"
        autoFocus
        value={newProduct.description || ''}
        onChange={(event) => handleInputChange(event)}
      />
    </>
  );
};
