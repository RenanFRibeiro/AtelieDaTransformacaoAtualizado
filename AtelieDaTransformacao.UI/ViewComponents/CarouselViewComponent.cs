using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AtelieDaTransformacao.UI.ViewComponents;

public sealed class CarouselViewComponent : ViewComponent
{
    private readonly IWebHostEnvironment _env;
    public CarouselViewComponent(IWebHostEnvironment env) => _env = env;

    public IViewComponentResult Invoke()
    {
        var folder = Path.Combine(_env.WebRootPath ?? string.Empty, "images", "carousel");
        var list = new List<string>();
        if (Directory.Exists(folder))
        {
            // enumerate and sort by name to keep predictable order (slide1, slide2, ...)
            var files = Directory.EnumerateFiles(folder)
                .Where(f => {
                    var ext = Path.GetExtension(f)?.ToLowerInvariant();
                    return ext is ".jpg" or ".jpeg" or ".png" or ".webp" or ".gif";
                })
                .OrderBy(f => Path.GetFileName(f), StringComparer.OrdinalIgnoreCase);

            foreach (var file in files)
            {
                var rel = Path.Combine("/images/carousel", Path.GetFileName(file)).Replace('\\','/');
                list.Add(rel);
            }
        }
        // fallback images or pages if none provided
        if (!list.Any())
        {
            list.Add("/images/carousel/slide01.jpg");
            list.Add("/images/carousel/slide02.jpg");
            list.Add("/images/carousel/slide03.jpg");
        }

        return View(list);
    }
}
