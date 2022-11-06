import { CssBaseline } from '@mui/material';
import { ThemeProvider } from '@emotion/react';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers';
import dayjs from 'dayjs';
import isSameOrAfter from 'dayjs/plugin/isSameOrAfter';
import isSameOrBefore from 'dayjs/plugin/isSameOrBefore';
import utc from 'dayjs/plugin/utc';
import isoWeek from 'dayjs/plugin/isoWeek';
import duration from 'dayjs/plugin/duration';
import isBetween from 'dayjs/plugin/isBetween';
import weekday from 'dayjs/plugin/weekday';
import './services/apiHttpInterceptors';
import { MainRouter } from '@router/MainRouter';
import { MainContainer, InnerContainer } from '@components/index';
import { theme } from '@styles/theme';

dayjs.extend(utc);
dayjs.extend(duration);
dayjs.extend(isoWeek);
dayjs.extend(isBetween);
dayjs.extend(isSameOrAfter);
dayjs.extend(isSameOrBefore);
dayjs.extend(weekday);

function App() {
  return (
    <ThemeProvider theme={theme}>
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <CssBaseline />
        <MainContainer>
          <InnerContainer>
            <MainRouter />
          </InnerContainer>
        </MainContainer>
      </LocalizationProvider>
    </ThemeProvider>
  );
}

export default App;
