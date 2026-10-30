namespace GeometryLive.Tests;

public class CircleTests
{
    [Fact]
    public void Area_is_pi_times_the_radius_squared()
    {
        var circle = new Circle(3);

        Assert.Equal(28.27, circle.Area, 2);
    }

    [Fact]
    public void Perimeter_is_pi_times_the_diameter()
    {
        var circle = new Circle(3);

        Assert.Equal(18.85, circle.Perimeter, 2);
    }

    [Fact]
    public void A_radius_of_one_gives_pi_and_two_pi()
    {
        var circle = new Circle(1);

        Assert.Equal(Math.PI, circle.Area, 6);
        Assert.Equal(2 * Math.PI, circle.Perimeter, 6);
    }

    [Fact]
    public void The_radius_is_kept_as_given()
    {
        Assert.Equal(2.5, new Circle(2.5).Radius);
    }

    [Fact]
    public void It_presents_itself_as_Cirkel()
    {
        Assert.Equal("Cirkel", new Circle(3).Name);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(double.NaN)]
    public void A_radius_that_is_not_positive_is_rejected(double radius)
    {
        Assert.Throws<ArgumentException>(() => new Circle(radius));
    }

    [Fact]
    public void It_can_be_used_as_a_plain_shape()
    {
        IShape shape = new Circle(3);

        Assert.Equal(28.27, shape.Area, 2);
    }
}
