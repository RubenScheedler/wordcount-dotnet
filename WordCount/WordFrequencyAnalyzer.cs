using System.Text.RegularExpressions;
using WordCount.Abstractions;

namespace WordCount;

public partial class WordFrequencyAnalyzer : IWordFrequencyAnalyzer
{
    public int CalculateHighestFrequency(string text)
    {
        var words = SplitIntoWords(text.ToLower());

        return CalculateWordFrequencies(words).MaxBy(wordFrequency => wordFrequency.Frequency)?.Frequency ?? 0;
    }

    public int CalculateFrequencyForWord(string text, string wordToCount)
    {
        var words = SplitIntoWords(text.ToLower());

        return words.Count(word => word == wordToCount.ToLower());
    }

    public List<IWordFrequency> CalculateMostFrequentNWords(string text, int n)
    {
        var words = SplitIntoWords(text.ToLower());

        return
        [
            ..CalculateWordFrequencies(words)
            .OrderBy(wordFrequency => wordFrequency.Word)
            .Take(n)
        ];
    }

    private static List<WordFrequency> CalculateWordFrequencies(IEnumerable<string> words)
    {
        return words.GroupBy(word => word).Select(group => new WordFrequency(
            group.Key,
            group.Count()
        )).ToList();
    }

    private static IEnumerable<string> SplitIntoWords(string text)
    {
        var textRemaining = text.ToCharArray().ToList();
        while (textRemaining.Count > 0)
        {
            if (IsAlphabetical(textRemaining[0]))
            {
                textRemaining = TakeWord(textRemaining, out var word);
                yield return word;
            }
            else
            {
                textRemaining = SkipSeparator(textRemaining);
            }
        }
    }

    /// <summary>
    /// Removes characters from the beginning of the character list while they are a separator of words.
    /// </summary>
    /// <param name="characterList"></param>
    /// <returns>A new copy of the list with the separator characters removed from the start.</returns>
    private static List<char> SkipSeparator(IEnumerable<char> characterList)
    {
        return characterList.SkipWhile(c => !IsAlphabetical(c)).ToList();
    }

    /// <summary>
    /// Removes characters from the start of the array while they form a word.
    /// The characters together are outputted in `word` as a string and removed from the start of characterList.
    /// </summary>
    /// <param name="characterList">Text as a character list</param>
    /// <param name="word">Output parameter in which the word is placed</param>
    /// <returns>A new copy of the list with the word characters removed from the start.</returns>
    private static List<char> TakeWord(IReadOnlyCollection<char> characterList, out string word)
    {
        word = new string(characterList.TakeWhile(IsAlphabetical).ToArray());
        
        return characterList.Skip(word.Length).ToList();
    }

    private static bool IsAlphabetical(char character)
    {
        return AlphabeticalRegex().IsMatch(character.ToString());
    }

    [GeneratedRegex("[a-z]", RegexOptions.IgnoreCase, "nl-NL")]
    private static partial Regex AlphabeticalRegex();
}