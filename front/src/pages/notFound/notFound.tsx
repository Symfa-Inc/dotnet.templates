import { Container, Link, Typography } from '@mui/material';
import { RouterLink } from '@router/utils';

export function NotFound() {
  return (
    <Container
      maxWidth="xl"
      sx={{
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        height: '100vh',
        alignItems: 'center',
        gap: '1rem',
      }}
    >
      <Typography variant="h1">404</Typography>
      <Typography variant="h4">Oops ... page not found</Typography>
      <Link component={RouterLink} to="/">
        Go to home page
      </Link>
    </Container>
  );
}
