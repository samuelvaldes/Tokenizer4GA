namespace Tokenizer4GA.Shared.WebServices
{
    public class BaseRequest
    {
        public string BearerToken { get; set; }
        public string AppVersion { get; set; }
    }

    public sealed class BaseRequest<TData> : BaseRequest
    {
        public TData Data { get; set; }
    }
}
