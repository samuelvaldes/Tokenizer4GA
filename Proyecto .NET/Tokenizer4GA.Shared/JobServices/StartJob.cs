using Tokenizer4GA.Shared.JobServices.Jobs;
using Tokenizer4GA.Shared.JobServices.Sync;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer4GA.Shared.JobServices
{
    public class StartJob
    {
        public static async Task Start(ISyncInformation context)
        {
            await RunJobs(context);
        }

        private static async Task RunJobs(ISyncInformation context)
        {
            try
            {
                //Scheduler instance
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();

                //start instance
                await scheduler.Start();

                IDictionary<string, object> data = new Dictionary<string, object>();
                data.Add("context", context);

                //Job Sync
                IJobDetail job = JobBuilder.Create<SyncJob>()
                    .UsingJobData(new JobDataMap(data))
                    .WithIdentity("SyncJob", "SyncJob")
                    .Build();

                // Trigger the job to run now, and then repeat every 60 seconds
                ITrigger trigger = TriggerBuilder.Create()
                     .WithIdentity("SyncTrigger1", "SyncJob")
                     .WithSimpleSchedule(x => x
                         .WithIntervalInMinutes(5)
                         .RepeatForever())
                     .Build();
                // Tell quartz to schedule the job using our trigger
                await scheduler.ScheduleJob(job, trigger);

                // some sleep to show what's happening
                // await Task.Delay(TimeSpan.FromSeconds(35));

                // and last shut down the scheduler when you are ready to close your program
                //await scheduler.Shutdown();

            }
            catch (SchedulerException ex)
            {
                Console.Out.WriteLine("SchedulerException. Message:'{0}' ", ex.Message);
            }
        }


    }
}
