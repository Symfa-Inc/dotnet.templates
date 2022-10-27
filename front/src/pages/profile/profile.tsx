import { ProfileForm } from '@components/profile-form/profile-form';
import { UserAdditionalFields } from '@pages/auth/auth.interface';
import { selectUser, updateProfile } from '@pages/auth/authSlice';
import { Avatar, Card, Box, Typography, Grid, Button } from '@mui/material';
import { blue } from '@mui/material/colors';
import Container from '@mui/material/Container';
import { ProfileService } from '@services/profile.service';
import { useAppDispatch, useAppSelector } from '@store/hooks';
import dayjs from 'dayjs';
import _ from 'lodash';
import { useState } from 'react';

export function Profile() {
  const user = useAppSelector(selectUser);
  const dispatch = useAppDispatch();

  const profileFormData: UserAdditionalFields = _.omit(user, ['userId', 'userName', 'email']);
  const [profileForm, updateProfileForm] = useState(profileFormData);

  const validAdditionalForm = profileForm.dateOfBirth && dayjs(profileForm.dateOfBirth).isValid();

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const profile = await ProfileService.updateProfile(profileForm);
    dispatch(updateProfile(profile.data));
  };

  return (
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
          <Avatar
            alt={user.userName}
            sx={{
              width: '100px',
              height: '100px',
              bgcolor: blue[500],
            }}
          />
          <Box sx={{ display: 'flex', flexDirection: 'column', ml: 2 }}>
            <Typography variant="h4">{user.userName}</Typography>
            <Typography variant="h5">{user.email}</Typography>
          </Box>
        </Box>
        <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
          <Grid container spacing={2}>
            {/* <Grid item xs={12}>
              <TextField
                autoComplete="given-name"
                name="username"
                required
                fullWidth
                id="username"
                label="User Name"
                autoFocus
                defaultValue={user.username}
              />
            </Grid> */}
            {/* <Grid item xs={12}>
              <DatePicker
                openTo="year"
                views={['year', 'month', 'day']}
                label="Year, month and date"
                value={dateOfBirth}
                onChange={(newValue) => {
                  setDateOfBirth(newValue as Date);
                }}
                renderInput={(params: TextFieldProps) => <TextField {...params} helperText={null} fullWidth />}
              />
            </Grid> */}
            {/* <Grid item xs={12}>
              <TextField
                required
                fullWidth
                id="email"
                label="Email Address"
                name="email"
                autoComplete="email"
                defaultValue={user.email}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                required
                fullWidth
                name="password"
                label="Password"
                type="password"
                id="password"
                autoComplete="current-password"
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                required
                fullWidth
                name="password_confirm"
                label="Confirm Password"
                type="password"
                id="password_confirm"
                autoComplete="new-password"
              />
            </Grid> */}
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
  );
}
