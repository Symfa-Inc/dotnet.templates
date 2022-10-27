import { UserAdditionalFields } from '@services/authServices/auth.interface';

export type ProfileFormProps = {
  profileData: UserAdditionalFields;
  onChange: (userFields: UserAdditionalFields) => void;
};
