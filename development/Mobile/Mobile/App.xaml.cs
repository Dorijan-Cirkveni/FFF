using System;
using Xamarin.Forms;
using Mobile.Views;
using Xamarin.Forms.Xaml;
using System.Globalization;
using System.Threading;

namespace Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("hr-HR");
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
