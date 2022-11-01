import { useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';

import Modal from '@mui/material/Modal';
import { TextField, Typography } from '@mui/material';

const style = {
  display: 'flex',
  flexDirection: 'column',
  gap: '1rem',
  position: 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 400,
  bgcolor: 'background.paper',
  borderRadius: 2,
  boxShadow: 24,
  p: 6,
};

export function GlobalModal({ open, handleClose, product, mode }: any) {
  const [newProduct, setNewProduct] = useState({
    ...product,
  });

  const modeMapper = {
    title: {
      add: 'Add Product',
      edit: 'Edit Product',
      show: 'Product Detail',
    },
    button: {
      add: 'Add',
      edit: 'Save',
      show: 'Close',
    },
  };

  const handleInputChange = (e: any) => {
    setNewProduct({
      ...newProduct,
      [e.target.name]: e.target.value,
    });
  };

  const sendChanges = () => {
    handleClose();
  };

  useEffect(() => {
    setNewProduct({ ...product });
  }, [product]);

  return (
    <Box>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style} component="form">
          <Typography variant="h4">{modeMapper.title[mode as keyof typeof modeMapper.title]}</Typography>
          {mode !== 'show' ? (
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
          ) : (
            <>
              <Typography variant="h6"> {newProduct.name}</Typography>

              <Typography variant="body2">{newProduct.description}</Typography>
            </>
          )}

          <Button onClick={() => (mode === 'show' ? handleClose() : sendChanges())} variant="contained">
            {modeMapper.button[mode as keyof typeof modeMapper.button]}
          </Button>
        </Box>
      </Modal>
    </Box>
  );
}
