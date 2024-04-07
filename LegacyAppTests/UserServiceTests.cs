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
        var result = userService.AddUser("John", "Doe", "doe@gmail.pl", new DateTime(2012, 12, 25), 4);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Limit_Lower_Than_500()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.AddUser("Jan", "Kowalski", "kowalski@wp.pl", new DateTime(1995, 12, 25), 1);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Name_Null()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.AddUser(null, "Andrzejewicz", "andrzejewicz@wp.pl", new DateTime(1995, 12, 25), 6);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void AddUser_Should_Return_True_When_CreditLimit_Above_500()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.AddUser("John", "Doe", "doe@gmail.pl", new DateTime(1998, 12, 25), 4);

        // Assert
        Assert.Equal(true, result);
    }
}