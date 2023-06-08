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
using System.Security.Policy;
using Data.Entities;
using System.Text;
using MVC.ViewModels.Product;

namespace MVC.Controllers
{
    public class productController : Controller
    {
           
        private readonly Uri url = new Uri("https://localhost:44386/api/product");

        public async Task<ActionResult> Index(IndexVM model, int page = 1, int itemsPerPage = 5)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");

                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<ProductVM>>(jsonString);

                model.Pager = model.Pager ?? new pagerVM();
                model.Pager.Page = page <= 0 ? 1 : page;

                model.Filter = model.Filter ?? new FilterVM();

                var filteredData = responseData.Where(p =>
                    string.IsNullOrEmpty(model.Filter.Category) || p.Category.Contains(model.Filter.Category));

                model.Pager.ItemsPerPage = itemsPerPage;
                model.Pager.PagesCount = (int)Math.Ceiling((double)filteredData.Count() / model.Pager.ItemsPerPage);

                model.Items = filteredData
                    .OrderBy(p => p.Id)
                    .Skip(model.Pager.ItemsPerPage * (model.Pager.Page - 1))
                    .Take(model.Pager.ItemsPerPage)
                    .ToList();

                ViewBag.ItemsPerPage = itemsPerPage; // Pass the selected itemsPerPage value to the view

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


                HttpResponseMessage response = await client.GetAsync("product/" + id);


                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ProductVM>(jsonString);
                return View(responseData);
                 }
            }

            public ActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public async Task<ActionResult> Create(ProductVM productVM)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                    client.BaseAddress = url;
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var content = JsonConvert.SerializeObject(productVM);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        
                        HttpResponseMessage response = await client.PostAsync("", byteContent);
                    }

                    return RedirectToAction("Index");
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

                // Make the request
                HttpResponseMessage response = await client.GetAsync("product/" + id);

                // Parse the response and return data
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<ProductVM>(jsonString);
                    return View(responseData);
                }
                else
                {
                    // Handle the error response
                    return View("Error");
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProductVM productVM)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(JsonConvert.SerializeObject(productVM), Encoding.UTF8, "application/json");

                    // Make the request
                    HttpResponseMessage response = await client.PutAsync($"product/Edit/{productVM.Id}", content);

                    // Handle the response as needed
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

                    HttpResponseMessage response = await client.DeleteAsync($"product/delete/{id}");

                    return RedirectToAction("Index");
                }
            }
        }
    }
