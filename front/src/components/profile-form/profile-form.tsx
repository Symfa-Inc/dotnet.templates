import { Grid, TextField, TextFieldProps } from '@mui/material';
import { DatePicker } from '@mui/x-date-pickers';
import dayjs from 'dayjs';
import { ProfileFormProps } from './profile-form.interface';

export const ProfileForm: React.FC<ProfileFormProps> = ({ profileData, onChange }) => {
  const {
    position, postalCode, dateOfBirth, district, country, state,
  } = profileData;

  // console.log('Render form');

  const dateValidation = {
    valid: dateOfBirth === undefined || dayjs(dateOfBirth).isValid(),
    message: '',
  };

  if (!dateValidation.valid) {
    dateValidation.message = dateOfBirth === null ? 'Date of birth is required' : 'Date is invalid';
  }

  const handleChanges = <T extends keyof ProfileFormProps['profileData']>(
    value: ProfileFormProps['profileData'][T],
    field: T,
  ) => {
    const updatedUser = {
      ...profileData,
      [field]: value,
    };

    // console.log('Update form / setState');

    onChange(updatedUser);
  };

  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        <DatePicker
          openTo="year"
          views={['year', 'month', 'day']}
          label="Year, month and date"
          value={dateOfBirth}
          autoFocus
          onChange={(newValue) => handleChanges<'dateOfBirth'>(newValue, 'dateOfBirth')}
          renderInput={(params: TextFieldProps) => (
            <TextField {...params} error={!dateValidation.valid} required helperText={dateValidation.message} fullWidth />
          )}
        />
      </Grid>
      <Grid item xs={12}>
        <TextField
          name="country"
          fullWidth
          id="country"
          label="Country"
          autoFocus
          value={country}
          onChange={(event) => handleChanges<'country'>(event.target.value, 'country')}
        />
      </Grid>
      <Grid item xs={12} sm={6}>
        <TextField
          name="state"
          fullWidth
          id="state"
          label="State"
          value={state}
          onChange={(event) => handleChanges<'state'>(event.target.value, 'state')}
        />
      </Grid>
      <Grid item xs={12} sm={6}>
        <TextField
          name="postal_code"
          fullWidth
          id="postal_code"
          label="Postal Code"
          value={postalCode}
          onChange={(event) => handleChanges<'postalCode'>(event.target.value, 'postalCode')}
        />
      </Grid>
      <Grid item xs={12} sm={6}>
        <TextField
          fullWidth
          id="district"
          label="District"
          name="district"
          value={district}
          onChange={(event) => handleChanges<'district'>(event.target.value, 'district')}
        />
      </Grid>
      <Grid item xs={12} sm={6}>
        <TextField
          fullWidth
          name="position"
          label="Position"
          id="position"
          value={position}
          onChange={(event) => handleChanges<'position'>(event.target.value, 'position')}
        />
      </Grid>
    </Grid>
  );
};
