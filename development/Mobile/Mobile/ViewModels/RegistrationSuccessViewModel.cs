using System;
using System.Collections.Generic;
using System.Text;
using Mobile.Views;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    class RegistrationSuccessViewModel : BaseViewModel
    {
        public Command BackCommand { get; }

        public RegistrationSuccessViewModel()
        {
            BackCommand = new Command(OnBackClicked);
        }

        private async void OnBackClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(StartPage)}");
        }
    }    
}
