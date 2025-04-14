export const AppRoutes = {
  public: {
    landing: '',
    notFound: '**',
  },

  auth: {
    route: '',
    login: 'login',
    register: 'register',
    forgotPassword: 'forgot-password',
    resetPassword: 'reset-password',
    emailSent: 'email-sent',
    confirmEmail: 'confirm-email',
  },

  profile: {
    route: 'profile',
    index: '',
  },
};

export function getAuthPath(
  child: keyof Omit<typeof AppRoutes.auth, 'route'>,
): string {
  const base = AppRoutes.auth.route;
  const path = AppRoutes.auth[child];
  const fullPath = [base, path].filter(Boolean).join('/');
  return `/${fullPath}`;
}

export function getProfilePath(
  child: keyof Omit<typeof AppRoutes.profile, 'route'>,
): string {
  const base = AppRoutes.profile.route;
  const path = AppRoutes.profile[child];
  const fullPath = [base, path].filter(Boolean).join('/');
  return `/${fullPath}`;
}
