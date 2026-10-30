namespace GeometryLive;

/// <summary>
/// A triangle described by its three sides. The sides must be positive and
/// able to meet: the two shorter ones together must be longer than the longest.
/// </summary>
public sealed class Triangle : IShape
{
    /// <summary>Creates a triangle.</summary>
    /// <param name="a">The first side, greater than zero.</param>
    /// <param name="b">The second side, greater than zero.</param>
    /// <param name="c">The third side, greater than zero.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when a side is not positive, or when the sides cannot form a triangle.
    /// </exception>
    public Triangle(double a, double b, double c)
    {
        A = Guard.Positive(a, nameof(a));
        B = Guard.Positive(b, nameof(b));
        C = Guard.Positive(c, nameof(c));

        if (A + B <= C || A + C <= B || B + C <= A)
        {
            throw new ArgumentException($"Sides {A}, {B} and {C} cannot form a triangle.");
        }
    }

    /// <summary>The first side.</summary>
    public double A { get; }

    /// <summary>The second side.</summary>
    public double B { get; }

    /// <summary>The third side.</summary>
    public double C { get; }

    /// <inheritdoc />
    public string Name => "Triangel";

    /// <inheritdoc />
    /// <remarks>Heron's formula: area = sqrt(s * (s-a) * (s-b) * (s-c)).</remarks>
    public double Area
    {
        get
        {
            var half = Perimeter / 2;

            return Math.Sqrt(half * (half - A) * (half - B) * (half - C));
        }
    }

    /// <inheritdoc />
    public double Perimeter => A + B + C;
}
