using Android.Provider;

namespace ChicoKoodo.AndroidApp.Platforms.Android.Model
{
    public class MediaStoreQueryRequest
    {
        public string[] Projection { get; set; } = [ IBaseColumns.Id ];

        public string[] SelectionArgs { get; set; } = [];

        public string Selection { get; set; } = string.Empty;

        public MediaStoreQueryRequest(string fileName, string targetPath)
        {
            Selection = string.Concat(MediaStore.IMediaColumns.DisplayName,
                " = ? AND ", MediaStore.IMediaColumns.RelativePath, " = ?");

            var relativePath = Path.Combine("Download", targetPath);

            SelectionArgs = [$"{fileName}.json", $"{relativePath}/"];
        }

        public MediaStoreQueryRequest(string targetPath)
        {
            Selection = $"{MediaStore.IMediaColumns.RelativePath} = ?";

            var relativePath = Path.Combine("Download", targetPath);

            SelectionArgs = [$"{relativePath}/"];
        }
    }
}
