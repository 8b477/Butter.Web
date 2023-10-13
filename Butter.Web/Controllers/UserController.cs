using Butter.Web.Models;

using Microsoft.AspNetCore.Mvc;

using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Butter.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("GetAllUser");
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            string apiUrl = "https://localhost:7141/api/";

            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(apiUrl);

                HttpResponseMessage responce = httpClient.GetAsync("User").Result;

                if (responce.IsSuccessStatusCode)
                {
                    string json = responce.Content.ReadAsStringAsync().Result;
                    List<UserModelForm>? data = JsonSerializer.Deserialize<List<UserModelForm>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (data is not null)
                    {

                        UserList viewModel = new UserList
                        {
                            Users = data
                        };

                        return View(viewModel);

                    }
                }
            }
            return View(); // redirect vers une erreur
        }


        [HttpGet]
        public IActionResult GetUserById(int userId)
        {

            string apiUrl = "https://localhost:7141/api/";

            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(apiUrl);

                HttpResponseMessage responce = httpClient.GetAsync($"User/{userId}").Result;

                if (responce.IsSuccessStatusCode)
                {
                    string json = responce.Content.ReadAsStringAsync().Result;
                    UserModelForm? data = JsonSerializer.Deserialize<UserModelForm>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (data is not null)
                    {

                        return View(data);

                    }
                }
            }

            return View(); // => redirect vers une erreur
        }


        [HttpGet]
        public IActionResult DeleteUser(int userId)
        {
            string apiUrl = "https://localhost:7141/api/";

            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(apiUrl);

                HttpResponseMessage responce = httpClient.DeleteAsync($"User/{userId}").Result;

                if (responce.IsSuccessStatusCode)
                {
                        return RedirectToAction("GetAllUser");                
                }

                return RedirectToAction("Index");
            }
        }

        
        public IActionResult CreateUser(UserModelCreateUserForm user)
        {
            if(ModelState.IsValid)
            {
                string apiUrl = "https://localhost:7141/api/";

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(apiUrl);

                    
                    string json = JsonSerializer.Serialize(user);

                    
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    
                    HttpResponseMessage response = httpClient.PostAsync("User", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        
                        return RedirectToAction("GetAllUser");
                    }
                    else
                    {
                        return View(user);
                    }
                }
            }
                return View(user);
        }



        public IActionResult UpdateUser(int userId)
        {

            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7141/api/");

                HttpResponseMessage responce = httpClient.GetAsync($"User/{userId}").Result;

                if (responce.IsSuccessStatusCode)
                {
                    string json = responce.Content.ReadAsStringAsync().Result;
                    UserModelForm? data = JsonSerializer.Deserialize<UserModelForm>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (data is not null)
                    {

                        UserModelUpdateForm userModel = new()
                        {
                            Id = data.UserId,
                            NickName = data.NickName,
                            Email = data.Email,
                            BirthDate = data.BirthDate,
                            Password = data.Password,
                            Genre = data.Genre,
                            Town = data.Town
                        };

                        return View(userModel);
                    }
                }

            }
            return RedirectToAction(nameof(GetAllUser));

        }



        public IActionResult UpdateSaveUser(UserModelUpdateForm userUp)
        {

            if (ModelState.IsValid)
            {

            string apiUrl = "https://localhost:7141/api/User/Update";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(apiUrl);


                string json = JsonSerializer.Serialize(userUp);


                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");


                HttpResponseMessage response = httpClient.PatchAsync(apiUrl, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

        }

            return RedirectToAction(nameof(GetAllUser));
        }


    }
}