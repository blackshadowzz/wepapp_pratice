using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebAppWeek01.Models;
using System.Net.Http.Json;//Use Json

namespace WebAppWeek01.Controllers
{
    public class PeopleController : Controller
    {
        Uri baseAddress = new Uri("http://blackshadowz-001-site1.dtempurl.com/api");
        HttpClient client;
        public PeopleController()
        {
            client= new HttpClient();   
            client.BaseAddress = baseAddress;   
        }
        public IActionResult Index()
        {
            List<PeopleModel> people = new List<PeopleModel>();
            HttpResponseMessage plist = client.GetAsync(client.BaseAddress + "/People").Result;
            if (plist.IsSuccessStatusCode)
            {
                string data=plist.Content.ReadAsStringAsync().Result;
                people = JsonConvert.DeserializeObject<List<PeopleModel>>(data);
            }
            return View(people);
        } 
        public IActionResult Index2()
        {
          
            return View();
        }

        public IActionResult GetById(int id)
        {

            PeopleModel people = new PeopleModel();

            HttpResponseMessage plist = client.GetAsync(client.BaseAddress + "/People/"+id).Result;

            if (plist.IsSuccessStatusCode)
            {
                string data = plist.Content.ReadAsStringAsync().Result;
                people = JsonConvert.DeserializeObject<PeopleModel>(data);

            }
            
            return View(people);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(PeopleModel people)
        {
            string data=JsonConvert.SerializeObject(people);
            StringContent content= new StringContent(data,Encoding.UTF8,"application/json");

            HttpResponseMessage res = client.PostAsync(client.BaseAddress + "/people", content).Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
   
        public async Task<ActionResult> Edit(int id)
        {
            //return View(new PeopleModel { Id = id});
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            PeopleModel people = new PeopleModel();
            HttpResponseMessage plist = client.GetAsync(client.BaseAddress + $"/people/id?id={id}").Result;

            var result = await client.GetFromJsonAsync<PeopleModel>($"{client.BaseAddress}/people/{id}");
            
            
            if (plist.IsSuccessStatusCode)
            {
                var data = plist.Content.ReadAsStringAsync().Result;
                people = JsonConvert.DeserializeObject<PeopleModel>(data);
            }
            //return Json(people);
            return View(result);
        }
        [HttpPost]
        public IActionResult Edit(PeopleModel p)
        {
            string data = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage res = client.PutAsync(client.BaseAddress + "/people/"+p.Id, content).Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(p);
        } 
        [HttpGet]
        public IActionResult Delete(int id)
        {
            string data = JsonConvert.SerializeObject(id);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage res = client.DeleteAsync($"{client.BaseAddress}/people/{id}").Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return NotFound(id);
        }
    }
}
