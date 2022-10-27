import { userManager } from './authSlice';

export function ProviderCallback() {
  if (window.location.href.includes('authCallback')) {
    userManager.signinPopupCallback();
  }

  return <></>;
}
