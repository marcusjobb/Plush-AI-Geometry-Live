namespace GeometryLive.Tests;

public class SquareTests
{
    [Fact]
    public void Area_is_the_side_squared()
    {
        var square = new Square(5);

        Assert.Equal(25, square.Area);
    }

    [Fact]
    public void Perimeter_is_four_sides()
    {
        var square = new Square(5);

        Assert.Equal(20, square.Perimeter);
    }

    [Fact]
    public void The_side_is_kept_as_given()
    {
        Assert.Equal(2.5, new Square(2.5).Side);
    }

    [Fact]
    public void It_presents_itself_as_Kvadrat()
    {
        Assert.Equal("Kvadrat", new Square(1).Name);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-0.0)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    public void A_side_that_is_not_positive_is_rejected(double side)
    {
        Assert.Throws<ArgumentException>(() => new Square(side));
    }

    [Fact]
    public void The_message_names_the_parameter_to_blame()
    {
        var error = Assert.Throws<ArgumentException>(() => new Square(-5));

        Assert.Equal("side", error.ParamName);
    }

    [Fact]
    public void It_can_be_used_as_a_plain_shape()
    {
        IShape shape = new Square(5);

        Assert.Equal(25, shape.Area);
    }
}
