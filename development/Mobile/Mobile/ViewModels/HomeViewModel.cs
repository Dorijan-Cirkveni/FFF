using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mobile.Views;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private bool _isStudent = false;

        public HomeViewModel()
        {
            Title = "Naslovnica";
            HelloMessage = string.Empty;

            var handler = new JwtSecurityTokenHandler();
            var decodedvalue = handler.ReadJwtToken((string)Application.Current.Properties["token"]);
            var claims = decodedvalue.Claims;
            foreach(Claim c in claims)
            {
                Console.WriteLine("\n\n" + c.Type + " " + c.Value + "\n\n");
                if (c.Type.Equals(ClaimTypes.Role) && c.Value.ToLower().Equals("student"))
                {
                    _isStudent = true;
                }

                if (c.Type.Equals(ClaimTypes.NameIdentifier))
                {
                    Application.Current.Properties["id"] = c.Value;
                }

                if (c.Type.Equals(ClaimTypes.Name))
                {
                    HelloMessage = "Bok, " + c.Value + "!";
                }
            }

            if (!_isStudent) NotStudent();
        }

        private async void NotStudent()
        {
            await Shell.Current.GoToAsync($"//{nameof(NotStudentPage)}");
        }
    }
}
