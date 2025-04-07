# Sylink

_A collaboration-first space for writing, planning, and hanging out with friends._

---

## Project Requirements (Dev Machine)

To run **Sylink** locally (pre-Docker), install the following global tools:

### Global Dependencies

| Tool                                                              | Version  | Install Command                                                      |
| ----------------------------------------------------------------- | -------- | -------------------------------------------------------------------- |
| [Node.js](https://nodejs.org/en/download)                         | `v22.x`  | —                                                                    |
| [Angular CLI](https://www.npmjs.com/package/@angular/cli)         | `v19.x`  | `npm install -g @angular/cli`                                        |
| [.NET SDK](https://dotnet.microsoft.com/en-us/download)           | `v9.x`   | —                                                                    |
| [Docker Desktop](https://www.docker.com/products/docker-desktop/) | `v4.x+`  | \_\_                                                                 |
| [Taskfile CLI](https://taskfile.dev/#/installation)               | `v3.x`   | `scoop install task` _(Windows)_ or `brew install go-task` _(macOS)_ |
| [OpenSSL](https://slproweb.com/products/Win32OpenSSL.html)        | `>= 3.x` | \_\_                                                                 |

---

### Local HTTPS Dev (Angular)

To support local development over **HTTPS**:

- A self-signed SSL cert is auto-generated and stored in:

  ```
  ClientApp/ssl/localhost.crt
  ClientApp/ssl/localhost.key
  ```

- This happens automatically as part of:
  ```
  task setup
  ```

---

> ℹ These global dependencies are only required for local development.  
> Once full Docker setup is in place, all tooling will be containerized.

## Project Architecture

**Sylink** is a full-stack, real-time collaboration platform built with a modular, container-ready architecture optimized for team collaboration and feature isolation.

### Core Technologies

- **.NET 9 Web API** — Backend built with ASP.NET Core, using Identity for auth and SignalR for real-time messaging
- **Angular 19 SPA** — Frontend built with Angular standalone components and feature-based routing
- **PostgreSQL** — Relational database powered by EF Core and managed via Docker
- **Docker + Compose** — Used for local and staging environments, including containerized Postgres and backend
- **Taskfile CLI** — Cross-platform dev automation (setup, dev, deploy tasks)

---

### Project Folder Structure

The backend and frontend are fully decoupled but integrated via REST APIs and SignalR. The monorepo is structured for scalability, and all services are orchestrated via `Taskfile` and Docker.

```
sylink/
├── ClientApp/           → Angular frontend
├── WebApi/              → .NET backend
├── Docker/              → Dockerfiles
├── Taskfile.yml         → Project automation
├── .gitignore
├── LICENSE
└── README.md            → Project roadmap + architecture

```

---

## ⚠️ Implementation Notes

This project is currently running with relaxed development settings:

- Weak password requirements
- A simple, insecure JWT secret (do not use in production)
- Refresh tokens are not yet implemented
- Email confirmation is not enforced
- Authentication middleware is not fully configured for production

# WebAPI Backend – Onion Architecture

This project is a modular .NET Core Web API built with long-term maintainability and real-world deployment in mind.  
It follows a Onion Architecture with a pragmatic setup suitable for solo or small team development.

---

## Architecture Overview

The app is split into clear layers, each with its own responsibility. This makes it easier to test, swap infrastructure, and avoid spaghetti code over time.

### `Api/` – Presentation Layer

- Contains controllers and middleware
- Responsible for handling HTTP requests and responses
- Should not contain any business logic

### `Application/` – Use Cases & Business Logic

- Contains services, interfaces, request/response models, and validation
- Coordinates business processes and workflows
- Doesn’t know or care about infrastructure

### `Domain/` – Core Models

- Just plain C# classes`
- No external dependencies
- Represents business rules and structures
- Exception is Idenity for Application User

### `Infrastructure/` – Implementations

- Connects to real systems: EF Core, JWT, email, etc.
- Implements the interfaces defined in Application
- Swappable and mockable

### `Startup/` – App Composition

- Wires everything together
- Registers services and middleware

---

## Why This Structure?

- Clear separation of responsibilities
- Business logic can be tested without hitting the database
- Easy to swap out infrastructure pieces (e.g. replace JWT system)
- Scales well as new features are added
- Works great with SPAs using secure cookie-based JWT auth

This setup aims to balance **structure and flexibility** — especially helpful when working alone but building for the long term.

## Why Not Identity Directly?

This app uses **cookie-based JWT authentication** designed for SPA clients (e.g., Angular):

- **Access Token**: Short-lived, stored in an HttpOnly cookie
- **Refresh Token**: Stored separately, tied to DB entries, and allows token renewal

Because this app uses custom refresh tokens and a stateless JWT flow, Identity's built-in auth pipeline doesn't directly apply. Instead:

- Middleware handles JWT validation
- Manual claims extraction enables custom context
- Refresh token validation is done in `AuthService`

This gives full control over auth mechanics while keeping things simple.

---

### Phase 1: Auth Backend

- [x] Setup .NET WebApi project
- [x] Layered architecture (Domain / Application / Infrastructure)
- [x] Register / login / logout endpoints
- [x] Remember-me support (persistent tokens)
- [x] Email verification before login
- [x] Forgot password / reset password flow (token-based)
- [x] JWT access + refresh tokens
- [x] Refresh token rotation
- [x] Token refresh via middleware
- [x] Secure cookies (HttpOnly, SameSite, Secure)
- [x] ClaimsPrincipal with user ID + email
- [x] Auth middleware (sets HttpContext.User)
- [x] Authorization policy for confirmed users
- [x] Logout clears refresh token
- [x] Request validation (attributes)
- [x] Structured error messages and validation feedback

---

### Phase 2: Frontend Skeleton

- [x] Initialize Angular project
- [ ] Link to backend (auth flows)
- [ ] Auth guards + routing
- [ ] Homepage/dashboard
- [ ] Profile editor
- [ ] AuthService
- [ ] JWT cookie handling (interceptors)

---

### Phase 3: Postgres

- [ ] Add `docker-compose.dev.yml` with Postgres
- [ ] Switch to EF Core + Npgsql
- [ ] Migrations + dev DB seeding
- [ ] Replace in-memory structures

---

### Phase 4: Feature Modules

---

#### 4.1 Servers & Channels

**Requirements**

- [ ] Server creation, joining, leaving
- [ ] Server invites (token/direct)
- [ ] Server creator as admin
- [ ] Channel creation in servers
- [ ] Server member list

**Wants**

- [ ] Custom roles & permissions (owner-defined)
- [ ] App integrations (notes, calendar)
- [ ] Voice + video chat
- [ ] Role-based server permissions

---

#### 4.2 Messaging

**Requirements**

- [ ] Send messages in channels
- [ ] Channel message history
- [ ] Real-time updates (SignalR)
- [ ] Direct messages (1-to-1)
- [ ] Show user (even if deleted)

**Wants**

- [ ] Group DMs
- [ ] Reactions, edits, deletions
- [ ] Typing indicators
- [ ] Message threads/replies

---

#### 4.3 Notes

**Requirements**

- [ ] Private notes
- [ ] Markdown content
- [ ] Read/edit views

**Wants**

- [ ] Live collaborative editing (Google Docs-style)
- [ ] Shared notes via invite
- [ ] Auto-save + real-time updates
- [ ] Markdown toolbar (friendly UI)
- [ ] File/folder structure (max 2 levels)
- [ ] Note search by title

---

#### 4.4 Calendar & Schedule

**Requirements**

- [ ] Weekly recurring schedule per user
- [ ] One-off weekly overrides
- [ ] Notes per calendar day
- [ ] Checklist/todo per day

**Wants**

- [ ] Google Calendar sync
- [ ] Server-level shared calendars
- [ ] RSVP/reminders
- [ ] Drag/drop weekly view
- [ ] Link notes to calendar events

---

### Phase 5: Prestaging requirements

**Requirements**

- [ ] Enforce a stronger password policy via IdentityOptions
- [ ] Implement Oauth
- [ ] Lock down CORS policy for known environments
- [ ] Rotate and secure JWT secret via environment config
- [ ] Add background task runner
- [ ] Purge unconfirmed users after X days
- [ ] Clean up expired email/password tokens
- [ ] Revoke old refresh tokens
- [ ] Add audit logging for auth events
- [ ] Rate limit sensitive auth endpoints (register, login, forgot-password)
- [ ] Global app-level rate limiting (IP-based)

**Wants**

- [ ] PWA setup
- [ ] Track email bounces
- [ ] Cleanup orphaned data

### Phase 6: Testing plan

tba ...

- [ ] Unit tests - backend services
- [ ] Integration tests - auth flow, db interactions
- [ ] Frontend component testing - form validation, guards, services
- [ ] E2E tests - Playwright

### Phase 7: Staging Environment

- [ ] Dockerize backend for deploy
- [ ] Add staging config
- [ ] Deploy to EC2
- [ ] Connect AWS SES for email
- [ ] Basic logging/error tracking
- [ ] Review and lock down CORS, HTTPS, and auth middleware
- [ ] Replace the JWT secret with a secure, environment-based key
- [ ] Setup CI/CD pipeline with github actions

---

### Phase 8: Production Readiness

- [ ] Mobile responsiveness
- [ ] UI polish
- [ ] Final docs & README polish
