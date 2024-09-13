namespace WordCount.Abstractions;

public interface IWordFrequency {
    string Word { get; }
    int Frequency { get; }
}