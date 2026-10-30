# geometry_live

Ett live-exempel på interface i C#, byggt för klassrummet. Samma uppgift som
`kalkylatorn_live`, men i C# och med geometri i stället för moms.

```
Former
------
Cirkel       area  28.27   omkrets  18.85
Rektangel    area  24.00   omkrets  20.00
Triangel     area   6.00   omkrets  12.00
Kvadrat      area  25.00   omkrets  20.00

Total area: 83.27
Störst:     Cirkel
```

## Kör den

```bash
dotnet test                                  # 58 tester
dotnet run --project GeometryLive            # tabellen ovan
```

## Vad exemplet visar

| Fil | Poängen |
|---|---|
| `IShape.cs` | Kontraktet: area, omkrets, namn. Inget mer. |
| `Circle.cs`, `Rectangle.cs`, `Triangle.cs`, `Square.cs` | Varje form räknar på sig själv. Ingen annan vet hur. |
| `ShapeReport.cs` | Summerar och hittar störst — utan en enda typkontroll. |
| `Program.cs` | En loop över `IShape[]`. Den vet inte vad den skriver ut. |

Jämför med den vanliga nybörjarlösningen:

```csharp
// Så här vill vi inte ha det
public double Area(object shape)
{
    if (shape is Circle c) return Math.PI * c.Radius * c.Radius;
    if (shape is Rectangle r) return r.Width * r.Height;
    if (shape is Triangle t) return Heron(t);      // och nu då?
    throw new NotSupportedException();
}
```

Varje ny form kräver att den här metoden ändras — och den är redan testad och
fungerar. Med `IShape` är en ny form en ny fil, och ingenting annat rörs.

---

## SOLID i det här projektet

SOLID är fem designprinciper som hjälper kod att växa utan att råtna. Här är
alla fem, kopplade till faktisk kod i repot.

### S — Single Responsibility Principle

> En klass gör en sak. En anledning att ändra den.

`Triangle.cs` räknar ut sin area och sin omkrets — det är allt. `ShapeReport.cs`
sammanställer en lista former — det är allt. Ingen av dem formaterar text, sparar
till fil eller validerar indata från andra klasser.

`Guard.cs` är ett tydligt exempel: all indata-validering av mätetal bor på ett
ställe. Behöver regeln ändras (t.ex. tillåt nollsidor för degenerade figurer —
mer om det längre ned) ändras den på ett enda ställe, inte i varje klass.

```csharp
// Guard.cs — en klass, ett ansvar: kontrollera att mätvärdet är rimligt
public static double Positive(double value, string parameterName) { … }
```

### O — Open/Closed Principle

> Öppen för utökning. Stängd för förändring.

Det här är hela poängen med exemplet. Lägg till en liksidig hexagon:

1. Skapa `Hexagon.cs` som implementerar `IShape`
2. Lägg den i listan i `Program.cs`
3. Klart

`ShapeReport`, `Program`, `IShape` — inget av dem rörs. De är stängda för
förändring men öppna för att ta emot en ny form de aldrig hört talas om.

Utan interfacet: varje ny form kräver en ny `if`-gren i `Area()`-metoden ovan,
och den metoden är aldrig "klar".

### L — Liskov Substitution Principle

> En subtyp ska kunna ersätta sin bastyp utan att beteendet går sönder.

`ShapeReport` tar `IEnumerable<IShape>`. Den bryr sig inte om listan innehåller
cirklar, trianglar eller kvadrater — den anropar `Area` och `Perimeter` och får
korrekta svar oavsett. Det är LSP i praktiken.

Det är också därför `Square` **inte** ärver från `Rectangle`. En rektangel kan
ha `Width = 3, Height = 7`. En kvadrat som ärver det kontraktet kan plötsligt
vara icke-kvadratisk — och det bryter mot Liskovprincipen. Kvadraten implementerar
`IShape` direkt i stället.

```csharp
// ShapeReport.cs — ingen typkontroll, inga cast, inga if-satser
public double TotalArea => _shapes.Sum(shape => shape.Area);
```

### I — Interface Segregation Principle

> Tvinga inte en klass att implementera metoder den inte behöver.

`IShape` har tre egenskaper: `Name`, `Area`, `Perimeter`. Inte mer. Om rapporten
senare behöver färg, position eller 3D-volym — det är ett annat interface.

En cirkel tvingas inte implementera `GetVertices()` bara för att en polygon har
hörn. Interfacet är smalt med flit.

### D — Dependency Inversion Principle

> Beroenden ska peka mot abstraktioner, inte mot konkreta klasser.

`ShapeReport` beror på `IShape` — inte på `Circle` eller `Triangle`. Det innebär
att testerna kan stoppa in en minimal testdubbel utan att behöva instansiera en
riktig cirkel med alla dess regler.

```csharp
// ShapeReport.cs — beror på IShape, aldrig på Circle eller Rectangle
public ShapeReport(IEnumerable<IShape> shapes) { … }
```

`Program.cs` är det enda stället i hela projektet som vet vilka konkreta klasser
som faktiskt existerar. Det är rätt: skapandet av objekt (`new Circle(3)`) sker
nära kanten av programmet, inte mitt i affärslogiken.

---

## Degenererade geometriska figurer

En degenererad figur är en figur vars egenskaper har "kollapsat" — en eller flera
dimensioner har försvunnit.

| Figur | Degenererar till | När |
|---|---|---|
| Triangel | En linje | Alla tre hörn på samma linje |
| Polygon | En linje eller punkt | Area = 0 |
| Rektangel | En linje | En sida = 0 |
| Rektangel | En punkt | Båda sidor = 0 |
| Cirkel | En punkt | Radien = 0 |

Den yttersta degenereringen är **punkten**: varken längd, bredd eller area.

### Varför det är relevant för det här repot

`Guard.Positive()` blockerar noll och negativa värden. Det är ett designbeslut:
det här exemplet representerar verkliga, fysiska former som kan mätas. En cirkel
med radien 0 är inte en cirkel — det är en punkt, och en annan sorts objekt.

Om du vill tillåta degenererade figurer (t.ex. för matematisk komplethet) måste
du antingen:

- Ersätta `Guard.Positive` med `Guard.NonNegative` och acceptera att `Area` kan
  returnera 0
- Eller modellera degenerering explicit med separata typer (`Point`, `LineSegment`)
  som också implementerar ett gemensamt interface

Det sistnämnda är det mer SOLID-riktiga valet: varje typ gör en sak, och en
punkt som låtsas vara en cirkel med area 0 bryter mot Liskovprincipen — den
beter sig inte som en cirkel borde.

---

## Live-delen

`Square.cs` ligger i repot som facit — 27 rader, varav hälften kommentarer. Ta
bort den och be klassen skriva den tillbaka:

```csharp
public sealed class Square : IShape
{
    public Square(double side) => Side = Guard.Positive(side, nameof(side));

    public double Side { get; }

    public string Name => "Kvadrat";
    public double Area => Side * Side;
    public double Perimeter => 4 * Side;
}
```

Lägg den sist i `shapes` i `Program.cs` och kör. Ingenting annat ändras, ingen
`switch`, ingen ny `if` i rapporten. Sedan `dotnet test` — grönt, fast koden
växte.

Nästa övning, när kvadraten sitter: en liksidig hexagon. Area `3 * sqrt(3) / 2
* s * s`, omkrets `6 * s`. Samma tre steg — ny fil, `IShape`, in i listan — och
fortfarande noll ändringar i `ShapeReport`.

Frågan som avgör om de förstått det: *"Varför ärver `Square` inte från
`Rectangle`, som har samma formler?"* Svar: `Rectangle` bär en bredd och en höjd
som kan sättas olika, och en kvadrat som plötsligt har `Width = 3, Height = 7` är
inte längre en kvadrat. Den ärver bara kontraktet.

---

## Noteringar för den som vill gräva

- `Guard.cs` finns för att sidor och radier måste vara positiva — samma regel i
  tre klasser. Det är delad kunskap, inte bara likadan kod.
- `Circle.Radius`, `Rectangle.Width`, `Rectangle.Height`, `Square.Side` och
  `Triangle.A/B/C` är publika med flit, fast ingen kod utanför klasserna läser
  dem. De är formens identitet, kostar en rad var och syns i debuggern när en
  studerande undrar vad hen skapade. Det är ett medvetet undantag från YAGNI,
  inte glömska.
- `Square` implementerar `IShape` direkt i stället för att ärva `Rectangle`.
  Arv hade gett den en `Width` och en `Height` som kan sättas olika — och då är
  den inte längre en kvadrat. Kontrakt, inte arv.
- Mycket stora tal svämmar över: `new Circle(1e200).Area` blir oändligt. Inget
  skydd finns, och det är ett rimligt val för ett demo — men det står här i
  stället för att upptäckas av en studerande.
- `Triangle.Area` använder Herons formel. Triangelolikheten i konstruktorn gör att
  uttrycket under roten aldrig kan bli negativt — det är därför det inte finns
  någon `Math.Max(0, ...)` där. Mutationstestning visade att en sådan klämma var
  oåtkomlig kod.
- Triangelolikheten (`A + B > C`) ligger i `Triangle`, inte i `Guard`. Bara
  trianglar har den regeln.
- Tal skrivs med `:F2` och maskinens kultur. Vill du ha svenskt komma i hela
  utskriften, sätt `CultureInfo.CurrentCulture` i `Program.cs`.
