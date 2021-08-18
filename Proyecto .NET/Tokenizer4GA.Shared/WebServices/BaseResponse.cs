namespace Tokenizer4GA.Shared.WebServices
{
    public class BaseResponse
    {
        public string BearerToken { get; set; }
        public int ResponseCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public sealed class BaseResponse<TData> : BaseResponse
    {
        public TData Data { get; set; }
    }
}
