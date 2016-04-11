using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Auth;
using System.Configuration;
using OwinSelfHostSample;


namespace OwinSelfHostSample
{
    public class RefuseModel
    {

        public CloudTable Table(String tableName)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(tableName);

            return table;
        }

        public IEnumerable<RefuseEntity> Selectbyid(String id, CloudTable table)
        {
            TableQuery<RefuseEntity> query = new TableQuery<RefuseEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id));

            var results = table.ExecuteQuery(query);

            return results;
        }

        public String InsertbyPostrefuse(CloudTable table, PostRefuse postrefuse)
        {

            DateTime nowDate = DateTime.Now;

            string time = "time:" + nowDate;

            DateTime collectionDate = nowDate.AddYears(1000);

            string time2 = "time:" + collectionDate;

            RefuseEntity refuse = new RefuseEntity(postrefuse.id, postrefuse.pareaid, postrefuse.platitude, postrefuse.plongitude, time, time2, "false");

            TableOperation insertOperation = TableOperation.Insert(refuse);
            try {
                table.Execute(insertOperation);
                return "insert executed sucessfully";
            }
            catch (Exception e)
            {
                return "insert not executed sucessfully";
                //do something
            }
        }

        public String UpdatebyPutrefuse(CloudTable table, PutRefuse putrefuse)
        {
            RefuseEntity updateEntity = RetrieveRefuse(table, putrefuse);

            if (updateEntity != null)
            {
                DateTime nowDate = DateTime.Now;
                string time3 = "Time:" + nowDate;
                // Change the collection time
                updateEntity.Timecollected = time3;

                // Create the Replace TableOperation.
                TableOperation updateOperation = TableOperation.Replace(updateEntity);

                // Execute the operation.
                try
                {
                    table.Execute(updateOperation);
                    return "update executed sucessfully";

                }
                catch (Exception e)
                {
                    return "update not executed sucessfully";
                    //do something
                }
            }
            else { return "update not executed sucessfully"; }

        }

        public  RefuseEntity RetrieveRefuse(CloudTable table, PutRefuse putrefuse)
        {
            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<RefuseEntity>(putrefuse.id, putrefuse.pareaid);

            // Execute the operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            // Assign the result to a CustomerEntity object.
            RefuseEntity refuseEntity = (RefuseEntity)retrievedResult.Result;
            return refuseEntity;
        }

        public String Deleteentry(CloudTable table, String id)
        {
            TableQuery<RefuseEntity> query = new TableQuery<RefuseEntity>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));
            var results = table.ExecuteQuery(query);
            String outcome = "";
            // Create the Delete TableOperation.
            foreach (RefuseEntity deleteEntity in results)
                if (deleteEntity != null)
                {
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                    // Execute the operation.
                    try
                    {
                        table.Execute(deleteOperation);
                        outcome="success row deleted";
                        return outcome;

                    }
                    catch (Exception e) {
                        //do soomething
                        outcome = "error ocuured while trying to delete record"+e;
                    }
                }

                else { outcome="row not deleted could not find entity"; }

            return outcome;

        }
    }
}
