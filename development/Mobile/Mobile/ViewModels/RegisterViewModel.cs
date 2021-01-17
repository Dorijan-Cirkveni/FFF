using System;
using System.Collections.Generic;
using System.Text;
using Mobile.Views;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public Command RegisterCommand { get; }

        public RegisterViewModel()
        {
            Title = "Registracija";
            RegisterCommand = new Command(OnRegisterClicked);
        }

        private async void OnRegisterClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(StartPage)}");
        }
    }
}
