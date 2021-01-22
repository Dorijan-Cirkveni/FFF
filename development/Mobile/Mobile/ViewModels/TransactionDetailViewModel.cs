using System;
using System.Collections.Generic;
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
        private DateTime transactionDate;
        private TransactionModel transaction;

        public TransactionModel Transaction
        {
            get => transaction;
            set => SetProperty(ref transaction, value);
        }

        public DateTime TransactionDate
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

        public async void LoadTransaction(DateTime transactionDate)
        {
            try
            {
                object userInfos = new { date = transactionDate };
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
                        RequestUri = new Uri("https://10.0.2.2:5001/api/Appointments/Student/" + (string)Application.Current.Properties["id"] + "/Transaction"),
                        Method = HttpMethod.Post,
                        Content = content
                    };

                    var response = await client.SendAsync(request);
                    var dataResult = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<TransactionModel>(dataResult);
                        Transaction = result;
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
