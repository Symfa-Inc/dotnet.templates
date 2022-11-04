import { createTheme } from '@mui/material';
import { indigo, red } from '@mui/material/colors';

export const COLORS = {
  primary: indigo,
  secondary: red,
  error: '#f44336',
  warning: '#ff9800',
  info: '#2196f3',
  success: '#4caf50',
  link: '#3f51b5',
};

export const theme = createTheme({
  palette: {
    mode: 'light',
    primary: COLORS.primary,
    secondary: COLORS.secondary,
  },
});
