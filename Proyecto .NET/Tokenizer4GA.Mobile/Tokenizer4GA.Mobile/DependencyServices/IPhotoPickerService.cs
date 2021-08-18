using System.Threading.Tasks;

namespace Tokenizer4GA.Mobile.DependencyServices
{
    public interface IPhotoPickerService
    {
        Task<string> GetImageBase64Async();
    }
}
