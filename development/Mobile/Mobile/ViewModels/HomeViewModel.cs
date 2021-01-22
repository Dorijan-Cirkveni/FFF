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
        private AppointmentModel _selectedAppointment;
        public Command LoadAppointmentsCommand { get; }
        public ObservableCollection<AppointmentModel> Appointments { get; }
        public Command<AppointmentModel> AppointmentTapped { get; }

        public HomeViewModel()
        {
            Title = "Naslovnica";
            Appointments = new ObservableCollection<AppointmentModel>();
            LoadAppointmentsCommand = new Command(async () => await ExecuteLoadAppointmentsCommand());
            AppointmentTapped = new Command<AppointmentModel>(OnAppointmentSelected);
        }

        public void OnAppearing()
        {
            IsBusy = true;
            _selectedAppointment = null;
        }

        //internal async void GetData() {}

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
                        foreach (var appointment in result)
                        {
                            Appointments.Add(appointment);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnAppointmentSelected(AppointmentModel appointment)
        {
            if (appointment == null)
                return;

            // This will push the AppointmentDetailPage onto the navigation stack
            // await Shell.Current.GoToAsync($"{nameof(AppointmentDetailPage)}?{nameof(AppointmentDetailViewModel.AppointmentId)}={appointment.Id}");
        }
    }
}
