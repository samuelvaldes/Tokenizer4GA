using Tokenizer4GA.Shared.WebServices;
using System.Threading.Tasks;

namespace Tokenizer4GA.Shared.PlatformServices.Contracts
{
    public interface IRequestManagerService
    {
        BaseRequest CreateBaseRequest();
        BaseRequest<TData> CreateBaseRequest<TData>(TData data);
        BaseRequest<TData> CreateBaseRequest<TData>(TData data, string appVersion);
        Task<BaseResponse> ExecuteRequestAsync(Task<BaseResponse> request, bool disableLoader = false);
        Task<BaseResponse<TData>> ExecuteRequestAsync<TData>(Task<BaseResponse<TData>> request, bool disableLoader = false);
    }
}
