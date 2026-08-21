namespace MemoryPerformance.Showcase.Performance;

public static class TextMetrics
{
    public static int CountWordsBaseline(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        return text.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    public static int CountWords(ReadOnlySpan<char> text)
    {
        var wordCount = 0;
        var insideWord = false;

        foreach (var character in text)
        {
            if (char.IsWhiteSpace(character))
            {
                insideWord = false;
                continue;
            }

            if (!insideWord)
            {
                wordCount++;
                insideWord = true;
            }
        }

        return wordCount;
    }
}