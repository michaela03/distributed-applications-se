using HotelManagmentMVC.Data;
using HotelManagmentMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace HotelManagmentMVC.Controllers
{

    
    public class ClientController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44341/api");
        private readonly HttpClient _client;

        public ClientController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
          
        }

        // GET: Clients
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            List<ClientViewModel> clientList = new List<ClientViewModel>();
            string requestUri = "https://localhost:44341/api/Client/GetClients";

            if (!string.IsNullOrEmpty(searchString))
            {
                requestUri = $"https://localhost:44341/api/Client/GetClientByName/clientName/{searchString}";
            }

            HttpResponseMessage response = await _client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();

                // Check if the data is a single object or an array
                if (data.Trim().StartsWith("["))
                {
                    clientList = JsonConvert.DeserializeObject<List<ClientViewModel>>(data);
                }
                else
                {
                    var singleClient = JsonConvert.DeserializeObject<ClientViewModel>(data);
                    if (singleClient != null)
                    {
                        clientList.Add(singleClient);
                    }
                }
            }
            else
            {
                TempData["Error"] = $"Error retrieving client list. Status code: {response.StatusCode}";
            }

            ViewData["SearchString"] = searchString;
            return View(clientList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClientViewModel client)
        {
            try 
            {
                string data = JsonConvert.SerializeObject(client);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Client/CreateClient", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(client);
            }
          
            return View(client);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                ClientViewModel client = new ClientViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Client/GetClient/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    client = JsonConvert.DeserializeObject<ClientViewModel>(data);
                }
                return View(client);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
           
        }

        [HttpPost]
        public IActionResult Edit(ClientViewModel client)
        {
            try
            {
                string data = JsonConvert.SerializeObject(client);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Client/UpdateClient/"+client.ClientID, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(client);
            }

            return View(client);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Client/DeleteClient/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                ClientViewModel client = new ClientViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Client/GetClient/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    client = JsonConvert.DeserializeObject<ClientViewModel>(data);
                }
                return View(client);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }
    }
}

