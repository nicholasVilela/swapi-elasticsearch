using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace swapi_elasticsearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SwapiController : ControllerBase 
    {
        static HttpClient client = new HttpClient();

        [Route("get-all")]
        [HttpGet]
        public async Task<Person> GetAll()
        {
            HttpResponseMessage res = await client.GetAsync("https://swapi.co/api/people");

            Person person = await res.Content.ReadAsAsync<Person>();
        }
    }
}
