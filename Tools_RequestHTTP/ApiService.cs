using System.Text.Json;
using System.Text;
using Butter.Web.Models;

namespace Tools_RequestHTTP
{
    public class ApiService
    {


        private readonly HttpClient _httpClient;

        public ApiService(string baseApiUrl)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseApiUrl);
        }



        public async Task<List<UserModelForm>?> GetAllUsersAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("User");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();


                if (json is not null)
                {
                    return JsonSerializer.Deserialize<List<UserModelForm>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            return null;
        }



        public async Task<UserModelForm?> GetUserByIdAsync(int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"User/{userId}");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                if (json is not null)
                {
                    return JsonSerializer.Deserialize<UserModelForm>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            return null;
        }



        public async Task<bool> DeleteUserAsync(int userId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"User/{userId}");
            return response.IsSuccessStatusCode;
        }



        public async Task<bool> CreateUserAsync(UserModelCreateUserForm user)
        {
            string json = JsonSerializer.Serialize(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("User", content);
            return response.IsSuccessStatusCode;
        }



        public async Task<UserModelUpdateForm?> GetUserForUpdateAsync(int userId)
        {
            UserModelForm? data = await GetUserByIdAsync(userId);
            if (data != null)
            {
                return new UserModelUpdateForm
                {
                    Id = data.UserId,
                    NickName = data.NickName,
                    Email = data.Email,
                    BirthDate = data.BirthDate,
                    Password = data.Password,
                    Genre = data.Genre,
                    Town = data.Town
                };
            }
            return null;
        }



        public async Task<bool> UpdateUserAsync(UserModelUpdateForm userUp)
        {
            string apiUrl = $"User/Update";
            string json = JsonSerializer.Serialize(userUp);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PatchAsync(apiUrl, content);
            return response.IsSuccessStatusCode;
        }
    }
}