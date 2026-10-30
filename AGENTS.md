# Geometri och interface

## Om projektet
Ett litet live-exempel för C#: en `IShape` med tre implementationer, och en
rapport som räknar på dem utan att veta vilka de är.

## Teknik
- Språk: C#
- Ramverk: .NET 10
- Tester: xUnit

## Regler
- Svara mig på svenska. Kod och variabelnamn på engelska.
- Gör små ändringar. Fråga innan du lägger till nya beroenden.
- Skriv eller uppdatera tester för all ny logik.
- Kör testerna innan du säger att du är klar.
- Är testerna gröna så committa.
- Alla publika typer och medlemmar ska ha en XML-doc.

## Kommandon
- Kör tester: `dotnet test`
- Kör programmet: `dotnet run --project GeometryLive`
- Coverage: `dotnet test --collect:"XPlat Code Coverage"`
