using Jobsity.Presentation.Web.Models;
using Jobsity.Presentation.Web.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobsity.Presentation.Web.Services
{
    public interface IAccountService
    {
        User User { get; }
        Task Initialize();
        Task Login(Login model);
        Task Logout();
        Task Register(AddUser model);
    }

    public class AccountService : IAccountService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _userKey = "user";

        public User User { get; private set; }

        public AccountService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        ) {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<User>(_userKey);
        }

        public async Task Login(Login model)
        {
            var response = await _httpService.Post<Response<SignInResponse>>("/api/User/Login", model);

            if (response.Errors != null && response.Errors.Any())
                throw new System.Exception(response.Errors.First());

            var data = response.Data;

            User = new User
            {
                UserName = model.UserName,
                Id = data.AuthResponse.UserToken.Id,
                Email = data.AuthResponse.UserToken.Email,
                Token = data.AuthResponse.AccessToken
            };

            await _localStorageService.SetItem(_userKey, User);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem(_userKey);
            _navigationManager.NavigateTo("account/login");
        }

        public async Task Register(AddUser model)
        {
            var response = await _httpService.Post<Response<object>>("/api/User/Register", model);

            if (response.Errors != null && response.Errors.Any())
                throw new System.Exception(response.Errors.First());
        }
    }
}