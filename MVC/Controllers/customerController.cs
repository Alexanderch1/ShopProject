using MVC.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Data.Entities;
using MVC.ViewModels.Customer;

namespace MVC.Controllers
{
    public class customerController : Controller
    {
        private readonly Uri url = new Uri("https://localhost:44386/api/customer");

        public async Task<ActionResult> Index(IndexVM model, int page = 1, int itemsPerPage = 5)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");

                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<customerVM>>(jsonString);

                model.Pager = model.Pager ?? new pagerVM();
                model.Pager.Page = page <= 0 ? 1 : page;

                model.Filter = model.Filter ?? new FilterVM();

                var filteredData = responseData.Where(u => string.IsNullOrEmpty(model.Filter.FirstName) || u.FirstName.Contains(model.Filter.FirstName));

                model.Pager.ItemsPerPage = itemsPerPage;
                model.Pager.PagesCount = (int)Math.Ceiling((double)filteredData.Count() / model.Pager.ItemsPerPage);

                model.Items = filteredData
                    .OrderBy(i => i.Id)
                    .Skip(model.Pager.ItemsPerPage * (model.Pager.Page - 1))
                    .Take(model.Pager.ItemsPerPage)
                    .ToList();

             
                ViewBag.ItemsPerPage = itemsPerPage;

                return View(model);
            }
        }



        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("customer/" + id);

                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<customerVM>(jsonString);
                return View(responseData);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(customerVM customerVM)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(customerVM);
                    var buffer = Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await client.PostAsync("", byteContent);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index"); 
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("customer/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<customerVM>(jsonString);
                    return View(responseData);
                }
                else
                {
                    return View("Error");
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(customerVM customerVM)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(JsonConvert.SerializeObject(customerVM), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync("customer/edit/" + customerVM.Id, content);

                    response.EnsureSuccessStatusCode();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync($"customer/{id}");

                return RedirectToAction("Index");
            }
        }
    }
  }
