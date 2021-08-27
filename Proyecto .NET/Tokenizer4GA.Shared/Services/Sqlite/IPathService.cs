namespace Tokenizer4GA.Shared.Services.Sqlite
{
    public interface IPathService
    {
        string GetDatabasePath();
        string GetCertificatePath();
        bool ExistCertificate();
    }
}
