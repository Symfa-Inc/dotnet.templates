import { Typography } from '@mui/material';
import { PATHS } from '@router/paths';
import { RouterLink } from '@router/utils';
import Link from '@mui/material/Link';

export function Copyright(props: any) {
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
