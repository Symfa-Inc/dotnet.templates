import { UserAdditionalFields } from '@features/auth/auth.interface';

export type ProfileFormProps = {
  profileData: UserAdditionalFields,
  onChange: (userFields: UserAdditionalFields) => void
}
