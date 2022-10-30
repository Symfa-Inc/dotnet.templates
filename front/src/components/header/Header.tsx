import { Box } from '@mui/material';

export function Header({ children }: any) {
  return <Box sx={{ width: '100%', display: 'flex', justifyContent: 'space-between', gap: '1rem' }}>{children}</Box>;
}
