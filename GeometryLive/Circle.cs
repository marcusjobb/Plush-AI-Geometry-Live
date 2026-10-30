namespace GeometryLive;

/// <summary>A circle with a radius greater than zero.</summary>
public sealed class Circle : IShape
{
    /// <summary>Creates a circle.</summary>
    /// <param name="radius">The radius, greater than zero.</param>
    /// <exception cref="ArgumentException">Thrown when the radius is not positive.</exception>
    public Circle(double radius)
    {
        Radius = Guard.Positive(radius, nameof(radius));
    }

    /// <summary>The radius the circle was created with.</summary>
    public double Radius { get; }

    /// <inheritdoc />
    public string Name => "Cirkel";

    /// <inheritdoc />
    public double Area => Math.PI * Radius * Radius;

    /// <inheritdoc />
    public double Perimeter => 2 * Math.PI * Radius;
}
