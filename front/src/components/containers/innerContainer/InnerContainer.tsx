import { Box } from '@mui/material';

export function InnerContainer({ children }: any) {
  return (
    <Box maxWidth="lg" sx={{ display: 'flex', flexDirection: 'column', width: '100%', alignItems: 'center' }}>
      {children}
    </Box>
  );
}
