# KupujemProdajem

A full-stack classifieds marketplace where users publish, browse, and manage ads, modeled on the KupujemProdajem platform. The backend is an ASP.NET Core 8 REST API and the frontend is a React single-page app.

## Features

- Registration and login with ASP.NET Identity and JWT authentication
- Create, edit, and delete ads, with an image link per ad
- Ownership checks: only the owner of an ad can edit or delete it
- Ad detail page with a view counter
- Filtering by city, category, group, price range, fixed price, and replacement option
- Favorites: save and remove ads, and browse saved ads
- Personal dashboard with the user's own ads
- Real-time messages with SignalR

## Tech stack

**Backend:** ASP.NET Core 8, Entity Framework Core (SQL Server, code-first migrations), ASP.NET Identity, JWT, SignalR  
**Frontend:** React 18, React Router  
**Structure:** Controllers, Services, Repositories with dependency injection

## Getting started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or SQL Server Express)
- Node.js and npm

### Backend

1. Set the connection string in `KupujemProdajemWebApp/appsettings.json` (`ConnectionStrings:DefaultConnection`).
2. Store the JWT signing key with User Secrets (run inside the `KupujemProdajemWebApp` project folder). The key must be at least 64 characters long:
   ```bash
   dotnet user-secrets init
   dotnet user-secrets set "JWT:SigningKey" "<a-long-random-string-at-least-64-characters>"
   ```
3. Apply the migrations and run the API:
   ```bash
   dotnet ef database update
   dotnet run
   ```
   The API runs on `https://localhost:7084`.

### Frontend

```bash
cd frontend
npm install
npm start
```
The app opens on `http://localhost:3000`.

## Planned improvements

- Email-based password reset
- Automated tests
- Docker support and CI pipeline
- Configurable API base URL for the frontend