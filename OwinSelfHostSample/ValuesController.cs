using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Auth;
using System.Configuration;
using OwinSelfHostSample;

namespace OwinSelfhostSample
{
    public class ValuesController : ApiController
    {
        // GET api/values 
        public IEnumerable<RefuseEntity> Get(String id)
        {

            //string pcode = "Dublin 7";

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("Refuse");

            TableQuery<RefuseEntity> query = new TableQuery<RefuseEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id));

            var results = table.ExecuteQuery(query);

            //var json = JsonConvert.SerializeObject(results);

            return results;
        }

        // GET api/values/5 
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values 
        public void Post([FromBody] PostRefuse postrefuse)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("Refuse");

            DateTime nowDate = DateTime.Now;

            DateTime collectionDate = nowDate.AddYears(1000);

            RefuseEntity refuse = new RefuseEntity(postrefuse.id, postrefuse.pareaid, postrefuse.platitude, postrefuse.plongitude, nowDate, false, collectionDate);

            TableOperation insertOperation = TableOperation.Insert(refuse);

            table.Execute(insertOperation);
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]PostRefuse postrefuse)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
