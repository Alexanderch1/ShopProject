using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVC.ViewModels;
using Newtonsoft.Json;
using System.Text;
using ApplicationService.DTOs;
using MVC.ViewModels.Order;

namespace MVC.Controllers
{
    public class orderController : Controller
    {
        private string apiUrl = "https://localhost:44386/api/order";
        private readonly Uri url = new Uri("https://localhost:44386/api/order");

        [HttpGet]
        public async Task<ActionResult> Index(IndexVM model, int page = 1, int itemsPerPage = 5)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        var responseData = JsonConvert.DeserializeObject<List<orderDTO>>(jsonString);

                        model.Pager = model.Pager ?? new pagerVM();
                        model.Pager.Page = page <= 0 ? 1 : page;

                        model.Filter = model.Filter ?? new FilterVM();

                        var filteredData = responseData;

                        if (!string.IsNullOrEmpty(model.Filter.PaymentMethod))
                        {
                            filteredData = filteredData.Where(o => o.PaymentMethod == model.Filter.PaymentMethod).ToList();
                        }

                        model.Pager.ItemsPerPage = itemsPerPage;
                        model.Pager.PagesCount = (int)Math.Ceiling((double)filteredData.Count() / model.Pager.ItemsPerPage);

                        model.Items = filteredData
                            .OrderBy(o => o.Id)
                            .Skip(model.Pager.ItemsPerPage * (model.Pager.Page - 1))
                            .Take(model.Pager.ItemsPerPage)
                            .Select(dto => new orderVM(dto))
                            .ToList();

                        ViewBag.ItemsPerPage = itemsPerPage;

                        return View(model);
                    }
                    catch (Exception ex)
                    {
                        // Log or display the exception message for debugging purposes
                        ViewBag.ErrorMessage = $"Failed to deserialize JSON response: {ex.Message}";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to retrieve orders from the server.";
                }
            }

            return View(model);
        }







        public async Task<List<customerVM>> GetAllCustomers()
        {
            List<customerVM> customers = new List<customerVM>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44386/api/customer");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    customers = JsonConvert.DeserializeObject<List<customerVM>>(json);
                }
              
            }

            return customers;
        }

        private async Task<List<ProductVM>> GetAllProducts()
        {
            List<ProductVM> products = new List<ProductVM>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44386/api/product");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ProductVM>>(json);
                }
              
            }

            return products;
        }



        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"order/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var order = JsonConvert.DeserializeObject<orderVM>(json);
                    if (order != null)
                    {
                        return View(order);
                    }
                    else
                    {
                        Console.WriteLine("Received null order from the server.");
                    }
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve the order from the server. StatusCode: {response.StatusCode}");
                }
            }

            ViewBag.ErrorMessage = "Failed to retrieve the order from the server.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            orderVM order = new orderVM();

          
            order.Customers = await GetAllCustomers();

            
            order.Products = await GetAllProducts();

            return View(order);
        }



        [HttpPost]
        public async Task<ActionResult> Create(orderVM orderVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                       
                       
                        var content = JsonConvert.SerializeObject(orderVM);
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
                            ViewBag.ErrorMessage = "Failed to create the order.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred while creating the order: {ex.Message}";
                }

            }

            orderVM.Customers = await GetAllCustomers();
            orderVM.Products = await GetAllProducts();

            return View(orderVM);
        }




        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            orderVM order = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl + "/" + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    order = JsonConvert.DeserializeObject<orderVM>(json);
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to retrieve the order from the server.";
                }
            }

            order.Customers = await GetAllCustomers();
            order.Products = await GetAllProducts();

            return View(order);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, orderVM orderVM)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(JsonConvert.SerializeObject(orderVM), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(apiUrl + "/" + id.ToString(), content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to update the order.";
                    }
                }
            }

            orderVM.Customers = await GetAllCustomers();
            orderVM.Products = await GetAllProducts();

            return View(orderVM);
        }


        [HttpDelete]
      
        public async Task<ActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync(id.ToString());
                if (response.IsSuccessStatusCode)
                {
                  
                }
                else
                {
                    
                    ViewBag.ErrorMessage = "Failed to delete the order.";
                }
            }

            return RedirectToAction("Index");
        }
    }
}