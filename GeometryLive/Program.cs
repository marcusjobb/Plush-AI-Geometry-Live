using GeometryLive;

// Ett live-exempel: samma loop hanterar alla tre formerna, och den vet inte
// vad de heter. Lägg till en Square i morgon — den här filen rörs inte.

IShape[] shapes =
[
    new Circle(3),
    new Rectangle(6, 4),
    new Triangle(3, 4, 5),
];

Console.WriteLine("Former");
Console.WriteLine("------");

foreach (var shape in shapes)
{
    Console.WriteLine($"{shape.Name,-12} area {shape.Area,6:F2}   omkrets {shape.Perimeter,6:F2}");
}

var report = new ShapeReport(shapes);

Console.WriteLine();
Console.WriteLine($"Total area: {report.TotalArea:F2}");
Console.WriteLine($"Störst:     {report.Largest.Name}");
