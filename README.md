# Sylink

*A collaboration-first space for writing, planning, and hanging out with friends.*

---

## 📚 Table of Contents

- [📁 Folder Structure](#-folder-structure)
- [Phase 1: Auth Backend](#phase-1-auth-backend)
- [Phase 2: Postgres + EF Core](#phase-2-postgres--ef-core)
- [Phase 3: Frontend Skeleton (ClientApp)](#phase-3-frontend-skeleton-clientapp)
- [Phase 4: Feature Modules](#phase-4-feature-modules)
  - [4.1 Servers & Channels](#41-servers--channels)
  - [4.2 Messaging](#42-messaging)
  - [4.3 Notes](#43-notes)
  - [4.4 Calendar & Schedule](#44-calendar--schedule)
- [Phase 5: Staging Environment](#phase-5-staging-environment)
- [Phase 6: Production Readiness](#phase-6-production-readiness)

---

## 📁 Folder Structure

```
sylink/
├── WebApi/              → .NET backend (API, SignalR)
├── ClientApp/           → Angular frontend
├── Docker/              → Dockerfiles, Compose (Postgres only)
├── Scripts/             → Dev scripts (e.g. dev.sh, setup.sh)
├── .env.dev             → Local config
└── README.md
```

---

### Phase 1: Auth Backend

- [ ] Setup .NET WebApi project
- [ ] Register / login / logout endpoints
- [ ] JWT auth
- [ ] Email verification
- [ ] Password reset (token-based)
- [ ] Token refresh endpoint
- [ ] Display name reservation
- [ ] Background job: remove unverified accounts

---

### Phase 2: Postgres + EF Core

- [ ] Add `docker-compose.dev.yml` with Postgres
- [ ] Switch to EF Core + Npgsql
- [ ] Migrations + dev DB seeding
- [ ] Replace in-memory structures
- [ ] Begin integration tests

---

### Phase 3: Frontend Skeleton (ClientApp)

- [ ] Initialize Angular project
- [ ] Link to backend (auth flows)
- [ ] Auth guards + routing
- [ ] Homepage/dashboard
- [ ] Profile editor
- [ ] Feature route stubs (`/servers`, `/messages`, `/notes`, `/calendar`)
- [ ] `setup.sh` script for bootstrapping project

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

### Phase 5: Staging Environment

- [ ] Dockerize backend for deploy
- [ ] Add staging config
- [ ] Deploy to EC2
- [ ] Connect AWS SES for email
- [ ] Basic logging/error tracking

---

### Phase 6: Production Readiness

- [ ] Optimize DB schema
- [ ] Mobile responsiveness
- [ ] UI polish
- [ ] Optional PWA setup
- [ ] Final docs & README polish
