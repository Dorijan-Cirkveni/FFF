using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            CurrentItem = Start;
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            Application.Current.Properties["token"] = string.Empty;
            await Shell.Current.GoToAsync("//StartPage");
        }
    }
}