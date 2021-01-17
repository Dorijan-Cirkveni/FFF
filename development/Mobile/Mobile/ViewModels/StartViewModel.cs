using System;
using System.Collections.Generic;
using System.Text;
using Mobile.Views;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class StartViewModel
    {
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }

        public StartViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new Command(OnRegisterClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private async void OnRegisterClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }
    }
}
