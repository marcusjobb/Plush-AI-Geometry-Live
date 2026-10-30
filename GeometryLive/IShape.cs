namespace GeometryLive;

/// <summary>
/// Anything with a closed outline: it knows its own area and its own perimeter.
/// <para>
/// Every new shape is a new class that implements this interface. Nothing that
/// already works with an <see cref="IShape"/> has to change — that is the point.
/// </para>
/// </summary>
public interface IShape
{
    /// <summary>
    /// A label a report can print without asking what type the shape is.
    /// Putting it here keeps type checks out of the printing code.
    /// </summary>
    string Name { get; }

    /// <summary>The area, in square units.</summary>
    double Area { get; }

    /// <summary>The distance around the shape, in units.</summary>
    double Perimeter { get; }
}
