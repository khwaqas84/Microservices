using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SupportApp.Models;
using System.Security.Claims;
using System.Text.Json;


namespace SupportApp.Controllers
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
        public IActionResult Login()
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
                GenerateTicket(user);
                if (user.Roles.Contains("Admin"))
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View();
        }

        async void GenerateTicket(UserViewModel user)
        {
            string strData=JsonSerializer.Serialize(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.UserData, strData),
                new  Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, string.Join(",",user.Roles))
            };
            var identity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
                    ,new ClaimsPrincipal(identity)
                    ,new AuthenticationProperties 
                    { 
                     AllowRefresh = true,
                     ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
                    });
        }
    }
}
