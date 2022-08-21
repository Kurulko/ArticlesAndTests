namespace Articles.Models.ArticleModels
{
    public enum Language
    {
        Zh, En, Fr, De, Pl, Ru, Uk
    }

    public static class LanguageExtensions
    {
        public static string ToStringFullLanguage(this Language language)
            => language switch
            {
                Language.Ru => "Russian",
                Language.En => "English",
                Language.Fr => "French",
                Language.Zh => "Chinese",
                Language.De => "German",
                Language.Pl => "Polish",
                Language.Uk => "Ukraine",
                _ => string.Empty
            };
    }
}
