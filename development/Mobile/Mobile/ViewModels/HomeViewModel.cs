using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Mobile.Models;
using Mobile.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Command LoadAppointmentsCommand { get; }
        public Command AddAppointmentCommand { get; }
        public ObservableCollection<AppointmentModel> Appointments { get; }

        public HomeViewModel()
        {
            Title = "Naslovnica";
            Appointments = new ObservableCollection<AppointmentModel>();
            LoadAppointmentsCommand = new Command(async () => await ExecuteLoadAppointmentsCommand());
            AddAppointmentCommand = new Command(OnAddAppointment);
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        async Task ExecuteLoadAppointmentsCommand()
        {
            IsBusy = true;

            try
            {
                Appointments.Clear();

                var httpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
                };
                using (HttpClient client = new HttpClient(httpHandler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)Application.Current.Properties["token"]);

                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri("https://10.0.2.2:5001/api/Appointments/Student/" + (string)Application.Current.Properties["id"]),
                        Method = HttpMethod.Get
                    };

                    var response = await client.SendAsync(request);
                    var dataResult = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<List<AppointmentModel>>(dataResult);
                        result.Sort(delegate (AppointmentModel x, AppointmentModel y)
                        {
                            if (x.DateTimeStart == null && y.DateTimeStart == null) return 0;
                            else if (x.DateTimeStart == null) return -1;
                            else if (y.DateTimeStart == null) return 1;
                            else return x.DateTimeStart.CompareTo(y.DateTimeStart);
                        });

                        foreach (var appointment in result)
                        {
                            Appointments.Add(appointment);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnAddAppointment(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewAppointmentPage));
        }
    }
}
