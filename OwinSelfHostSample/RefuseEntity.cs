using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Auth;
using System.Configuration;

namespace OwinSelfHostSample
{
    public class RefuseEntity : TableEntity
    {
        public RefuseEntity()
        {
        }
        public RefuseEntity(string areaname, string areaid, string platitude, string plongitude, DateTime timereported, bool hasbeencollected, DateTime timecollected)
        {
            this.PartitionKey = areaname;
            this.RowKey = areaid;
            this.latitude = platitude;
            this.longitude = plongitude;
            this.Timereported = timereported;
            this.HasBeen_Collected = hasbeencollected;
            this.Timecollected = timecollected;
        }

        public string latitude { get; set; }
        public string longitude { get; set; }
        public DateTime Timereported { get; set; }
        public DateTime Timecollected { get; set; }
        public bool HasBeen_Collected { get; set; }
    }
}
