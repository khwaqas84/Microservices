using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IConfiguration _config;
        HttpClient _httpClient;

        

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _config = config;
            _httpClient.BaseAddress = new Uri(_config["ApiBaseAddress"]);
        }

        public IActionResult Index()
        {
            IEnumerable<ProductViewModels> products = null;
            var response=_httpClient.GetAsync(_httpClient.BaseAddress+"catalog/getproducts").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                products = JsonSerializer.Deserialize<IEnumerable<ProductViewModels>>(result);
                
            }
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
