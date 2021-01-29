using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Mobile.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    [QueryProperty(nameof(TransactionDate), nameof(TransactionDate))]
    public class TransactionDetailViewModel : BaseViewModel
    {
        private string transactionDate;
        private TransactionModel transaction;
        private Guid id;
        private DateTime date;
        private string membershipName;
        private decimal price;
        private bool isPaid;

        public Guid Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public string Name 
        {
            get => membershipName;
            set => SetProperty(ref membershipName, value);
        }

        public decimal Price 
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public bool Paid 
        {
            get => isPaid;
            set => SetProperty(ref isPaid, value);
        }

        public TransactionModel Transaction
        {
            get => transaction;
            set => SetProperty(ref transaction, value);
        }
        
        public string TransactionDate
        {
            get
            {
                return transactionDate;
            }
            set
            {
                transactionDate = value;
                LoadTransaction(value);
            }
        }
        
        public async void LoadTransaction(string transactionDate)
        {
            try
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
                        RequestUri = new Uri("https://10.0.2.2:5001/api/Transactions/Student/" + (string)Application.Current.Properties["id"] + "/Transaction?date=" + transactionDate),
                        Method = HttpMethod.Get
                    };

                    var response = await client.SendAsync(request);
                    var dataResult = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<TransactionModel>(dataResult);
                        Transaction = result;
                        Id = result.Id;
                        Date = result.Date;
                        Name = result.Membership.Type.Name;
                        Price = result.Membership.Type.Price;
                        Paid = result.Paid;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
