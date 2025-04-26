using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaDataset.Infrastructure.Configuration
{
    public class WorkerSettings
    {
        public int SyncIntervalMinutes { get; set; } = 360;
    }
}
