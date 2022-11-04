import { RequireAuth } from '@components/index';
import { Layout } from '@components/layout/layout';
import { AdminHome, Edit, AdminLogin } from '@pages/admin/index';
import { Login, SignUp, Home, PasswordReset } from '@pages/main/index';
import { Profile } from '@pages/main/profile/Profile';
import { NotFound } from '@pages/notFound/notFound';
import { ProviderCallback } from '@services/authServices/providerCallback';
import { Route, Routes } from 'react-router-dom';
import { PasswordRecovey } from '@pages/main/passwordRecovery/PasswordRecovery';
import { PATHS, ADMIN_PATHS } from './paths';

export function MainRouter() {
  return (
    <Routes>
      <Route element={<Layout />}>
        <Route path={PATHS.Home} element={<Home />} />
        <Route path={PATHS.Login} element={<Login />} />
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
        <Route path={PATHS.PasswordReset} element={<PasswordReset />} />
        <Route path={PATHS.Recovery} element={<PasswordRecovey />} />
        <Route
          path={ADMIN_PATHS.Home}
          element={
            <RequireAuth>
              <AdminHome />
            </RequireAuth>
          }
        />
        <Route path={ADMIN_PATHS.Login} element={<AdminLogin />} />
        <Route path={ADMIN_PATHS.Edit} element={<Edit />} />
        <Route
          path={ADMIN_PATHS.Profile}
          element={
            <RequireAuth>
              <Profile />
            </RequireAuth>
          }
        />
        <Route path={ADMIN_PATHS.PasswordReset} element={<PasswordReset />} />
        <Route path="*" element={<NotFound />} />
      </Route>
    </Routes>
  );
}
