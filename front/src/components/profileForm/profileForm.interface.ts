import { UserAdditionalFields } from '@pages/main/auth/auth.interface';

export type ProfileFormProps = {
  profileData: UserAdditionalFields;
  onChange: (userFields: UserAdditionalFields) => void;
};
