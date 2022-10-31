import { Container } from '@mui/material';

export function MainContainer({ children }: any) {
  return (
    <Container
      component="main"
      fixed={false}
      maxWidth={false}
      sx={{
        display: 'flex',
        flexDirection: 'column',
        width: '100vw',
        minHeight: '100vh',
        alignItems: 'center',
        // bgcolor: '#FFFAFA',
        pt: 1.5,
        pb: 1.5,
      }}
    >
      {children}
    </Container>
  );
}
