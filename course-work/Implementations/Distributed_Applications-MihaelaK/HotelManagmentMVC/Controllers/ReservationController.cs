using HotelManagmentMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelManagmentMVC.Controllers
{
    public class ReservationController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44341/api");
        private readonly HttpClient _client;
  


        public ReservationController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ReservationViewModel> reservationList = new List<ReservationViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Reservation").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                reservationList = JsonConvert.DeserializeObject<List<ReservationViewModel>>(data);
            }

            return View(reservationList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ReservationViewModel reservation, ClientViewModel client, RoomViewModel room)
        {
            try
            {
                
                reservation.ClientID = client.ClientID;
                reservation.RoomID = room.RoomID;

                string data = JsonConvert.SerializeObject(reservation);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Reservation", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return View();
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            ReservationViewModel reservation = new ReservationViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Reservation/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                reservation = JsonConvert.DeserializeObject<ReservationViewModel>(data);
            }

            return View(reservation);
        }

        [HttpPost]
        public IActionResult Edit(ReservationViewModel reservation)
        {
            try
            {
                string data = JsonConvert.SerializeObject(reservation);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Reservation/" + reservation.ReservationID, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Reservation/Delete" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            ReservationViewModel reservation = new ReservationViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Reservation/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                reservation = JsonConvert.DeserializeObject<ReservationViewModel>(data);
            }

            return View(reservation);
        }
    }
}
