import { Header } from '@components/header/Header';
import { Container, Grid, TextField, Typography, Box, Button } from '@mui/material';

export function PasswordReset() {
  return (
    <>
      <Header />

      <Container
        maxWidth="xl"
        sx={{
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',

          alignItems: 'center',
          gap: '1rem',
        }}
      >
        <Typography variant="h2">Recover Password</Typography>
        <Container
          component="div"
          maxWidth="xs"
          sx={{
            bgcolor: 'white',
            mt: 4,
            borderRadius: 2,
            boxShadow: 1,
            p: 1,
          }}
        >
          <Typography variant="body2">
            Enter your email address below and we will send you a link to recover your password
          </Typography>

          <Box component="form" noValidate onSubmit={() => ''} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={12}>
                <TextField required fullWidth id="email" label="Email Address" name="email" autoComplete="email" />
              </Grid>
            </Grid>
          </Box>
          <Button type="submit" fullWidth variant="contained" sx={{ mt: 3, mb: 2, borderRadius: 2 }}>
            Recover Password
          </Button>
        </Container>
      </Container>
    </>
  );
}
