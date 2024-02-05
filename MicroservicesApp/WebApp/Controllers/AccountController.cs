using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        IConfiguration _config;
        HttpClient _httpClient;

        public AccountController(IConfiguration config)
        {
            _httpClient = new HttpClient();
            _config = config;
            _httpClient.BaseAddress = new Uri(_config["ApiBaseAddress"]);
           
        }
        public IActionResult Login( )
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var response = _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "auth/login", model).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var user = JsonSerializer.Deserialize<UserViewModel>(result);
                if (user.Roles.Contains("User"))
                    return RedirectToAction("Index", "Home", new { area = "User" });
            }
            return View();
        }
    }
}
