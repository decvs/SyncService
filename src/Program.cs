using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SyncService
{
    public class Program
    {
        private static Repository<EMail> TestRepository = new Repository<EMail>();
        

        public static void Main(string[] args)
        {
            TestRepository.Insert(new EMail() { Id = 1, Subject = "Test 1", To = "a@a.de", State = 0 });
            TestRepository.Insert(new EMail() { Id = 2, Subject = "Test 2", To = "b@a.de", State = 0 });
            TestRepository.Insert(new EMail() { Id = 3, Subject = "Test 3", To = "c@a.de", State = 0 });
            TestRepository.Insert(new EMail() { Id = 4, Subject = "Test 4", To = "d@a.de", State = 0 });
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IRepository<EMail>, Repository<EMail>>(provider => TestRepository);
                    services.AddHostedService<Worker>();
                    services.AddHostedService<ImportWorker>();
                    services.AddHostedService<EMailWorker>();
                });
    }
}
