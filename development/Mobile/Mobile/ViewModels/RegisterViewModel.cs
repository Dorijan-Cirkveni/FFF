using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Mobile.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public Command RegisterCommand { get; }
        public Command CancelAction { get; }

        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;
        private bool _toggle = false;
        private bool _toggle2 = false;

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public bool Toggle
        {
            get => _toggle;
            set => SetProperty(ref _toggle, value);
        }

        public bool Toggle2
        {
            get => _toggle2;
            set => SetProperty(ref _toggle2, value);
        }

        public RegisterViewModel()
        {
            Title = "Registracija";
            RegisterCommand = new Command(OnRegisterClicked);
            CancelAction = new Command(OnCancelClicked);
        }

        private async void OnCancelClicked()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            Toggle = false;
            Toggle2 = false;
            await Shell.Current.GoToAsync($"//{nameof(StartPage)}");
        }

        private async void OnRegisterClicked(object obj)
        {
            if (!_password.Equals(_confirmPassword))
            {
                Toggle = true;
                Password = string.Empty;
                ConfirmPassword = string.Empty;
                return;
            }

            object userInfos = new { firstname = _firstName, lastname = _lastName, email = _email, phone = _phoneNumber, 
                                     role = "student", password = _password, confirmpassword = _confirmPassword };
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
                    RequestUri = new Uri("https://10.0.2.2:5001/api/User/Register"),
                    Method = HttpMethod.Post,
                    Content = content
                };

                var response = await client.SendAsync(request);
                var dataResult = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.GoToAsync($"//{nameof(RegistrationSuccess)}");
                    FirstName = string.Empty;
                    LastName = string.Empty;
                    Email = string.Empty;
                    PhoneNumber = string.Empty;
                    Password = string.Empty;
                    ConfirmPassword = string.Empty;
                    Toggle = false;
                    Toggle2 = false;
                }
                else
                {
                    FirstName = string.Empty;
                    LastName = string.Empty;
                    Email = string.Empty;
                    PhoneNumber = string.Empty;
                    Password = string.Empty;
                    ConfirmPassword = string.Empty;
                    Toggle = false;
                    Toggle2 = true;
                }
            }
        }
    }
}
