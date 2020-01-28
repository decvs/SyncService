using System;
using System.Collections.Generic;
using System.Text;

namespace SyncService
{
    public class EMail : Entity
    {

        public string From { get; set; }
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
        public int State { get; set; } // 0 = new, 1 = selected, 2 = done
    }
}
