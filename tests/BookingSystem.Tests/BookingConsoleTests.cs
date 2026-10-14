using BookingSystem.ConsoleUI;

namespace BookingSystem.Tests;

public class BookingConsoleTests
{
    [Fact]
    public void ReadValidParticipants_ValidInput_ReturnsNumber()
    {
        // Arrange
        var originalInput = Console.In;
        Console.SetIn(new StringReader("4"));

        // Act
        var result = BookingConsole.ReadValidParticipants();

        // Assert
        Assert.Equal(4, result);
        Console.SetIn(originalInput);
    }

    [Fact]
    public void ReadValidParticipants_InvalidInput_AllowsRetry()
    {
        // Arrange
        var originalInput = Console.In;
        Console.SetIn(new StringReader("abc" + Environment.NewLine + "4"));

        // Act
        var result = BookingConsole.ReadValidParticipants();

        // Assert
        Assert.Equal(4, result);

        Console.SetIn(originalInput);
    }
}
