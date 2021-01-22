using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScolarshipPage : ContentPage
    {
        private ScolarshipViewModel _scolarship;
        public ScolarshipPage()
        {
            InitializeComponent();
            BindingContext = _scolarship = new ScolarshipViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _scolarship.OnAppearing();
        }
    }
}