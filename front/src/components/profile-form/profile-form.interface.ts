import { UserAdditionalFields } from '@pages/auth/auth.interface';

export type ProfileFormProps = {
  profileData: UserAdditionalFields;
  onChange: (userFields: UserAdditionalFields) => void;
};
