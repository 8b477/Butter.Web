using Butter.Web.Models;
using Microsoft.AspNetCore.Mvc;

using Tools_RequestHTTP;

namespace Butter.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly ApiService _apiService;

        public UserController(ApiService apiService)
        {
            _apiService = apiService;
        }


        public IActionResult Index()
        {
            return RedirectToAction("GetAllUser");
        }



        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            List<UserModelForm>? data = await _apiService.GetAllUsersAsync();
            if (data != null)
            {
                UserList viewModel = new UserList
                {
                    Users = data
                };
                return View(viewModel);
            }
            return View(); // Redirection vers une vue d'erreur
        }


        [HttpGet]
        public async Task<IActionResult> GetUserById(int userId)
        {
            UserModelForm? data = await _apiService.GetUserByIdAsync(userId);

                if (data != null)
                {
                    return View(data);
                }

            return View(); // Redirection vers une vue d'erreur
        }




        [HttpGet]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            bool isSuccess = await _apiService.DeleteUserAsync(userId);

                if (isSuccess)
                {
                    return RedirectToAction("GetAllUser");
                }

            return RedirectToAction("Index");
        }



        
        public async Task<IActionResult> CreateUser(UserModelCreateUserForm user)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _apiService.CreateUserAsync(user);

                    if (isSuccess)
                    {
                        return RedirectToAction("GetAllUser");
                    }
                    else
                    {
                        return View(user);
                    }
            }

            return View(user);
        }



        [HttpGet]
        public async Task<IActionResult> UpdateUser(int userId)
        {
            UserModelUpdateForm? userUp = await _apiService.GetUserForUpdateAsync(userId);

                if (userUp != null)
                {
                    return View(userUp);
                }

            return RedirectToAction(nameof(GetAllUser));
        }



        [HttpPost]
        public async Task<IActionResult> UpdateSaveUser(UserModelUpdateForm userUp)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _apiService.UpdateUserAsync(userUp);

                    if (isSuccess)
                    {
                        return RedirectToAction("Index");
                    }
            }

            return RedirectToAction(nameof(GetAllUser));
        }


    }
}