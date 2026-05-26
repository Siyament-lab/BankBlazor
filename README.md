# BankBlazor

En headless bankadministrationsapplikation byggd med Blazor WebAssembly och ASP.NET Core Web API.

## Om applikationen
BankBlazor låter administratörer hantera kunders bankkonton, se transaktionshistorik samt utföra insättningar, uttag och överföringar mellan konton.

## Live Demo
[BankBlazor Klient](https://bankblazor-clientsi-hah9efc7g8e0h4g7.swedencentral-01.azurewebsites.net)

[BankBlazor API](https://banlblazor-api-h7gsddfychb0g6ey.swedencentral-01.azurewebsites.net)

## Källkod
[GitHub Repository](https://github.com/Siyament-lab/BankBlazor)


## Teknikstack
- **Frontend:** Blazor WebAssembly (.NET 8)
- **Backend:** ASP.NET Core Web API (.NET 8)
- **Databas:** Microsoft SQL Server (Azure SQL)
- **ORM:** Entity Framework Core (Database First)
- **Hosting:** Azure App Service

## Funktioner
- Visa kundprofil via Kund-ID
- Visa kontosaldo och kontoinformation
- Visa transaktionshistorik med filtrering på typ (Credit/Debit)
- Sätta in pengar på ett konto
- Ta ut pengar från ett konto
- Överföra pengar mellan konton
- Alla transaktioner sparas i databasen

## Sidor
- **Home** - Startsida, innehåller endast välkomsttext
- **Dashboard** - Sök efter kund via Kund-ID, visa profil och konton samt snabblänk till Transaction sidan, där data för sökta kund-ID överförs från Dashboard sökning och kan användas för uttag, överföring eller insättning.
- **Transactions** - Visa transaktionshistorik, sätta in, ta ut och överför pengar

## Installation

### Krav
- .NET 8 SDK
- SQL Server
- Visual Studio 2022

### Databasanslutning
Använd denna connection string för lokal utveckling:
Server=localhost;Database=BankBlazor;Trusted_Connection=True;TrustServerCertificate=true;


## Kända problem
- Kontosaldot lagras som ett statiskt värde i databasen istället för att beräknas dynamiskt från transaktionshistoriken. Detta innebär att saldot kan bli missvisande om transaktioner raderas manuellt från databasen.

## Arkitektur
Applikationen följer en headless arkitektur där frontend (Blazor WebAssembly) kommunicerar med backend (ASP.NET Core Web API) via JSON över HTTP.

## Projektstruktur

### BankBlazor.API
- **Controllers/** - API-kontroller för Account, Customer och Transaction
- **Data/** - DbContext genererad via Database First
- **DTOs/** - Dataöverföringsobjekt för API-anrop
- **Entities/** - Databasmodeller genererade via Entity Framework
- **Services/** - Valideringslogik för transaktioner

### BankBlazor.Client
- **DTOs/** - Dataöverföringsobjekt för klienten
- **Layout/** - MainLayout och NavMenu
- **Pages/** - Blazor-sidor (Home, Dashboard, Transactions)
