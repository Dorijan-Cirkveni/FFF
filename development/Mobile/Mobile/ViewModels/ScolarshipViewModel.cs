using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Mobile.Models;
using Mobile.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class ScolarshipViewModel : BaseViewModel
    {
        private TransactionModel _selectedTransaction;
        public Command LoadTransactionsCommand { get; }
        public ObservableCollection<TransactionModel> Transactions { get; }
        public Command<TransactionModel> TransactionTapped { get; }
        public ScolarshipViewModel()
        {
            Title = "Pregled školarina";
            Transactions = new ObservableCollection<TransactionModel>();
            LoadTransactionsCommand = new Command(async () => await ExecuteLoadTransactionsCommand());
            TransactionTapped = new Command<TransactionModel>(OnTransactionSelected);
        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }

        async Task ExecuteLoadTransactionsCommand()
        {
            IsBusy = true;

            try
            {
                Transactions.Clear();

                var httpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
                };
                using (HttpClient client = new HttpClient(httpHandler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)Application.Current.Properties["token"]);

                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri("https://10.0.2.2:5001/api/Transactions/Student/" + (string)Application.Current.Properties["id"]),
                        Method = HttpMethod.Get
                    };

                    var response = await client.SendAsync(request);
                    var dataResult = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<List<TransactionModel>>(dataResult);
                        result.Sort((x, y) =>
                        {
                            if (x.Date == null && y.Date == null) return 0;
                            else if (x.Date == null) return -1;
                            else if (y.Date == null) return 1;
                            else return y.Date.CompareTo(x.Date);
                        });

                        foreach (var transaction in result)
                        {
                            Transactions.Add(transaction);
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

        public TransactionModel SelectedTransaction
        {
            get => _selectedTransaction;
            set
            {
                SetProperty(ref _selectedTransaction, value);
                OnTransactionSelected(value);
            }
        }

        async void OnTransactionSelected(TransactionModel transaction)
        {
            if (transaction == null)
                return;

            string tDate = DateTime.ParseExact(transaction.Date.ToString(), "dd.M.yyyy. H:mm:ss", CultureInfo.GetCultureInfo("hr-HR")).ToString("yyyy-MM-ddTHH:mm:ss");
            await Shell.Current.GoToAsync($"{nameof(TransactionDetailPage)}?{nameof(TransactionDetailViewModel.TransactionDate)}={tDate}");
        }
    }
}
