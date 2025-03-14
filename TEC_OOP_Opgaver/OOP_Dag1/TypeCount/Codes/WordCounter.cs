namespace TEC_OOP_Opgaver.OOP_Dag1.TypeCountingApp
{
    public class WordCounter
    {
        public SearchResultDTO CountOccurrences(string text, string word)
        {
            // Hvis tekst eller ord mangler, returner 0
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(word))
                return new SearchResultDTO { Text = text, WordToSearch = word, Result = 0 };

            // Tæl ord
            int count = 0;
            foreach (var w in text.Split(' ', '\n', '\r', '\t'))
                if (w.Equals(word, StringComparison.OrdinalIgnoreCase))
                    count++;

            return new SearchResultDTO { Text = text, WordToSearch = word, Result = count };
        }
    }
}