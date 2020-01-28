using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SyncService
{
    public class EMailWorker : BackgroundService
    {

        IRepository<EMail> _repository;

        private readonly ILogger<EMailWorker> _logger;
        

        public EMailWorker(ILogger<EMailWorker> logger, IRepository<EMail> repo)
        {
            _logger = logger;
            _repository = repo;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var email = LookupNewMail();
                if(email != null)
                {
                    if (SendMail(email))
                    {
                        SaveState(email, 2);
                    }
                } else
                {
                    _logger.LogInformation("nothing todo... {time}", DateTimeOffset.Now);
                }
                
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void SaveState(EMail email, int state)
        {
            email.State = state;
            _repository.Update(email);
        }

        private EMail LookupNewMail()
        {
            // Fifo
            if(_repository.GetAll().Any(x => x.State == 0))
            {
                var m = _repository.GetAll().First();
                m.State = 1;
                _repository.Update(m);
                return m;
            }
            return null;
        }

        private bool SendMail(EMail mail)
        {
            _logger.LogInformation($"\u001b[36mSending Mail: {mail.Id}: {mail.Subject} To: {mail.To} Time: {DateTimeOffset.Now}\u001b[0m");
            return true;
        }


    }
}
