using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Mobile.Views;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    class NotStudentViewModel : BaseViewModel
    {
        public Command BackCommand { get; }

        public NotStudentViewModel()
        {
            BackCommand = new Command(OnBackClicked);
        }

        private async void OnBackClicked(object obj)
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
            };
            using (HttpClient client = new HttpClient(httpHandler))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)Application.Current.Properties["token"]);

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://10.0.2.2:5001/api/User/Logout"),
                    Method = HttpMethod.Post
                };

                var response = await client.SendAsync(request);
                var dataResult = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Application.Current.Properties["token"] = string.Empty;
                    Application.Current.Properties["id"] = string.Empty;
                    await Shell.Current.GoToAsync("//StartPage");
                }
            }
        }
    }
}
