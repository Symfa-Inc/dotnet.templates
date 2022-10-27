import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { RouterLink } from '@router/utils';
import { PATHS } from '@router/paths';
import { OverlaySpinner } from '@components/index';
import { useAppDispatch, useAppSelector } from '@store/hooks';
import { ProcessState } from '@enums/progressState.enum';
import { Alert, Snackbar } from '@mui/material';
import { useEffect, useState } from 'react';
import { ProfileForm } from '@components/profileForm/profileForm';
import { useNavigate } from 'react-router-dom';
import dayjs from 'dayjs';
import {
  signupAction,
  selectLoadingState,
  selectPartlyRegistered,
  completeRegistrationAction,
  resetSignUpErrorState,
  selectSignUpError,
} from '../../../store/reducers/authSlice';
import { UserAdditionalFields, UserCredentials } from '../../../services/authServices/auth.interface';

function Copyright(props: any) {
  return (
    <Typography variant="body2" color="text.secondary" align="center" {...props}>
      {'Copyright © '}
      <Link component={RouterLink} to={PATHS.Home} color="inherit">
        Your Website
      </Link>{' '}
      {new Date().getFullYear()}.
    </Typography>
  );
}

export function SignUp() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const state = useAppSelector(selectLoadingState);
  const error = useAppSelector(selectSignUpError);
  const partlyRegistered = useAppSelector(selectPartlyRegistered);

  // console.log('Render SignUP');

  const [message, setMessage] = useState('');
  const [profileData, updateProfileData] = useState<UserAdditionalFields>({
    state: '',
    country: '',
    position: '',
    postalCode: '',
    dateOfBirth: new Date(),
    district: '',
  });

  const validAdditionalForm = profileData.dateOfBirth && dayjs(profileData.dateOfBirth).isValid();

  useEffect(() => {
    if (!message || !error) return;
    setTimeout(() => {
      if (message) {
        setMessage('');
      } else if (error) {
        dispatch(resetSignUpErrorState());
      }
    }, 3000);
  }, [dispatch, message, error]);

  // The sign up
  const registr = async (data: UserCredentials) => {
    try {
      await dispatch(signupAction(data)).unwrap();
      setMessage('User successfully created!');
    } catch (e) {
      console.error(e);
    }
  };

  // Create the user profile
  const finishRegistration = async (data: UserAdditionalFields) => {
    // console.log('Complete reg/dispatch');
    try {
      await dispatch(completeRegistrationAction(data)).unwrap();
      navigate(PATHS.Home);
    } catch (e) {
      console.error(e);
    }
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const form = new FormData(event.currentTarget);

    if (!partlyRegistered) {
      const data = {
        email: form.get('email')!.toString(),
        password: form.get('password')!.toString(),
        username: form.get('name')!.toString(),
      };
      registr(data);
    } else {
      finishRegistration(profileData);
    }
  };

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
            Sign up
          </Typography>
          <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
            {!partlyRegistered && (
              <Box>
                <Grid container spacing={2}>
                  <Grid item xs={12}>
                    <TextField
                      autoComplete="given-name"
                      name="name"
                      required
                      fullWidth
                      id="name"
                      label="User Name"
                      autoFocus
                    />
                  </Grid>
                  <Grid item xs={12}>
                    <TextField required fullWidth id="email" label="Email Address" name="email" autoComplete="email" />
                  </Grid>
                  <Grid item xs={12}>
                    <TextField
                      required
                      fullWidth
                      name="password"
                      label="Password"
                      type="password"
                      id="password"
                      autoComplete="new-password"
                    />
                  </Grid>
                </Grid>
              </Box>
            )}
            {partlyRegistered && (
              <Box mt={2}>
                <ProfileForm profileData={profileData} onChange={updateProfileData} />
              </Box>
            )}
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              disabled={partlyRegistered && !validAdditionalForm}
            >
              {!partlyRegistered ? 'Sign Up' : 'Complete registration'}
            </Button>
            <Grid container justifyContent="flex-end">
              <Grid item>
                <Link component={RouterLink} to={PATHS.SignIn} variant="body2">
                  Already have an account? Sign in
                </Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
        <Copyright sx={{ mt: 5, mb: 4 }} />
        {state === ProcessState.Loading && <OverlaySpinner />}
      </Container>
      {(!!message || !!error) && (
        <Snackbar
          open={!!message || !!error}
          anchorOrigin={{
            vertical: 'bottom',
            horizontal: 'center',
          }}
        >
          <Alert severity={error ? 'warning' : 'success'} sx={{ width: '100%' }}>
            {error || message}
          </Alert>
        </Snackbar>
      )}
    </>
  );
}
