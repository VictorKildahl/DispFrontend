using F20ITONK.ASPNETCore.MicroService.ClassLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Testfront.Models;

namespace Testfront.Controllers
{
    public class HomeController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "http://host.docker.internal:8888/";

        //string beHost = "http://";

        // GET: Home
        public async Task<ActionResult> Index()
        {
            List<Haandvaerker> haandvaerkers = new List<Haandvaerker>();
            //beHost += Environment.GetEnvironmentVariable("BE_HOST") ?? "localhost:59899";
           // beHost += "/";

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/haandvaerkers");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details received from the web api
                    var HResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response received from web api and storing into the Haandvaerker list
                    haandvaerkers = JsonConvert.DeserializeObject<List<Haandvaerker>>(HResponse);
                }
            }

            return View(haandvaerkers);
        }

        // GET: Haandvaerker/Details/id
        public async Task<ActionResult> Details(int? id)
        {
            Haandvaerker hv = new Haandvaerker();
            if (id == null)
            {
                return new StatusCodeResult((int)System.Net.HttpStatusCode.BadRequest);
            }

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                string urlId = id.ToString();
                HttpResponseMessage Res = await client.GetAsync("api/haandvaerkers/" + urlId);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details received from the web api
                    var HResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response received from web api and storing into the Haandvaerker list
                    hv = JsonConvert.DeserializeObject<Haandvaerker>(HResponse);
                }

            }

            if (hv == null)
            {
                return NotFound();
            }

            return View(hv);
        }

    }
}
