using BankApp.Domain.IdentityModels;
using BankApp.Enteties.DataTransferObjects.IdentityDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankApp.Frontend.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly string _baseUrl = "http://localhost:5000/api/auth/";

        public async Task<IActionResult> Login(LoginDTO login)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responce = await client.PostAsJsonAsync("login", login);

                if (responce.IsSuccessStatusCode)
                {
                    var res = await responce.Content.ReadAsStringAsync();
                    string jwtString = JsonConvert.DeserializeObject<dynamic>(res).token;
                    JwtSecurityTokenHandler handler = new();
                    var jwtToken = handler.ReadJwtToken(jwtString);
                    var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

                    HttpContext.Session.SetString("jwt", jwtString);
                    var id = Int32.Parse(jwtToken.Claims.FirstOrDefault(i => i.Type == "customerid").Value);

                    HttpContext.Session.SetInt32("id", id);
                }
            };

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("jwt");
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Register(RegristrationDTO reg)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("jwt"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responce = await client.PostAsJsonAsync("register", reg);

                if (!responce.IsSuccessStatusCode)
                {
                    ViewBag.error = "Server error try again";
                }
            }
            return RedirectToAction("Index", "Admin");
        }
    }
}