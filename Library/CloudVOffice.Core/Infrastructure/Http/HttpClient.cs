

using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CloudVOffice.Core.Infrastructure.Http
{

    public class HttpWebClients : IHttpWebClients
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpWebClients(IHttpContextAccessor httpContextAccessor)
        {

            _httpContextAccessor = httpContextAccessor;
        }



        public string PostRequest(string Url, string parameterValues, bool isAnonymous)
        {

            string URL = Url;
            string jsonString = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.Timeout = TimeSpan.FromMinutes(20);
                //GET Method  
                HttpContent c = new StringContent(parameterValues, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(URL, c).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    jsonString = response.Content.ReadAsStringAsync()
                                                   .Result
                                                   //.Replace("\\", "")
                                                   //.Replace("\r\n", "'")
                                                   .Trim(new char[1] { '"' });
                }



            }
            return jsonString;


        }



        public async Task<string> GetRequest(string Url, bool isAnonymous = true)
        {



            string URL = Url;
            string jsonString = null;
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://" + host + URL);
                client.DefaultRequestHeaders.Accept.Clear();

                //GET Method  
                var response = await client.GetAsync(URL);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    jsonString = response.Content.ReadAsStringAsync()
                                                   .Result
                                                   //.Replace("\\", "")
                                                   .Trim(new char[1] { '"' });

                }

            }
            return jsonString;


        }



    }
}
