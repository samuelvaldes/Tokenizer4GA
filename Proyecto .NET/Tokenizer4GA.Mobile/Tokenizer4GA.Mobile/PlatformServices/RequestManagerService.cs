using Acr.UserDialogs;
using Tokenizer4GA.Shared.Localization;
using Tokenizer4GA.Shared.PlatformServices.Contracts;
using Tokenizer4GA.Shared.WebServices;
using System.Threading.Tasks;

namespace Tokenizer4GA.Mobile.PlatformServices
{
    class RequestManagerService : IRequestManagerService
    {
        public BaseRequest CreateBaseRequest() =>
            new BaseRequest
            {
            };

        public BaseRequest<TData> CreateBaseRequest<TData>(TData data) =>
            new BaseRequest<TData>
            {
                Data = data
            };

        public BaseRequest<TData> CreateBaseRequest<TData>(TData data, string appVersion) =>
            new BaseRequest<TData>
            {
                Data = data,
                AppVersion = appVersion
            };

        public async Task<BaseResponse> ExecuteRequestAsync(Task<BaseResponse> request, bool disableLoader = false)
        {
            if (disableLoader)
                return await request;
            else
                using (UserDialogs.Instance.Loading(LocalizedStrings.Loading))
                    return await request;
        }

        public async Task<BaseResponse<TData>> ExecuteRequestAsync<TData>(Task<BaseResponse<TData>> request, bool disableLoader = false)
        {
            if (disableLoader)
                return await request;
            else
                using (UserDialogs.Instance.Loading(LocalizedStrings.Loading))
                    return await request;
        }
    }
}
