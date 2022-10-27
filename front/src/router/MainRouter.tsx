import { NoMatch, RequireAuth } from '@components/index';
import { Layout } from '@components/layout/layout';
import { ProviderCallback } from '@pages/auth/provider-callback';
import SignIn from '@pages/auth/sign-in';
import SignUp from '@pages/auth/sign-up';
import { Counter } from '@pages/home/home';
import { Profile } from '@pages/profile/profile';
import { Route, Routes } from 'react-router-dom';
import { PATHS } from './paths';

export function MainRouter() {
  return (
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
  );
}
