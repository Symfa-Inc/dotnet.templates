import { Box } from '@mui/material';
import { RouterLink } from '@router/utils';

export function NoMatch() {
  return (
    <Box>
      <h2>Nothing to see here!</h2>
      <p>
        <RouterLink to="/">Go to the home page</RouterLink>
      </p>
    </Box>
  );
}
