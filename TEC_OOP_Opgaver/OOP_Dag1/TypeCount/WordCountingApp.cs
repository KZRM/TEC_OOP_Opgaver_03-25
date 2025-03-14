namespace TEC_OOP_Opgaver.OOP_Dag1.TypeCountingApp
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    class WordCountingApp
    {
        public WordCountingApp()
        {
            // Opret struct-DTO instans
            var search = new SearchDataDTO("type",
                "https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/",
                "The common type system",
                2000);

            Console.WriteLine($"Søger efter '{search.Word}' i '{search.Chapter}' på:\n{search.SiteUrl}\n");

            RunWordCountingApp(search).GetAwaiter().GetResult();
        }

        static async Task RunWordCountingApp(SearchDataDTO search)
        {
            // Hent WordCounter via DI
            var counter = Program.serviceProvider.GetService<WordCounter>()!;
            string html = await new HttpClient().GetStringAsync(search.SiteUrl);

            SearchResultDTO result = counter.CountOccurrences(
                ExtractSection(html, search),
                search.Word);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\"{result.WordToSearch}\" dukker op {result.Result} gange i de første " +
                              $"{search.CheckLength} ord i '{search.Chapter}'.\nTryk en tast for at fortsætte\n");
            Console.ResetColor();

            Console.ReadKey(true);
        }

        private static string ExtractSection(string html, SearchDataDTO search)
        {
            int start = html.IndexOf(search.Chapter, StringComparison.OrdinalIgnoreCase);
            return start < 0 ? "" : html.Substring(start, Math.Min(search.CheckLength, html.Length - start));
        }
    }
}