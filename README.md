# Silicon Web App

En webbapplikation byggd med **ASP.NET Core MVC** – mitt första projekt med den teknologin! I denna applikation kan användare registrera sig, logga in, byta profilbild, ändra lösenord och gå med i kurser. Det finns även ett administratörskonto med möjligheter att skapa och hantera kurser.

## Funktioner

### Användarhantering
- Skapa nytt konto (registrering)
- Inloggning och utloggning
- Ändra profilbild
- Uppdatera lösenord

### Kursadministration
- Användare kan gå med i befintliga kurser
- Administratörer kan skapa och hantera kurser

## Verktyg och Teknologier

Projektet använder ett flertal moderna verktyg och ramverk, bland annat:

- **ASP.NET Core MVC** – För att bygga den serverbaserade applikationen med MVC-arkitektur.
- **Entity Framework Core** – För att hantera databasåtkomst och ORM.
- **Visual Studio** – Huvudsakligt utvecklingsverktyg för projektet.
- **C#** – Huvudspråket för backend-utvecklingen.
- **HTML, CSS och SCSS** – För att strukturera och styla användargränssnittet.
- **JavaScript** – För att hantera interaktivitet på klientsidan.

## Projektstruktur

Projektet är uppdelat i flera mappar för att hålla koden organiserad:

- **Infrastructure**  
  Innehåller databasmodeller, migrations och konfigurationer relaterade till Entity Framework Core.

- **Web-API-Camilla**  
  Exponerar API-endpoints som används av webbapplikationen. (Om API:et används för att hantera asynkrona anrop eller integrationer.)

- **Web-app_Camilla**  
  Själva ASP.NET Core MVC-applikationen med controllers, views och modeller.

### Övriga filer

- `Uppgifter_ASPNET.sln` – Lösningsfilen för Visual Studio.
- `.gitignore` och `.gitattributes` – För att hantera versionskontroll och kodformattering.
- `README.md` – Denna fil.

## Installation och Uppstart

Följ dessa steg för att köra projektet lokalt:

### Kloning av repository

Klona projektet från GitHub:

```bash
git clone https://github.com/Camilla-bjorkcom/Silicon-web-app-CamillaBjorkgren.git

Öppna lösningen
Öppna Uppgifter_ASPNET.sln i Visual Studio.

Återställ NuGet-paket
Visual Studio bör automatiskt återställa de nödvändiga NuGet-paketen. Om inte, högerklicka på lösningen och välj "Restore NuGet Packages".

Konfigurera databas
Se till att din databasanslutning är korrekt inställd i appsettings.json eller Web.config (beroende på projektets version). Om du använder LocalDB, kontrollera att din connection string pekar på rätt filväg.

Kör applikationen
Starta applikationen genom att köra projektet i Visual Studio (F5 eller Ctrl+F5).

Användning
Användarkonto
Registrera ett nytt konto eller logga in med ett befintligt konto. Efter inloggning kan du uppdatera din profil, byta profilbild eller ändra lösenord.
Kursdeltagande
Utforska tillgängliga kurser och gå med i de kurser du är intresserad av.
Administratör
Logga in som administratör för att skapa nya kurser och hantera existerande kurser.
Vidare Utveckling
Här finns möjlighet att utveckla applikationen vidare, till exempel genom att:

Implementera fler användarroller.
Utöka API:et för mer komplex funktionalitet.
Förbättra UI/UX med hjälp av moderna frontend-ramverk.
