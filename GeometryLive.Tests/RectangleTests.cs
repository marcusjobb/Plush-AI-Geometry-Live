namespace GeometryLive.Tests;

public class RectangleTests
{
    [Fact]
    public void Area_is_width_times_height()
    {
        var rectangle = new Rectangle(6, 4);

        Assert.Equal(24, rectangle.Area);
    }

    [Fact]
    public void Perimeter_is_twice_the_sum_of_the_sides()
    {
        var rectangle = new Rectangle(6, 4);

        Assert.Equal(20, rectangle.Perimeter);
    }

    [Fact]
    public void A_square_is_a_rectangle_with_equal_sides()
    {
        var square = new Rectangle(5, 5);

        Assert.Equal(25, square.Area);
        Assert.Equal(20, square.Perimeter);
    }

    [Fact]
    public void It_presents_itself_as_Rektangel()
    {
        Assert.Equal("Rektangel", new Rectangle(1, 1).Name);
    }

    [Theory]
    [InlineData(0, 1, "width")]
    [InlineData(-1, 1, "width")]
    [InlineData(1, 0, "height")]
    [InlineData(1, -1, "height")]
    public void A_side_that_is_not_positive_is_rejected(double width, double height, string blamed)
    {
        var error = Assert.Throws<ArgumentException>(() => new Rectangle(width, height));

        Assert.Equal(blamed, error.ParamName);
    }

    [Fact]
    public void It_can_be_used_as_a_plain_shape()
    {
        IShape shape = new Rectangle(6, 4);

        Assert.Equal(24, shape.Area);
    }
}
