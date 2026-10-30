namespace GeometryLive;

/// <summary>A rectangle with a width and a height, both greater than zero.</summary>
public sealed class Rectangle : IShape
{
    /// <summary>Creates a rectangle.</summary>
    /// <param name="width">The width, greater than zero.</param>
    /// <param name="height">The height, greater than zero.</param>
    /// <exception cref="ArgumentException">Thrown when a side is not positive.</exception>
    public Rectangle(double width, double height)
    {
        Width = Guard.Positive(width, nameof(width));
        Height = Guard.Positive(height, nameof(height));
    }

    /// <summary>The width the rectangle was created with.</summary>
    public double Width { get; }

    /// <summary>The height the rectangle was created with.</summary>
    public double Height { get; }

    /// <inheritdoc />
    public string Name => "Rektangel";

    /// <inheritdoc />
    public double Area => Width * Height;

    /// <inheritdoc />
    public double Perimeter => 2 * (Width + Height);
}
