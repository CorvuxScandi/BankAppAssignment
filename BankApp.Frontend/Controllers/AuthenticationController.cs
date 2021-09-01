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
using System.Threading.Tasks;

namespace BankApp.Frontend.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly string _baseUrl = "https://localhost:5000/api/authentication/";

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
                    var jwtString = await responce.Content.ReadAsStringAsync();
                    JwtSecurityToken jwt = JsonConvert.DeserializeObject<JwtSecurityToken>(jwtString);
                    var role = jwt.Claims.FirstOrDefault(c => c.Type == "Role").Value;

                    HttpContext.Session.SetString("jwt", jwtString);

                    if (role == UserRoles.Admin)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    if (role == UserRoles.User)
                    {
                        return RedirectToAction("CustomerView", "Home");
                    };
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