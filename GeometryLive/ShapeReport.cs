namespace GeometryLive;

/// <summary>
/// Answers questions about a collection of shapes. It never asks what type a
/// shape is — it only uses what <see cref="IShape"/> promises.
/// </summary>
public sealed class ShapeReport
{
    private readonly IReadOnlyList<IShape> _shapes;

    /// <summary>Creates a report over <paramref name="shapes"/>.</summary>
    /// <param name="shapes">At least one shape.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="shapes"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="shapes"/> is empty.</exception>
    public ShapeReport(IEnumerable<IShape> shapes)
    {
        ArgumentNullException.ThrowIfNull(shapes);

        _shapes = shapes.ToList();

        if (_shapes.Count == 0)
        {
            throw new ArgumentException("A report needs at least one shape.", nameof(shapes));
        }
    }

    /// <summary>The summed area of every shape in the report.</summary>
    public double TotalArea => _shapes.Sum(shape => shape.Area);

    /// <summary>The shape with the largest area. The first one wins a tie.</summary>
    public IShape Largest
    {
        get
        {
            var largest = _shapes[0];

            foreach (var shape in _shapes)
            {
                if (shape.Area > largest.Area)
                {
                    largest = shape;
                }
            }

            return largest;
        }
    }
}
