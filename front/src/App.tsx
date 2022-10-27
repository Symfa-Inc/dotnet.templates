import SignIn from '@pages/auth/sign-in';
import SignUp from '@pages/auth/sign-up';
import { Counter } from '@pages/home/home';
import './App.scss';
import { Route, Routes } from 'react-router-dom';
import { Container, createTheme, CssBaseline } from '@mui/material';
import { ThemeProvider } from '@emotion/react';
import { Profile } from '@pages/profile/profile';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { RequireAuth } from '@components/require-auth/require-auth';
import { NoMatch } from '@components/no-match-route/no-match';

import dayjs from 'dayjs';
import isSameOrAfter from 'dayjs/plugin/isSameOrAfter';
import isSameOrBefore from 'dayjs/plugin/isSameOrBefore';
import utc from 'dayjs/plugin/utc';
import isoWeek from 'dayjs/plugin/isoWeek';
import duration from 'dayjs/plugin/duration';
import isBetween from 'dayjs/plugin/isBetween';
import weekday from 'dayjs/plugin/weekday';
import { Layout } from '@components/layout/layout';
import { ProviderCallback } from '@pages/auth/provider-callback';
import { PATHS } from './router/paths';
import './services/api-http-interceptors';

dayjs.extend(utc);
dayjs.extend(duration);
dayjs.extend(isoWeek);
dayjs.extend(isBetween);
dayjs.extend(isSameOrAfter);
dayjs.extend(isSameOrBefore);
dayjs.extend(weekday);

function App() {
  const theme = createTheme({});
  return (
    <ThemeProvider theme={theme}>
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <Container
          component="main"
          fixed={false}
          maxWidth={false}
          sx={{
            display: 'flex',
            bgcolor: 'grey.100',
            pt: 0.5,
          }}
        >
          <CssBaseline />
          <Routes>
            <Route element={<Layout />}>
              <Route path={PATHS.Home} element={<Counter />} />
              <Route path={PATHS.SignIn} element={<SignIn />} />
              <Route path={PATHS.SignUp} element={<SignUp />} />
              <Route path={PATHS.ProviderAuthCallBack} element={<ProviderCallback />} />
              <Route
                path={PATHS.Profile}
                element={
                  <RequireAuth>
                    <Profile />
                  </RequireAuth>
                }
              />
              <Route path="*" element={<NoMatch />} />
            </Route>
          </Routes>
        </Container>
      </LocalizationProvider>
    </ThemeProvider>
  );
}

export default App;
