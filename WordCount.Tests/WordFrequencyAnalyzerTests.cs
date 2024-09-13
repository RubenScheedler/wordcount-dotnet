using FluentAssertions;

namespace WordCount.Tests;

public class WordFrequencyAnalyzerTests
{
    private readonly WordFrequencyAnalyzer _systemUnderTest = new();
    
    [Fact]
    public void CalculateFrequencyForWord_TextIsOneWord_Returns1()
    {
        // Arrange
        var text = "apple";
        var word = "apple";
        
        // Act
        var actual = _systemUnderTest.CalculateFrequencyForWord(text, word);
        
        // Assert
        actual.Should().Be(1);
    }
    
    [Theory]
    [InlineData("Apple")]
    [InlineData("APPLE")]
    [InlineData("aPple")]
    public void CalculateFrequencyForWord_WordIsCapitalised_Returns1(string word)
    {
        // Arrange
        var text = "apple";
        
        // Act
        var actual = _systemUnderTest.CalculateFrequencyForWord(text, word);
        
        // Assert
        actual.Should().Be(1);
    }
    
    [Theory]
    [InlineData("Apple")]
    [InlineData("APPLE")]
    [InlineData("aPple")]
    public void CalculateFrequencyForWord_TextIsCapitalised_Returns1(string text)
    {
        // Arrange
        var word = "apple";
        // Act
        var actual = _systemUnderTest.CalculateFrequencyForWord(text, word);
        
        // Assert
        actual.Should().Be(1);
    }
    
    [Theory]
    [InlineData("apple!")]
    [InlineData("apple?!")]
    [InlineData("apple  ")]
    [InlineData("  apple  ")]
    [InlineData("...apple")]
    public void CalculateFrequencyForWord_TextContainsSeparatorCharacters_Returns1(string text)
    {
        // Arrange
        var word = "apple";
        
        // Act
        var actual = _systemUnderTest.CalculateFrequencyForWord(text, word);
        
        // Assert
        actual.Should().Be(1);
    }
    
    [Fact]
    public void CalculateFrequencyForWord_TextContainsTwoWords_Returns1()
    {
        // Arrange
        var text = "An apple";
        var word = "apple";
        
        // Act
        var actual = _systemUnderTest.CalculateFrequencyForWord(text, word);
        
        // Assert
        actual.Should().Be(1);
    }
    
    [Fact]
    public void CalculateFrequencyForWord_TextContainsAWordTwoTimes_Returns2()
    {
        // Arrange
        var text = "An apple is an apple.";
        var word = "apple";
        
        // Act
        var actual = _systemUnderTest.CalculateFrequencyForWord(text, word);
        
        // Assert
        actual.Should().Be(2);
    }
    
    [Fact]
    public void CalculateFrequencyForWord_TextContainsAWordTwoTimesWithDifferentCasings_Returns2()
    {
        // Arrange
        var text = "An apple is an apple.";
        var word = "an";
        
        // Act
        var actual = _systemUnderTest.CalculateFrequencyForWord(text, word);
        
        // Assert
        actual.Should().Be(2);
    }

    [Fact]
    public void CalculateMostFrequentNWords_OneWord_ReturnsWordWithFrequency1()
    {
        // Arrange
        var text = "apple";
        
        // Act
        var actual = _systemUnderTest.CalculateMostFrequentNWords(text, 1);
        
        // Assert
        actual[0].Word.Should().Be("apple");
        actual[0].Frequency.Should().Be(1);
    }
    
    [Fact]
    public void CalculateMostFrequentNWords_MultipleWords_ReturnsWordWithHighestFrequency()
    {
        // Arrange
        var text = "apple apple banana";
        
        // Act
        var actual = _systemUnderTest.CalculateMostFrequentNWords(text, 1);
        
        // Assert
        actual[0].Word.Should().Be("apple");
        actual[0].Frequency.Should().Be(2);
    }
    
    [Fact]
    public void CalculateMostFrequentNWords_WordsTieInFrequency_ReturnsAlphabeticallyFirst()
    {
        // Arrange
        var text = "blueberry blueberry banana banana";
        
        // Act
        var actual = _systemUnderTest.CalculateMostFrequentNWords(text, 1);
        
        // Assert
        actual[0].Word.Should().Be("banana");
        actual[0].Frequency.Should().Be(2);
    }
    
    [Fact]
    public void CalculateMostFrequentNWords_3WordsAndNIs3_Returns3Frequencies()
    {
        // Arrange
        var text = "blueberry blueberry. banana! apple apple apple?";
        
        // Act
        var actual = _systemUnderTest.CalculateMostFrequentNWords(text, 3);
        
        // Assert
        actual[0].Word.Should().Be("apple");
        actual[0].Frequency.Should().Be(3);
        
        actual[1].Word.Should().Be("banana");
        actual[1].Frequency.Should().Be(1);
        
        actual[2].Word.Should().Be("blueberry");
        actual[2].Frequency.Should().Be(2);
    }

    [Fact]
    public void CalculateHighestFrequency_OneWordInText_ReturnsFrequencyOfMostFrequentWord()
    {
        // Arrange
        var text = "blueberry blueberry";
        
        // Act
        var actual = _systemUnderTest.CalculateHighestFrequency(text);

        // Assert
        actual.Should().Be(2);
    }
    
    [Fact]
    public void CalculateHighestFrequency_MultipleWordsInText_ReturnsFrequencyOfMostFrequentWord()
    {
        // Arrange
        var text = "blueberry blueberry. banana! apple apple apple?";
        
        // Act
        var actual = _systemUnderTest.CalculateHighestFrequency(text);

        // Assert
        actual.Should().Be(3); // apple occurs the most: 3 times
    }
    
    [Fact]
    public void CalculateHighestFrequency_EmptyText_Returns0()
    {
        // Arrange
        var text = "";
        
        // Act
        var actual = _systemUnderTest.CalculateHighestFrequency(text);

        // Assert
        actual.Should().Be(0);
    }
}