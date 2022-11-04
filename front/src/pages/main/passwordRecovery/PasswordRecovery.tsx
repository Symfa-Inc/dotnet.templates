import { Container, Grid, TextField, Typography, Box, Button, Snackbar, Alert } from '@mui/material';
import { useAppDispatch, useAppSelector } from '@store/hooks';
import { resetPasswordAction, resetSignUpErrorState, selectSignUpError } from '@store/reducers/authSlice';
import { useEffect, useState } from 'react';
import { useSearchParams, useNavigate } from 'react-router-dom';

export function PasswordRecovey() {
  const dispatch = useAppDispatch();
  const error = useAppSelector(selectSignUpError);
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [searchParams] = useSearchParams();
  const token = searchParams.get('token');
  const email = searchParams.get('userEmail');
  const navigate = useNavigate();

  const credentials = {
    token,
    password,
    email,
  };

  const handleReset = async (e: any) => {
    e.preventDefault();
    try {
      await dispatch(resetPasswordAction(credentials)).unwrap();
      navigate('/login');
    } catch (err) {
      console.log('Error reseting the password. ', err);
    }
  };

  useEffect(() => {
    if (!error) return;
    setTimeout(() => {
      dispatch(resetSignUpErrorState());
    }, 4000);
  }, [dispatch, error]);

  function validatePassword() {
    return password === confirmPassword;
  }

  return (
    <>
      <Container
        maxWidth="xl"
        sx={{
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
          mt: 10,
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
          <Typography variant="h5" textAlign="center">
            Enter your new password below
          </Typography>

          <Box sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="new-password"
                  label="New password"
                  type="password"
                  name="new-password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  error={!validatePassword()}
                  required
                  fullWidth
                  id="confirm-password"
                  label="Confirm password"
                  name="confirm-password"
                  type="password"
                  value={confirmPassword}
                  onChange={(e) => setConfirmPassword(e.target.value)}
                  helperText={!validatePassword() && 'Passwords do not match'}
                />
              </Grid>
            </Grid>
          </Box>
          <Button
            type="submit"
            onClick={(e) => handleReset(e)}
            disabled={!validatePassword()}
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2, borderRadius: 2 }}
          >
            Recover Password
          </Button>
        </Container>
      </Container>
      {!!error && (
        <Snackbar
          open={!!error}
          anchorOrigin={{
            vertical: 'top',
            horizontal: 'center',
          }}
          message={error}
        >
          <Alert severity={error ? 'error' : 'success'} sx={{ width: '100%' }}>
            {error}
          </Alert>
        </Snackbar>
      )}
    </>
  );
}
