using Microsoft.Owin.Hosting;
using OwinSelfHostSample;
using System;
using System.Net.Http;

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
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();

                var response = client.GetAsync(baseAddress + "api/values/Dublin7").Result;

                client.Dispose();
                Console.WriteLine(response);
                
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                HttpClient client2 = new HttpClient();
                //string id, string pareaid, string platitude, string plongitude

                //api / values
                var postrefuse = new PostRefuse() { id="Dublins 7", pareaid="D 7",platitude="6",plongitude="56"};
                Console.WriteLine(postrefuse.id);
                Console.WriteLine(postrefuse.pareaid);
                Console.WriteLine(postrefuse.platitude);
                Console.WriteLine(postrefuse.plongitude);

                //string baseAddress2 = "http://localhost:9000/api/values/";
                var response2 = client2.PostAsJsonAsync("http://localhost:9000/api/values", postrefuse).Result;
                client2.Dispose();
                Console.WriteLine(response2);
                
               

            }

            Console.ReadLine();
        }
    }
}