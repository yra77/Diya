
using System.Threading.Tasks;

namespace MyApp.Services
{
    public interface IMediaService
    {
        string SaveToAppFolder(byte[] image, string fileName);
        Task<string> OpenCamera();

    }
}
