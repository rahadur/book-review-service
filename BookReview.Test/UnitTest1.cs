namespace BookReview.Test;

public class UnitTest1
{
    public UnitTest1()
    {
        
    }

    
    [Theory]
    [InlineData(10, 3, 13)]
    [InlineData(11, 2, 13)]
    public void Add_TwoSimpleValueShouldCalculate(int x, int y, int expected)
    {
        // Arrange
        // double expected = 11;
        // Act
        double actual = (x + y);
        // Assert
        Assert.Equal(expected, actual);
    }
}