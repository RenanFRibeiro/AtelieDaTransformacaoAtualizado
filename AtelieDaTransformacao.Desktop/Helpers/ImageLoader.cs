using System.Drawing;
using SkiaSharp;

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

            // Formatos comuns (jpg/png/bmp/gif) — suportados nativamente pelo Windows, sem dependências extras.
            try
            {
                using var stream = new MemoryStream(bytes);
                using var image = System.Drawing.Image.FromStream(stream);
                return new Bitmap(image);
            }
            catch { }

            // Fallback para formatos que o GDI+ não decodifica (ex: WebP) — SkiaSharp é gratuito (MIT), sem licença.
            using var skBitmap = SKBitmap.Decode(bytes);
            if (skBitmap is null) return null;

            using var skImage = SKImage.FromBitmap(skBitmap);
            using var skData = skImage.Encode(SKEncodedImageFormat.Png, 100);
            using var pngStream = new MemoryStream(skData.ToArray());
            using var png = new Bitmap(pngStream);
            return new Bitmap(png);
        }
        catch { return null; }
    }
}
