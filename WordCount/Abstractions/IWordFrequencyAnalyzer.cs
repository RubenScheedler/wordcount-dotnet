namespace WordCount.Abstractions;

public interface IWordFrequencyAnalyzer {
    int CalculateHighestFrequency(string text);
    int CalculateFrequencyForWord (string text, string word);
    List<IWordFrequency> CalculateMostFrequentNWords (string text, int n);
}