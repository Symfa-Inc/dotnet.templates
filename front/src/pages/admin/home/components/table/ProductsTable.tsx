import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { Button, IconButton, Stack, Typography } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import { BaseModal } from '@components/baseModal/BaseModal';
import { deleteProduct } from '@store/reducers/productSlice';
import { useAppDispatch } from '@store/hooks';
import { useState } from 'react';

export function ProductsTable({ list, openModal }: any) {
  const dispatch = useAppDispatch();

  const [open, setOpen] = useState(false);
  const [product, setProduct] = useState({ id: 0, name: '' });

  const deleteProductHandler = () => {
    dispatch(deleteProduct(product?.id));
    setOpen(false);
  };

  return (
    <>
      <Stack sx={{ gap: '1rem', width: '100%' }}>
        <Typography component="h2" variant="h3" textAlign="center">
          Products List
        </Typography>
        {list?.length > 0 ? (
          <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
              <TableBody>
                {list.map((item: any) => (
                  <TableRow key={item.name} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                    <TableCell align="left">
                      <Typography variant="h6">{item.name}</Typography>
                    </TableCell>
                    <TableCell align="center">
                      {' '}
                      <Typography variant="body2">{item.description.substring(0, 40)}...</Typography>
                    </TableCell>

                    <TableCell align="right">
                      <Button variant="contained" onClick={() => openModal(item)}>
                        Edit
                      </Button>
                    </TableCell>
                    <TableCell align="right">
                      <IconButton
                        aria-label="delete"
                        onClick={() => {
                          setProduct(item);
                          setOpen(true);
                        }}
                      >
                        <DeleteIcon sx={{ color: '#F44336' }} fontSize="large" />
                      </IconButton>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        ) : (
          <Typography variant="h6" textAlign="center">
            {' '}
            No products found
          </Typography>
        )}
      </Stack>
      <BaseModal
        open={open}
        handleClose={() => setOpen(false)}
        deleteAction={deleteProductHandler}
        title="Delete Product"
        btnText="Delete"
        body={
          <Typography variant="body1" textAlign="center">
            Are you sure you want to delete the product <b>{product.name}</b>?
          </Typography>
        }
        bodyContainerStyle={{ justifyContent: 'center' }}
      />
    </>
  );
}
