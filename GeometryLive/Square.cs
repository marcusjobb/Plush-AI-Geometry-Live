namespace GeometryLive;

/// <summary>
/// A square: four equal sides and four right angles. It implements
/// <see cref="IShape"/> directly instead of inheriting from
/// <see cref="Rectangle"/> — a rectangle carries a width and a height that can
/// be set to different values, and a square must never allow that.
/// </summary>
public sealed class Square : IShape
{
    /// <summary>Creates a square.</summary>
    /// <param name="side">The side, greater than zero.</param>
    /// <exception cref="ArgumentException">Thrown when the side is not positive.</exception>
    public Square(double side)
    {
        Side = Guard.Positive(side, nameof(side));
    }

    /// <summary>The side the square was created with.</summary>
    public double Side { get; }

    /// <inheritdoc />
    public string Name => "Kvadrat";

    /// <inheritdoc />
    public double Area => Side * Side;

    /// <inheritdoc />
    public double Perimeter => 4 * Side;
}
