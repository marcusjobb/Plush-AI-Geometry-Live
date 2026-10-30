namespace GeometryLive.Tests;

public class GuardTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(0.001)]
    [InlineData(1e9)]
    public void A_positive_number_is_returned_unchanged(double value)
    {
        Assert.Equal(value, Guard.Positive(value, "side"));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-0.5)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    public void Anything_else_is_rejected(double value)
    {
        Assert.Throws<ArgumentException>(() => Guard.Positive(value, "side"));
    }

    [Fact]
    public void The_message_names_the_parameter_to_blame()
    {
        var error = Assert.Throws<ArgumentException>(() => Guard.Positive(0, "radius"));

        Assert.Contains("radius", error.Message);
        Assert.Equal("radius", error.ParamName);
    }
}
