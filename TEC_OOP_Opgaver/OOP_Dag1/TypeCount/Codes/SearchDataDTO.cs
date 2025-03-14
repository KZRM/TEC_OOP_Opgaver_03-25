// Placeret i det globale namespace (ingen namespace-deklaration)
public struct SearchDataDTO
{
    public string Word;
    public string SiteUrl;
    public string Chapter;
    public int CheckLength;

    // Eksplicit constructor med parametre
    public SearchDataDTO(string searchWord, string siteUrl, string chapter, int wordCheckAmount)
    {
        Word = searchWord;
        SiteUrl = siteUrl;
        Chapter = chapter;
        CheckLength = wordCheckAmount;
    }
}