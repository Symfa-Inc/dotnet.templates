import { Header } from '@components/header/Header';
import { OverlaySpinner } from '@components/index';
import { ProcessState } from '@enums/progressState.enum';
import { Container, Grid, TextField, Typography, Box, Button } from '@mui/material';
import { useAppDispatch, useAppSelector } from '@store/hooks';
import { forgotPasswordAction, selectLoadingState } from '@store/reducers/authSlice';
import { useState } from 'react';

export function PasswordReset() {
  const dispatch = useAppDispatch();
  const state = useAppSelector(selectLoadingState);
  const [email, setEmail] = useState('');
  const [successMessage, setSuccessMessage] = useState('');

  const handleReset = async (e: any) => {
    e.preventDefault();
    try {
      await dispatch(forgotPasswordAction(email)).unwrap();
      setSuccessMessage(
        'Check your email for a link to reset your password. If it doesn’t appear within a few minutes, check your spam folder.',
      );
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <>
      {state === ProcessState.Loading && <OverlaySpinner />}
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
            p: 4,
          }}
        >
          {!successMessage ? (
            <>
              <Typography variant="body2">
                Enter your email address below and we will send you a link to recover your password
              </Typography>

              <Box sx={{ mt: 3 }}>
                <Grid container spacing={2}>
                  <Grid item xs={12}>
                    <TextField
                      required
                      fullWidth
                      id="email"
                      label="Email Address"
                      name="email"
                      autoComplete="email"
                      onChange={(e) => setEmail(e.target.value)}
                    />
                  </Grid>
                </Grid>
              </Box>
              <Button
                type="submit"
                onClick={(e) => handleReset(e)}
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2, borderRadius: 2 }}
              >
                Recover Password
              </Button>
            </>
          ) : (
            <Typography variant="body1">{successMessage}</Typography>
          )}
        </Container>
      </Container>
    </>
  );
}
