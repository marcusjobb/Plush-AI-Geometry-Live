# geometry_live

Ett live-exempel på interface i C#, byggt för klassrummet. Samma uppgift som
`kalkylatorn_live`, men i C# och med geometri i stället för moms.

```
Former
------
Cirkel       area  28.27   omkrets  18.85
Rektangel    area  24.00   omkrets  20.00
Triangel     area   6.00   omkrets  12.00

Total area: 58.27
Störst:     Cirkel
```

## Kör den

```bash
dotnet test                                  # 46 tester
dotnet run --project GeometryLive            # tabellen ovan
```

## Vad exemplet visar

| Fil | Poängen |
|---|---|
| `IShape.cs` | Kontraktet: area, omkrets, namn. Inget mer. |
| `Circle.cs`, `Rectangle.cs`, `Triangle.cs` | Varje form räknar på sig själv. Ingen annan vet hur. |
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

## Live-delen

Be klassen skriva en kvadrat. Det tar tre minuter:

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

Lägg den i `shapes` i `Program.cs` och kör. Ingenting annat ändras, ingen
`switch`, ingen ny `if` i rapporten. Sedan `dotnet test` — 46 gröna, fast koden
växte. Det är hela poängen.

`ShapeReportTests` innehåller redan en sådan kvadrat som privat testklass, som
bevis på att rapporten inte bryr sig om vilka former den får.

## Noteringar för den som vill gräva

- `Guard.cs` finns för att sidor och radier måste vara positiva — samma regel i
  tre klasser. Det är delad kunskap, inte bara likadan kod.
- `Circle.Radius`, `Rectangle.Width`, `Rectangle.Height` och `Triangle.A/B/C` är
  publika med flit, fast ingen kod utanför klasserna läser dem. De är formens
  identitet, kostar en rad var och syns i debuggern när en elev undrar vad hen
  skapade. Det är ett medvetet undantag från YAGNI, inte glömska.
- Mycket stora tal svämmar över: `new Circle(1e200).Area` blir oändligt. Inget
  skydd finns, och det är ett rimligt val för ett demo — men det står här i
  stället för att upptäckas av en elev.
- `Triangle.Area` använder Herons formel. Triangelolikheten i konstruktorn gör att
  uttrycket under roten aldrig kan bli negativt — det är därför det inte finns
  någon `Math.Max(0, ...)` där. Mutationstestning visade att en sådan klämma var
  oåtkomlig kod.
- Triangelolikheten (`A + B > C`) ligger i `Triangle`, inte i `Guard`. Bara
  trianglar har den regeln.
- Tal skrivs med `:F2` och maskinens kultur. Vill du ha svenskt komma i hela
  utskriften, sätt `CultureInfo.CurrentCulture` i `Program.cs`.
