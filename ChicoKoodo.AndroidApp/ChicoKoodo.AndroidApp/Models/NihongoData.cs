using System.Collections.Generic;

namespace ChicoKoodo.AndroidApp.Models
{
    public class NihongoData
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Level { get; set; } = string.Empty;

        public string Topic { get; set; } = string.Empty;

        public string NihongoSentence { get; set; } = string.Empty;

        public string EnglishSentence { get; set; } = string.Empty;

        public string Reference { get; set; } = string.Empty;

        public List<string> Helpers { get; init; } = [];

        public List<string> OtherCorrectNihongoSentences { get; init; } = [];
    }
}
