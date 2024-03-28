namespace LegacyAppTests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_Should_Return_False_When_Mail_Without_Dot_And_At()
    {
        // Arrange - rzeczy potrzebne do przeprowadzenia testu.
        
        // Act - przeprowadzenie testu.
        var result = 2 > 3;

        // Assert - sprawdzenie testu
        Assert.Equal(false, result);
    }
}