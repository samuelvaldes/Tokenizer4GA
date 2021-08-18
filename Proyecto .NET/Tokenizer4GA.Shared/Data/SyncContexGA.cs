namespace Tokenizer4GA.Shared.Data
{
    public class SyncContexGa: ISyncContextGa
    {

        private readonly IDbContext _ctx;
        
        public SyncContexGa(IDbContext ctx)
        {
            _ctx = ctx;          
        }
    }
}
