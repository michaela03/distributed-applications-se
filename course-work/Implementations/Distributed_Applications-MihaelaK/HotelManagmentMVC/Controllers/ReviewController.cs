using HotelManagmentMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelManagmentMVC.Controllers
{
    public class ReviewController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44341/api");
        private readonly HttpClient _client;


        public ReviewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ReviewViewModel> reviewList = new List<ReviewViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Review").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                reviewList = JsonConvert.DeserializeObject<List<ReviewViewModel>>(data);
            }

            return View(reviewList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ReviewViewModel review)
        {
            try
            {
                string data = JsonConvert.SerializeObject(review);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Review", content).Result;

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
            ReviewViewModel review = new ReviewViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Review/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                review = JsonConvert.DeserializeObject<ReviewViewModel>(data);
            }

            return View(review);
        }

        [HttpPost]
        public IActionResult Edit(ReviewViewModel review)
        {
            try
            {
                string data = JsonConvert.SerializeObject(review);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Review/" + review.ReviewID, content).Result;

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
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Review/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Delete(ReviewViewModel review)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Review/" + review.ReviewID).Result;

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

        public IActionResult Details(int id)
        {
            ReviewViewModel review = new ReviewViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Review/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                review = JsonConvert.DeserializeObject<ReviewViewModel>(data);
            }

            return View(review);
        }
    }
}
