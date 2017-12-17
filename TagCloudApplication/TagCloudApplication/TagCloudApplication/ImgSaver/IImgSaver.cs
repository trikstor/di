using System.Drawing;

namespace TagCloudApplication.ImgSaver
{
    public interface IImgSaver
    {
        Result<None> Save(Bitmap img, string path);
    }
}