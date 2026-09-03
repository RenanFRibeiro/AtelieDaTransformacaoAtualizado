using System.Drawing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace AtelieDaTransformacao.Desktop.Helpers;

public static class ImageLoader
{
    private static readonly HttpClient Client = new() { Timeout = TimeSpan.FromSeconds(20) };

    public static async Task<Bitmap?> LoadAsync(string? source)
    {
        if (string.IsNullOrWhiteSpace(source)) return null;
        try
        {
            byte[] bytes;
            if (Uri.TryCreate(source, UriKind.Absolute, out var uri) &&
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                bytes = await Client.GetByteArrayAsync(uri);
            else
            {
                if (!File.Exists(source)) return null;
                bytes = await File.ReadAllBytesAsync(source);
            }
            if (bytes.Length == 0) return null;
            try
            {
                using var stream = new MemoryStream(bytes);
                using var image = System.Drawing.Image.FromStream(stream);
                return new Bitmap(image);
            }
            catch { }
            using var sharpStream = new MemoryStream(bytes);
            using var sharp = await SixLabors.ImageSharp.Image.LoadAsync(sharpStream);
            using var pngStream = new MemoryStream();
            await sharp.SaveAsync(pngStream, new PngEncoder());
            pngStream.Position = 0;
            using var png = new Bitmap(pngStream);
            return new Bitmap(png);
        }
        catch { return null; }
    }
}
