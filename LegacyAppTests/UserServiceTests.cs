using LegacyApp;

namespace LegacyAppTests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_Should_Return_False_When_Mail_Without_Dot_And_At()
    {
        // Arrange - rzeczy potrzebne do przeprowadzenia testu.
        var userService = new UserService();

        // Act - przeprowadzenie testu.
        var result = userService.AddUser("John", "Doe", "doegmailpl", new DateTime(2000, 12, 25), 1);

        // Assert - sprawdzenie testu
        Assert.Equal(false, result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Age_Lower_Than_21()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.AddUser("John", "Doe", "doe@gmail.pl", new DateTime(2012, 12, 25), 1);
        
        // Assert
        Assert.Equal(false, result);
    }
}