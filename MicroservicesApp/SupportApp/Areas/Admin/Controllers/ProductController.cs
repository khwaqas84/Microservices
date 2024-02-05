using Microsoft.AspNetCore.Mvc;
using SupportApp.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SupportApp.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        IConfiguration _config;
        HttpClient _httpClient;

        public ProductController(IConfiguration config)
        {
            _httpClient = new HttpClient();
            _config = config;
            _httpClient.BaseAddress = new Uri(_config["ApiBaseAddress"]);

        }
        public IActionResult Index()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUser.Token);
            var response= _httpClient.GetAsync(_httpClient.BaseAddress+ "product/GetProducts").Result;
            if(response.IsSuccessStatusCode) 
            { 
                var result=response.Content.ReadAsStringAsync().Result;
                var products = JsonSerializer.Deserialize<List<ProductViewModels>>(result);
                return View(products);
            
            }
            return View();
        }
    }
}
