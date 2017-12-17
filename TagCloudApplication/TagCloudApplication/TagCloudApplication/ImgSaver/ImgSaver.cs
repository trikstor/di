using System.Drawing;
using System.IO;
using System.Linq;

namespace TagCloudApplication.ImgSaver
{
    public class ImgSaver : IImgSaver
    {
        public Result<None> Save(Bitmap img, string path)
        {
            var allowExt = new[] {".png", ".gif", ".jpg"};
            return allowExt.Any(e => e == Path.GetExtension(path))
                ? Result.OfAction(() => img.Save(path)) 
                : Result.Fail<None>("Неподдерживаемое расширение изображения.");
        }
    }
}