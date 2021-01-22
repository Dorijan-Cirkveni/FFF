using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private HomeViewModel _home;

        public HomePage()
        {
            InitializeComponent();
            BindingContext = _home = new HomeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _home.OnAppearing();
            UserLoggedIn();
        }

        public async void UserLoggedIn()
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedvalue = handler.ReadJwtToken((string)Application.Current.Properties["token"]);
            var claims = decodedvalue.Claims;
            foreach (Claim c in claims)
            {
                if (c.Type.Equals(ClaimTypes.Role) && c.Value.ToLower().Equals("student"))
                {
                    _home.Student = true;
                }

                if (c.Type.Equals(ClaimTypes.Role) && (c.Value.ToLower().Equals("admin") || c.Value.ToLower().Equals("teacher")))
                {
                    await Shell.Current.GoToAsync("//NotStudentPage");
                    _home.Student = false;
                }

                if (c.Type.Equals(ClaimTypes.NameIdentifier))
                {
                    Application.Current.Properties["id"] = c.Value;
                }

                if (c.Type.Equals(ClaimTypes.Name))
                {
                    _home.HelloMessage = "Bok, " + c.Value + "!";
                }
            }
        }
    }
}