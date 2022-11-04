import { UserAdditionalFields } from '@services/authServices/auth.interface';
import { selectSignUpError, selectUser, updateProfile } from '@store/reducers/authSlice';
import { Avatar, Card, Box, Typography, Grid, Button, Snackbar, Alert } from '@mui/material';
import { blue } from '@mui/material/colors';
import Container from '@mui/material/Container';
import { ProfileService } from '@services/profile.service';
import { useAppDispatch, useAppSelector } from '@store/hooks';
import dayjs from 'dayjs';
import _ from 'lodash';
import { useState } from 'react';
import { Header } from '@components/index';
import { ProfileForm } from './components/profileForm/profileForm';
import { ProfileModal } from './components/modal/ProfileModal';

export function Profile() {
  const user = useAppSelector(selectUser);
  const dispatch = useAppDispatch();
  const [open, setOpen] = useState(false);
  const [message, setMessage] = useState('');
  const error = useAppSelector(selectSignUpError);

  const profileFormData: UserAdditionalFields = _.omit(user, ['userId', 'userName', 'email']);
  const [profileForm, updateProfileForm] = useState(profileFormData);

  const validAdditionalForm = profileForm.dateOfBirth && dayjs(profileForm.dateOfBirth).isValid();

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    try {
      const profile = await ProfileService.updateProfile(profileForm);
      dispatch(updateProfile(profile.data));
      setMessage('Profile updated successfully');
      setTimeout(() => {
        setMessage('');
      }, 4000);
    } catch (err) {
      setMessage('');
      return err;
    }
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <>
      <ProfileModal open={open} handleClose={handleClose} />
      <Header />
      <Container maxWidth="sm">
        <Typography variant="h2" textAlign="center">
          Profile
        </Typography>
        <Card
          sx={{
            display: 'flex',
            flexDirection: 'column',
            mt: 2,
            p: 2,
            borderRadius: 2,
          }}
        >
          <Box display="flex" sx={{ p: 2 }}>
            <Button onClick={() => setOpen(true)}>
              <Avatar
                alt={user.userName}
                sx={{
                  width: '100px',
                  height: '100px',
                  bgcolor: blue[500],
                }}
                src={user.avatar}
              />
            </Button>
            <Box sx={{ display: 'flex', flexDirection: 'column', ml: 2 }}>
              <Typography variant="h4">{user.userName}</Typography>
              <Typography variant="h5">{user.email}</Typography>
            </Box>
          </Box>
          <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Box sx={{ mt: 2, pl: 2 }}>
                <ProfileForm profileData={profileForm} onChange={updateProfileForm} />
              </Box>
            </Grid>
            <Box sx={{ display: 'flex', justifyContent: 'center' }}>
              <Button type="submit" variant="contained" sx={{ mt: 3, mb: 2 }} disabled={!validAdditionalForm}>
                Edit Profile
              </Button>
            </Box>
          </Box>
        </Card>
      </Container>
      {!!error ||
        (!!message && (
          <Snackbar
            open={!!error || !!message}
            anchorOrigin={{
              vertical: 'top',
              horizontal: 'center',
            }}
            message={error}
            autoHideDuration={5000}
          >
            <Alert severity={error ? 'error' : 'success'} sx={{ width: '100%' }}>
              {error || message}
            </Alert>
          </Snackbar>
        ))}
    </>
  );
}
