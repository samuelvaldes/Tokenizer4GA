using System;
using System.Collections.Generic;
using System.Text;

namespace Tokenizer4GA.Shared.WebServices
{
    public static class BaseRequestExtensions
    {
        public static BaseResponse<TData> GetResponse<TData>(this BaseRequest<TData> request, bool success, int responseCode, string message = "")
        {
            BaseResponse<TData> response = new BaseResponse<TData>
            {
                Success = success,
                BearerToken = request.BearerToken,
                ResponseCode = responseCode,
                Message = message,
                Data = request.Data
            };

            return response;
        }

        public static BaseResponse<TData> GetResponse<TData>(this BaseRequest request, TData data, bool success, int responseCode, string message = "")
        {
            BaseResponse<TData> response = new BaseResponse<TData>
            {
                Success = success,
                BearerToken = request.BearerToken,
                ResponseCode = responseCode,
                Message = message,
                Data = data
            };

            return response;
        }
    }
}
