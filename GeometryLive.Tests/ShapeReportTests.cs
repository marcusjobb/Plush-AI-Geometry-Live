namespace GeometryLive.Tests;

public class ShapeReportTests
{
    private static readonly IShape[] ThreeShapes =
    [
        new Circle(3),      // 28.27
        new Rectangle(6, 4), // 24
        new Triangle(3, 4, 5), // 6
    ];

    [Fact]
    public void Total_area_is_the_sum_of_every_shape()
    {
        var report = new ShapeReport(ThreeShapes);

        Assert.Equal(58.27, report.TotalArea, 2);
    }

    [Fact]
    public void The_largest_shape_wins()
    {
        var report = new ShapeReport(ThreeShapes);

        Assert.Equal("Cirkel", report.Largest.Name);
    }

    [Fact]
    public void A_shape_further_down_the_list_can_be_the_largest()
    {
        var report = new ShapeReport([new Rectangle(1, 1), new Circle(3)]);

        Assert.Equal("Cirkel", report.Largest.Name);
    }

    [Fact]
    public void One_shape_is_its_own_total_and_its_own_largest()
    {
        var report = new ShapeReport([new Rectangle(2, 3)]);

        Assert.Equal(6, report.TotalArea);
        Assert.Equal("Rektangel", report.Largest.Name);
    }

    [Fact]
    public void The_first_shape_wins_a_tie()
    {
        var first = new Rectangle(2, 2);
        var second = new Rectangle(1, 4);

        var report = new ShapeReport([first, second]);

        Assert.Same(first, report.Largest);
    }

    [Fact]
    public void The_report_does_not_care_which_shapes_it_gets()
    {
        IShape custom = new Square(4);

        var report = new ShapeReport([custom, new Circle(1)]);

        Assert.Equal(19.14, report.TotalArea, 2);
        Assert.Equal("Kvadrat", report.Largest.Name);
    }

    [Fact]
    public void An_empty_report_is_rejected()
    {
        Assert.Throws<ArgumentException>(() => new ShapeReport([]));
    }

    [Fact]
    public void A_missing_collection_is_rejected()
    {
        Assert.Throws<ArgumentNullException>(() => new ShapeReport(null!));
    }

    /// <summary>
    /// A shape that did not exist when <see cref="IShape"/> was written.
    /// It slots in without a single change to <see cref="ShapeReport"/>.
    /// </summary>
    private sealed class Square : IShape
    {
        public Square(double side) => Side = Guard.Positive(side, nameof(side));

        public double Side { get; }

        public string Name => "Kvadrat";

        public double Area => Side * Side;

        public double Perimeter => 4 * Side;
    }
}
