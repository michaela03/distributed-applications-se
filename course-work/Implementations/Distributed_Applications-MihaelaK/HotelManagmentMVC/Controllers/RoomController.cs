using HotelManagmentMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelManagmentMVC.Controllers
{
    public class RoomController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44341/api");
        private readonly HttpClient _client;


        public RoomController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<RoomViewModel> reservationList = new List<RoomViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Room").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                reservationList = JsonConvert.DeserializeObject<List<RoomViewModel>>(data);
            }

            return View(reservationList);
        }
    }
}
