using System.Collections.Generic;

namespace ChicoKoodo.AndroidApp.Models
{
    public class NihongoData
    {
        public Guid? Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Level { get; set; } = string.Empty;

        public string Topic { get; set; } = string.Empty;

        public string NihongoSentence { get; set; } = string.Empty;

        public string EnglishSentence { get; set; } = string.Empty;

        public string Reference { get; set; } = string.Empty;

        public List<string> Helpers { get; init; } = [];

        public List<string> OtherCorrectNihongoSentences { get; init; } = [];

        public void Validate()
        {
            if (Id is null || Id == Guid.Empty) throw new ArgumentNullException(nameof(Id));
            ArgumentNullException.ThrowIfNullOrEmpty(Type);
            ArgumentNullException.ThrowIfNullOrEmpty(Level);
            ArgumentNullException.ThrowIfNullOrEmpty(Topic);
            ArgumentNullException.ThrowIfNullOrEmpty(NihongoSentence);
            ArgumentNullException.ThrowIfNullOrEmpty(EnglishSentence);
            ArgumentNullException.ThrowIfNullOrEmpty(Reference);
        }
    }
}
