using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class ScolarshipViewModel : BaseViewModel
    {
        public Command LoadTransactionsCommand { get; }
        public ObservableCollection<ScolarshipModel> Appointments { get; }
        public ScolarshipViewModel()
        {
            Title = "Pregled školarina";
        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
