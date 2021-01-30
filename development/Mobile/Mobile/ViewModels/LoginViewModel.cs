using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Mobile.Models;
using Mobile.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command CancelAction { get; }

        private bool _toggle = false;
        public bool Toggle
        {
            get => _toggle;
            set => SetProperty(ref _toggle, value);
        }

        private string _name = string.Empty;
        public string Username
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public LoginViewModel()
        {
            Title = "Prijava";
            LoginCommand = new Command(OnLoginClicked);
            CancelAction = new Command(OnCancelClicked);
        }

        private async void OnCancelClicked()
        {
            Username = string.Empty;
            Password = string.Empty;
            Toggle = false;
            await Shell.Current.GoToAsync($"//{nameof(StartPage)}");
        }

        private async void OnLoginClicked(object obj)
        {
            Student = false;
            object userInfos = new { username = _name, password = _password };
            var jsonObj = JsonConvert.SerializeObject(userInfos);
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
            };
            using (HttpClient client = new HttpClient(httpHandler))
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://10.0.2.2:5001/api/User/Login"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                    
                var response = await client.SendAsync(request);
                var dataResult = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<LoginModel>(dataResult);
                    Application.Current.Properties["token"] = result.Token;

                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                    
                    Username = string.Empty;
                    Password = string.Empty;
                    Toggle = false;
                }
                else
                {
                    Username = string.Empty;
                    Password = string.Empty;
                    Toggle = true;
                }   
            }
        }
    }
}
