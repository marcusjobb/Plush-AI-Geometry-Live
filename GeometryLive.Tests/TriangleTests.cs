namespace GeometryLive.Tests;

public class TriangleTests
{
    [Fact]
    public void The_classic_3_4_5_triangle_has_area_six()
    {
        var triangle = new Triangle(3, 4, 5);

        Assert.Equal(6, triangle.Area, 2);
    }

    [Fact]
    public void Perimeter_is_the_sum_of_the_sides()
    {
        var triangle = new Triangle(3, 4, 5);

        Assert.Equal(12, triangle.Perimeter);
    }

    [Fact]
    public void An_equilateral_triangle_with_side_two_is_the_square_root_of_three()
    {
        var triangle = new Triangle(2, 2, 2);

        Assert.Equal(Math.Sqrt(3), triangle.Area, 4);
    }

    [Fact]
    public void It_presents_itself_as_Triangel()
    {
        Assert.Equal("Triangel", new Triangle(3, 4, 5).Name);
    }

    [Theory]
    [InlineData(0, 4, 5)]
    [InlineData(3, -4, 5)]
    [InlineData(double.NaN, 4, 5)]
    public void A_side_that_is_not_positive_is_rejected(double a, double b, double c)
    {
        Assert.Throws<ArgumentException>(() => new Triangle(a, b, c));
    }

    [Theory]
    [InlineData(1, 1, 3)]
    [InlineData(10, 2, 2)]
    [InlineData(1, 2, 3)]
    public void Sides_that_cannot_meet_are_rejected(double a, double b, double c)
    {
        var error = Assert.Throws<ArgumentException>(() => new Triangle(a, b, c));

        Assert.Contains("cannot form a triangle", error.Message);
    }

    [Fact]
    public void A_just_barely_possible_triangle_does_not_go_nan()
    {
        var thin = new Triangle(1, 1, 1.9999999999);

        Assert.False(double.IsNaN(thin.Area));
        Assert.True(thin.Area >= 0);
    }

    [Fact]
    public void It_can_be_used_as_a_plain_shape()
    {
        IShape shape = new Triangle(3, 4, 5);

        Assert.Equal(6, shape.Area, 2);
    }
}
