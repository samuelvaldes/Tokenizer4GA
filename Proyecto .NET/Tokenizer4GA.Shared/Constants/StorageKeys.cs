namespace Tokenizer4GA.Shared.Constants
{
    public static class StorageKeys
    {
        // Secure storage
        public const string BearerToken = nameof(BearerToken);
        public const string DeviceGuid = nameof(DeviceGuid);
        public const string UserId = nameof(UserId);
        public const string IsStorageUser = nameof(IsStorageUser);
        public const string Base64 = nameof(Base64);

        // Standard storage
        public const string FcmTokenPendingUpload = nameof(FcmTokenPendingUpload);
        public const string UsedMockServices = nameof(UsedMockServices);

        public const string HomeMenuPage = nameof(HomeMenuPage);
        public const string MustChangePassword = nameof(MustChangePassword);
    }
}
