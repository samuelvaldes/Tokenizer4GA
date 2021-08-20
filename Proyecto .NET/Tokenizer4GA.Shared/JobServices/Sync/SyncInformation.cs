using Tokenizer4GA.Shared.Constants;
using Tokenizer4GA.Shared.Data;
using Tokenizer4GA.Shared.Localization;
using Tokenizer4GA.Shared.Models.Information;
using Tokenizer4GA.Shared.PlatformServices.Contracts;
using Tokenizer4GA.Shared.PlatformServices.ServiceEnviroment;
using Tokenizer4GA.Shared.WebServices;
using Tokenizer4GA.Shared.WebServices.RestServices;
using System;
using System.Threading.Tasks;

namespace Tokenizer4GA.Shared.JobServices.Sync
{
    public class SyncInformation : BaseRestService, ISyncInformation
    {
        protected override string Server => EnviromentManager.Configurations.ProfileService;
        private static string ServerProfile => EnviromentManager.Configurations.ProfileService;       

        //private readonly ISyncContextGa _ctx;
        private readonly IRequestManagerService _requestManager;
        private readonly IPlatformFunctionalityService _platformFunctionality;

        public bool Internet => _platformFunctionality.CheckInternetConnection();

        public int UserId
        {
            get
            {
                string id = _platformFunctionality.GetSecureDataAsync(StorageKeys.UserId, "0").Result;
                return int.Parse(id);
            }
        }

        public string Token
        {
            get
            {
                string tk = _platformFunctionality.GetSecureDataAsync(StorageKeys.BearerToken).Result;
                return tk;
            }
            set
            {
                _ = _platformFunctionality.SetSecureDataAsync(StorageKeys.BearerToken, value);
            }
        }

        public string Version
        {
            get
            {
                var vs = _platformFunctionality.GetVersion();
                return vs.Version;
            }
        }

        public SyncInformation(ISyncContextGa context,
            IRequestManagerService requestManager,
            IPlatformFunctionalityService platformFunctionality)
        {
            //_ctx = context;
            _requestManager = requestManager;
            _platformFunctionality = platformFunctionality;

            _platformFunctionality.ConnectivityChanged += _platformFunctionality_ConnectivityChanged;
        }

        private void _platformFunctionality_ConnectivityChanged(object sender, EventArgs e)
        {}

        public async Task<BaseResponse> SyncAll(BaseRequest request)
        {
            await Console.Out.WriteLineAsync("****** INICIA SYNC ALL ********");

            // Sync Menu
            await JobHomeMenusAsync();
            _platformFunctionality.CloseLoading();

            await Console.Out.WriteLineAsync("****** TERMINA SYNC ALL ********");

            return new BaseResponse { Success = true, BearerToken = request.BearerToken, Message = "", ResponseCode = 200 };
        }

        public async Task<bool> JobHomeMenusAsync()
        {
            try
            {
                //Hay conexion y exite usuario
                if (Internet && UserId > 0)
                {
                    var request = _requestManager.CreateBaseRequest();
                    request.AppVersion = Version;
                    request.BearerToken = Token;
                    BaseResponse<Menu[]> result = await GetAsync<Menu[]>($"{ServerProfile}GetHomeMenus", request);
                    if (result.Success)
                    {
                        //await _info.SaveMenuListAsync(result.Data);
                        Token = result.BearerToken;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }

        ~SyncInformation() { _platformFunctionality.ConnectivityChanged -= _platformFunctionality_ConnectivityChanged; }
    }
}
