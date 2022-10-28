import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { PATHS } from '@router/paths';
import { RouterLink } from '@router/utils';
import { useAppDispatch, useAppSelector } from '@store/hooks';
import { Alert, Snackbar } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import {
  resetSignInErrorState,
  signinAction,
  selectSignInError,
  signinWithProviderAction,
} from '@store/reducers/authSlice';
import { Copyright } from '@components/index';
import styles from './Login.module.scss';

export function Login() {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const error = useAppSelector(selectSignInError);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const form = new FormData(event.currentTarget);

    const params = {
      username: form.get('username')!.toString(),
      password: form.get('password')!.toString(),
    };

    try {
      await dispatch(signinAction(params)).unwrap();
      navigate(PATHS.Home);
    } catch (e) {
      if (typeof e === 'string') {
        if (e.includes('UserProfileNotFoundException')) {
          navigate(PATHS.SignUp);
        }
      }
    }
  };

  const signWithProvider = (provider: string) => {
    dispatch(signinWithProviderAction(provider));
  };

  useEffect(() => {
    if (!error) return;

    setTimeout(() => {
      if (error) {
        dispatch(resetSignInErrorState());
      }
    }, 3000);
  }, [dispatch, error]);

  return (
    <>
      <Container
        component="div"
        maxWidth="xs"
        sx={{
          bgcolor: 'white',
          mt: 4,
          borderRadius: 2,
          boxShadow: 1,
        }}
      >
        <Box
          sx={{
            marginTop: 3,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Sign in
          </Typography>
          <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="username"
              label="User Name"
              name="username"
              autoComplete="given-name"
              autoFocus
            />
            <TextField
              margin="normal"
              required
              fullWidth
              name="password"
              label="Password"
              type="password"
              id="password"
              autoComplete="current-password"
            />
            <FormControlLabel control={<Checkbox value="remember" color="primary" />} label="Remember me" />
            <Button type="submit" fullWidth variant="contained" sx={{ mt: 3, mb: 2, borderRadius: 2 }}>
              Sign In
            </Button>
            <Box alignItems="center" display="flex" justifyContent="center" sx={{ mb: 2 }}>
              <Button className={`${styles['g-btn']} ${styles.btn}`} onClick={() => signWithProvider('Google')} />
              <Button className={styles['tw-btn']} onClick={() => signWithProvider('Google')} />
              <Button className={styles['fb-btn']} onClick={() => signWithProvider('Google')} />
            </Box>
            <Grid container>
              <Grid item xs>
                <Link component={RouterLink} to={PATHS.PasswordReset} variant="body2">
                  Forgot password?
                </Link>
              </Grid>
              <Grid item>
                <Link component={RouterLink} to={PATHS.SignUp} variant="body2">
                  Don&apos;t have an account? Sign Up
                </Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
        <Copyright sx={{ mt: 8, mb: 4 }} />
      </Container>
      {!!error && (
        <Snackbar
          open={!!error}
          anchorOrigin={{
            vertical: 'bottom',
            horizontal: 'center',
          }}
        >
          <Alert severity="warning" sx={{ width: '100%' }}>
            {error}
          </Alert>
        </Snackbar>
      )}
    </>
  );
}
