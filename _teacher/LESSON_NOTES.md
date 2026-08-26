# Läraranteckningar – AI-stödd systemutveckling

Den här filen är avsedd för läraren och kan tas bort innan projektet delas med studenterna.

## Föreslaget lektionsflöde

1. Låt studenterna bekanta sig med flödet `Controller -> Service -> AppDbContext` och API-kontraktets DTO:er.
2. Be ett AI-verktyg generera `PUT /api/products/{id}` och motsvarande redigeringsfunktion i React.
3. Granska förslaget innan någon kod accepteras.
4. Låt studenten förbättra lösningen, bygga och testa den med `.http`-filen.
5. Be AI:t göra en andra code review av den förbättrade lösningen.
6. Diskutera vilka review-kommentarer som är relevanta, felaktiga eller överdrivna.

## Frågor för code review

- Använder API:t en särskild update-request eller tar det emot `Product` direkt?
- Kan klienten ändra `Id`, `CostPrice`, `InternalNotes` eller tidsstämplar?
- Har EF Core-frågor placerats i controllern i stället för servicen?
- Uppdateras bara de avsedda publika fälten?
- Hanteras en saknad produkt med 404?
- Valideras blankt namn, negativt pris och negativt lagersaldo i backend?
- Används async-metoder och `CancellationToken` konsekvent?
- Har AI:t duplicerat skapande- och uppdateringslogik på ett onödigt sätt?
- Visar frontend tydligt loading-, success-, validerings- och API-fel?
- Har den föreslagna ändringen fått representativa tester?

## Vanliga problem i AI-genererade lösningar

AI-förslag kan mass-assigna alla entitetsfält, returnera EF-entiteten direkt, använda `AppDbContext` i controllern eller skapa nya arkitekturlager som inte passar projektet. Andra vanliga problem är bortglömd 404-hantering, synkrona EF-anrop, inkonsekventa DTO:er, för bred CORS, exceptiondetaljer i API-responsen och frontendkod som antar att alla fel är JSON.

Var särskilt uppmärksam på lösningar som först läser en entitet men sedan ersätter den med hela requestobjektet. Det kan både överexponera fält och orsaka att värden som klienten inte skickar skrivs över. Diskutera också om AI:ts föreslagna abstraktioner faktiskt gör lösningen tydligare för projektets storlek.

## Förslag på fortsatta AI-övningar

- Be AI föreslå tester för update-funktionen och välj bara de fall som ger verkligt värde.
- Ge AI en avsiktligt misslyckad `.http`-respons och använd det som debugger-övning.
- Be två olika modeller göra code review och jämför råden.
- Låt AI förklara SQL som motsvarar en EF Core-fråga och kontrollera förklaringen.
- Be AI föreslå en större refaktorering och låt studenterna argumentera för eller emot den utifrån projektets undervisningsmål.
