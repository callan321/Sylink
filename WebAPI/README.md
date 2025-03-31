# WebAPI

An ASP.NET Core Web API with user authentication.

---

## ⚠ Development Mode Notice

This project is currently running with relaxed development settings:

- Weak password requirements
- A simple, insecure JWT secret (do not use in production)
- Refresh tokens are not yet implemented
- Email confirmation is not enforced
- Authentication middleware is not fully configured for production

### Before going live, Todos:
- [ ] Replace the JWT secret with a secure, environment-based key (at least 32 characters)
- [ ] Enforce a stronger password policy via `IdentityOptions`
- [x] Implement a refresh token
- [ ] Require email confirmation before allowing login
- [ ] Review and lock down CORS, HTTPS, and auth middleware
- [ ] Replace magic numbers from token experation times

---



