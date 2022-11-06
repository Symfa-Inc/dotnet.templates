import { createTheme } from '@mui/material';
import { COLORS } from './colorPalette';

export const theme = createTheme({
  palette: {
    mode: 'light',
    primary: COLORS.primary,
    secondary: COLORS.secondary,
    error: COLORS.error,
    warning: COLORS.warning,
    info: COLORS.info,
    success: COLORS.success,
  },
});
