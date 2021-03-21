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
            List<Recipe> recipes = new List<Recipe>();
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
                HttpResponseMessage Res = await client.GetAsync("api/recipes");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details received from the web api
                    var HResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response received from web api and storing into the Recipe list
                    recipes = JsonConvert.DeserializeObject<List<Recipe>>(HResponse);
                }
            }

            return View(recipes);
        }

        // GET: Recipe/Details/id
        public async Task<ActionResult> Details(int? id)
        {
            Recipe rp = new Recipe();
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
                HttpResponseMessage Res = await client.GetAsync("api/recipes/" + urlId);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details received from the web api
                    var HResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response received from web api and storing into the Recipe list
                    rp = JsonConvert.DeserializeObject<Recipe>(HResponse);
                }

            }

            if (rp == null)
            {
                return NotFound();
            }

            return View(rp);
        }

    }
}
