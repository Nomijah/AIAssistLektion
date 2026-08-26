# Product Inventory

Product Inventory är ett litet undervisningsprojekt för en lektion om AI-stödd systemutveckling. Applikationen visar hur en React-klient och ett ASP.NET Core Web API kan hantera produkter utan att interna produktfält exponeras över API-gränsen.

## Teknik

- .NET 10.0 LTS (SDK 10.0.400)
- ASP.NET Core 10 med Controllers och inbyggd OpenAPI
- Entity Framework Core 10.0.11 och SQL Server
- React 19.2.8 och Vite 8.2.2 med JavaScript
- Node.js 24 (projektet skapades och verifierades med Node 24.19.0)
- xUnit för tester

`global.json` väljer den .NET-version som finns installerad i projektmiljön. Frontendversionerna är även låsta i `frontend/package-lock.json`.

## Struktur

```text
ProductInventory.sln
backend/
  ProductInventory.Api/
    Controllers/     HTTP, statuskoder och API-kontrakt
    Services/        applikationslogik och mappning
    Data/            EF Core DbContext
    Entities/        databasentiteter, inklusive interna fält
    DTOs/            separata request- och responsemodeller
    Migrations/      initial SQL Server-migration och demo-data
  ProductInventory.Api.Tests/
frontend/
  src/
    api/             fetch-anrop
    components/      små React-komponenter
_teacher/            separata läraranteckningar
```

Backend följer flödet:

```text
Controller -> Service -> AppDbContext -> SQL Server
```

Controllern hanterar HTTP. Servicen hanterar applikationslogik, databasfrågor och mappning mellan entitet och DTO. `AppDbContext` hanterar EF Core och SQL Server. EF-entiteten skickas aldrig direkt till eller från API:t. Därför saknas de interna fälten `CostPrice` och `InternalNotes` i API-modellerna.

## Starta backend

Förutsättningar är .NET 10 SDK och en tillgänglig SQL Server. Från projektroten:

```bash
dotnet restore
dotnet tool restore
dotnet ef database update --project backend/ProductInventory.Api
dotnet run --project backend/ProductInventory.Api --launch-profile http
```

API:t kör då på `http://localhost:5080`. OpenAPI-dokumentet finns i development på `http://localhost:5080/openapi/v1.json`. Exempelanrop finns i `backend/ProductInventory.Api/ProductInventory.Api.http` och kan köras från Visual Studio, Rider eller VS Code med REST Client.

## Databas

Standardinställningen i `appsettings.json` pekar på en lokal SQL Server på port 1433 och använder ett uttryckligt demo-lösenord. Det är inte en riktig hemlighet och ska bytas lokalt. Starta exempelvis SQL Server i Docker:

```bash
docker run --name product-inventory-sql \
  -e ACCEPT_EULA=Y \
  -e MSSQL_SA_PASSWORD='YourStrong!Passw0rd' \
  -p 1433:1433 \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

För en annan SQL Server rekommenderas en environment variable så att inga credentials läggs i Git:

```bash
export ConnectionStrings__ProductInventory='Server=localhost,1433;Database=ProductInventory;User Id=sa;Password=DITT_LOKALA_LÖSENORD;TrustServerCertificate=True'
```

Alternativt kan .NET User Secrets användas:

```bash
dotnet user-secrets init --project backend/ProductInventory.Api
dotnet user-secrets set 'ConnectionStrings:ProductInventory' 'DIN CONNECTION STRING' --project backend/ProductInventory.Api
```

Kör sedan migrationen med `dotnet ef database update --project backend/ProductInventory.Api`. Den skapar databasen och tre demoprodukter.

## Starta frontend

Öppna en andra terminal:

```bash
cd frontend
npm install
npm run dev
```

Klienten kör på `http://localhost:5173`. Den använder `http://localhost:5080` som standard. Kopiera `.env.example` till `.env` och ändra `VITE_API_BASE_URL` om backend kör på en annan adress.

## Funktionalitet

- Lista produkter med namn, pris och lagersaldo.
- Visa publika detaljer för en vald produkt.
- Skapa en produkt med validering i både frontend och backend.
- Ta bort en produkt efter bekräftelse.
- Returnera konsekventa statuskoder och Problem Details vid fel.
- Testa API:t med både giltiga och ogiltiga exempel i `.http`-filen.

## Tester

```bash
dotnet test
npm --prefix frontend run build
```

De små servicetesterna använder EF Cores InMemory-provider. De visar bland annat att interna värden sätts av servern och inte finns på responsemodellen.

## Planned functionality

Redigering av en befintlig produkt är avsiktligt inte implementerad. Det finns därför ingen `PUT`- eller `PATCH`-endpoint och inget redigeringsformulär. Funktionen är planerad som en senare lektionsövning.
