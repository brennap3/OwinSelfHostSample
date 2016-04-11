using Microsoft.Owin.Hosting;
using OwinSelfHostSample;
using System;
using System.Net.Http;
using System.Text;

namespace OwinSelfhostSample
{
    public class Program
    {
        static void Main()
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                //[Route("album/{album:alpha}")]
                HttpClient client6 = new HttpClient();

                var response2x = client6.GetAsync(baseAddress + "api/values/D8/1qw11123234529064ef").Result;

                Console.WriteLine(response2x);

                Console.WriteLine(response2x.Content.ReadAsStringAsync().Result);

                client6.Dispose();

                HttpClient client2 = new HttpClient();
                //string id, string pareaid, string platitude, string plongitude

                //api / values

                var postrefuse = new PostRefuse() { id = "D8", pareaid = "x1xx113234143113", platitude = "6", plongitude = "56" };
                Console.WriteLine(postrefuse.id);


                //string baseAddress2 = "http://localhost:9000/api/values/";
                var response2 = client2.PostAsJsonAsync("http://localhost:9000/api/values", postrefuse).Result;
                client2.Dispose();
                Console.WriteLine(response2);

                var putrefuse = new PutRefuse() { id = "D8", pareaid = "x1xx113234143113" };
                Console.WriteLine(putrefuse.id);
                Console.WriteLine(putrefuse.pareaid);

                HttpClient client3 = new HttpClient();

                var response3 = client3.PutAsJsonAsync("http://localhost:9000/api/values", putrefuse).Result;
                client3.Dispose();
                Console.WriteLine(response3);
                client3.Dispose();

                HttpClient client4 = new HttpClient();
                var response4 = client4.DeleteAsync("http://localhost:9000/api/values/x1xx113234143113").Result;
                client4.Dispose();
                Console.WriteLine(response4);

            }

            Console.ReadLine();
        }
    }
}