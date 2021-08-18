namespace Tokenizer4GA.Shared.JobServices.Jobs
{
    using Quartz;
    using System;
    using System.Threading.Tasks;
    using Tokenizer4GA.Shared.JobServices.Sync;

    public class SyncJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await StartSync(context);
        }

        public async Task StartSync(IJobExecutionContext context)
        {
            try
            {
                await Console.Out.WriteLineAsync("****** INICIA SYNCRONICACION ********");
                ISyncInformation _sync = (ISyncInformation)context.JobDetail.JobDataMap.Get("context");

                // Sync Menu
                await _sync.JobHomeMenusAsync();

                await Console.Out.WriteLineAsync("****** FIN SYNCRONICACION ********");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"--- ERROR EN SYNCRONICACION {ex.Message} ---");
            }

        }
    }
}
