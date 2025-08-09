using Android.Content;
using Android.Provider;
using ChicoKoodo.AndroidApp.Interfaces.Platforms.Android;
using ChicoKoodo.AndroidApp.Platforms.Android.Model;
using Uri = Android.Net.Uri;

namespace ChicoKoodo.AndroidApp.Platforms.Android
{
    public class FileHelper : IFileHelper
    {

        private static Uri _externalStorageUri => MediaStore.Files.GetContentUri("external")!;

        public async Task SaveFileAsync(string fileName, string targetPath, string fileContent)
        {
            var contentValues = GenerateContentValues(fileName, targetPath);

            var fileUri = QueryFileUri(fileName, targetPath);

            if (fileUri is null) // This means that the file does not exists
            {
                fileUri = Platform.AppContext.ContentResolver?.Insert(_externalStorageUri, contentValues);
            }

            if (fileUri is null)
            {
                await Shell.Current.DisplayAlert("Error", "Unable to create file URI in Downloads.", "OK");
                return;
            }

            using var output = Platform.AppContext.ContentResolver?.OpenOutputStream(fileUri);

            if (output is null)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to open output stream.", "OK");
                return;
            }

            using var writer = new StreamWriter(output);

            await writer.WriteAsync(fileContent);

        }

        public async Task<string?> ReadFileAsync(string fileName, string targetPath)
        {
            try
            {
                var fileUri = QueryFileUri(fileName, targetPath);

                if (fileUri is null) return null;

                using var input = Platform.AppContext.ContentResolver?.OpenInputStream(fileUri);
                if (input is null)
                    return null;

                using var reader = new StreamReader(input);
                return await reader.ReadToEndAsync();

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Exception", ex.Message, "OK");
                return null;
            }
        }

        private Uri? QueryFileUri(string fileName, string targetPath)
        {
            var resolver = Platform.AppContext.ContentResolver;

            var queryRequest = new MediaStoreQueryRequest(fileName, targetPath);

            using var cursor = resolver?.Query(_externalStorageUri,
                queryRequest.Projection, queryRequest.Selection, queryRequest.SelectionArgs, null);

            if (cursor is null || !cursor.MoveToFirst()) // This means that the file does not exists
            {
                return null;
            }

            var id = cursor.GetLong(cursor.GetColumnIndexOrThrow(IBaseColumns.Id));

            return ContentUris.WithAppendedId(_externalStorageUri, id);
        }

        public async Task<IEnumerable<string>> ReadFilesAsync(string targetPath)
        {
            var results = new List<string>();

            var resolver = Platform.AppContext.ContentResolver;

            var queryRequest = new MediaStoreQueryRequest(targetPath);

            using var cursor = resolver?.Query(_externalStorageUri,
                queryRequest.Projection, queryRequest.Selection, queryRequest.SelectionArgs, null);

            if (cursor is null) return results;

            var idIndex = cursor.GetColumnIndexOrThrow(IBaseColumns.Id);

            while (cursor.MoveToNext())
            {
                var id = cursor.GetLong(idIndex);
                var fileUri = ContentUris.WithAppendedId(_externalStorageUri, id);

                using var stream = resolver?.OpenInputStream(fileUri);

                if (stream is null)
                {
                    await Shell.Current.DisplayAlert("Error", "Failed to open input stream.", "OK");
                    return results;
                }

                using var reader = new StreamReader(stream);
                var content = await reader.ReadToEndAsync();

                results.Add(content);
            }

            return results;
        }

        private ContentValues GenerateContentValues(string fileName, string targetPath)
        {
            var filePath = Path.Combine("Download", targetPath);

            var contentValues = new ContentValues();

            contentValues.Put(MediaStore.IMediaColumns.DisplayName, $"{fileName}.json");
            contentValues.Put(MediaStore.IMediaColumns.MimeType, "application/json");
            contentValues.Put(MediaStore.IMediaColumns.RelativePath, filePath); // ✅ Valid for MediaStore

            return contentValues;
        }
    }
}
