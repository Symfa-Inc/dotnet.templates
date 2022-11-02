import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { Button, Stack, Typography } from '@mui/material';

export function ProductsTable({ list, openModal }: any) {
  return (
    <Stack sx={{ gap: '1rem', width: '100%' }}>
      <Typography component="h2" variant="h3" textAlign="center">
        Products List
      </Typography>

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
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Stack>
  );
}
