﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewAppointmentPage), typeof(NewAppointmentPage));
            CurrentItem = Start;
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
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