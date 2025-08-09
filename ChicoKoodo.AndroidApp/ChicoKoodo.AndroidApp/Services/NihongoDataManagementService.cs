using ChicoKoodo.AndroidApp.Interfaces.Platforms.Android;
using ChicoKoodo.AndroidApp.Models;
using ChicoKoodo.AndroidApp.Utilities;
using System.Text.Json;

namespace ChicoKoodo.AndroidApp.Services
{
    public class NihongoDataManagementService
    {
        private readonly IFileHelper _fileHelper;

        public NihongoDataManagementService(IFileHelper fileHelper)
        {
            ArgumentNullException.ThrowIfNull(fileHelper, nameof(fileHelper));

            _fileHelper = fileHelper;
        }

        public async Task<IEnumerable<NihongoData>> GetNihongoDataAsync(string type, string level)
        {
            var targetPath = Path.Combine("Nihongo", level, type);

            var result = new List<NihongoData>();

            var nihongoData = await _fileHelper.ReadFilesAsync(targetPath);

            foreach (var data in nihongoData)
            {
                var deserializedData = JsonSerializer.Deserialize<NihongoData>(data, 
                    JsonSerializerHelper.NihongoSerializerOption());

                if (deserializedData is null)
                {
                    // TODO: What to do with this?? Perhaps log error, where?
                    continue;
                }

                result.Add(deserializedData);
            }

            return result;
        }

        public async Task SaveNihongoDataAsync(NihongoData data)
        {
            var targetPath = Path.Combine("Nihongo", data.Level, data.Type);

            var serializedData = JsonSerializer.Serialize(data, 
                Utilities.JsonSerializerHelper.NihongoSerializerOption());

            await _fileHelper.SaveFileAsync(data.Id.ToString(), targetPath, serializedData);
        }
    }
}
