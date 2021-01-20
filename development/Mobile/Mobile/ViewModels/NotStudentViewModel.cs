using System;
using System.Collections.Generic;
using System.Text;
using Mobile.Views;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    class NotStudentViewModel : BaseViewModel
    {
        public Command BackCommand { get; }

        public NotStudentViewModel()
        {
            BackCommand = new Command(OnBackClicked);
        }

        private async void OnBackClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(StartPage)}");
        }
    }
}
