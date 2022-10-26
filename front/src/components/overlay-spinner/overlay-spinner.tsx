import { Box, CircularProgress, CircularProgressProps } from '@mui/material';
import classes from './overlay-spinner.module.scss';

export const OverlaySpinner: React.FC<CircularProgressProps> = (props) => (
  <Box className={classes.wrapper} sx={{ borderColor: 'secondary.main', bgcolor: 'action.disabledBackground' }}>
    <CircularProgress size={60} {...props} />
  </Box>
);
