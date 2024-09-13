using WordCount.Abstractions;

namespace WordCount;

public record WordFrequency(string Word, int Frequency) : IWordFrequency;