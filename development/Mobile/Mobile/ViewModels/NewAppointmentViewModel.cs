using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class NewAppointmentViewModel : BaseViewModel
    {
        private DateTime _date;
        private TimeSpan _time;
        private DateTime _minimumDate;

        public NewAppointmentViewModel()
        {
            AddAppointment = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            MinDate = DateTime.Today.AddDays(1);
            this.PropertyChanged += (_, __) => AddAppointment.ChangeCanExecute();
        }

        public DateTime MinDate
        {
            get => _minimumDate;
            set => SetProperty(ref _minimumDate, value);
        }

        public DateTime SelectedDate
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public TimeSpan SelectedTime
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        public Command AddAppointment { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            DateTime result = _date + _time;
            object userInfos = new { dateTimeStart = result };
            var jsonObj = JsonConvert.SerializeObject(userInfos);
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
            };
            using (HttpClient client = new HttpClient(httpHandler))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)Application.Current.Properties["token"]);

                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://10.0.2.2:5001/api/AppointmentRequests"),
                    Method = HttpMethod.Post,
                    Content = content
                };

                var response = await client.SendAsync(request);
                var dataResult = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.DisplayAlert("Uspjeh!", "Zahtjev za novim terminom poslan je profesoru.", "OK");
                    await Shell.Current.GoToAsync("..");
                } 
                else
                {
                    await Shell.Current.DisplayAlert("Ups!", "Nešto je pošlo po zlu.", "OK");
                    await Shell.Current.GoToAsync("..");
                }

            }                     
        }
    }
}
