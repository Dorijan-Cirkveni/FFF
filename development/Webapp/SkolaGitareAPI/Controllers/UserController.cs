using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SkolaGitareAPI.Data.Entities;
using SkolaGitareAPI.Data.Repositories.Interfaces;
using SkolaGitareAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using SkolaGitareAPI.Utilities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace SkolaGitareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;
        private readonly ILogger<UserController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMembershipTypeRepository _membershipType;
        private readonly IMembershipRepository _membership;
        private readonly IConfiguration _configuration;

        public UserController(SignInManager<Person> signInManager,
            ILogger<UserController> logger,
            UserManager<Person> userManager,
            RoleManager<IdentityRole> roleManager,
            IMembershipTypeRepository membershipType,
            IMembershipRepository membership,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _membershipType = membershipType;
            _membership = membership;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginPost(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var user = await _userManager.FindByEmailAsync(model.Username);
                if (user == null)
                {
                    return BadRequest(new { code = -1, errorMessage = "Invalid login attempt." });
                }
                if (user.Verified == false)
                {
                    return BadRequest(new { code = -2, errorMessage = "User is not verified" });
                }
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    var roles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                    foreach (var userRole in roles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });

                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction("Login");
                }
                else
                {
                    _logger.LogError("Invalid login attempt");
                    return BadRequest(new { code = -1, errorMessage = "Invalid login attempt." });
                }
            }

            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterPost(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Person user = null;

                if (model.Role.ToLower() == "student")
                {
                    var membershipType = await _membershipType.GetByName(model.Tariff);
                    user = new Person { UserName = model.Email, Email = model.Email, Name = model.FirstName, Surname = model.LastName, PhoneNumber = model.Phone };

                }
                else
                {
                    BadRequest();
                }

                if (await _roleManager.FindByNameAsync(model.Role) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = model.Role });
                }


                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    if (model.Role.ToLower() == "student")
                    {
                        await _membership.Create(new Membership { Id = new Guid(), Member = user, Type = await _membershipType.GetByName(model.Tariff) });
                    }

                    _logger.LogInformation("User created a new account with password.");

                    var currentUser = await _userManager.FindByEmailAsync(user.Email);

                    var roleResult = await _userManager.AddToRoleAsync(currentUser, model.Role);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Action(new Microsoft.AspNetCore.Mvc.Routing.UrlActionContext
                    {
                        Action = "ConfirmEmail",
                        Controller = "User",
                        Protocol = Request.Scheme,
                        Values = new { userId = user.Id, code = code }
                    });

                    await MailSender.SendMail(model.Email, "Confirm your email",
                        $"Please confirm your account by <a href = '{HtmlEncoder.Default.Encode(callbackUrl)}' > clicking here </a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return Ok();
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return Ok();
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return BadRequest();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordPost(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return Ok();
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = Url.Action(new Microsoft.AspNetCore.Mvc.Routing.UrlActionContext
                {
                    Action = "ResetPasswordPost",
                    Controller = "User",
                    Protocol = Request.Scheme,
                    Values = new { code = code }
                });

                await MailSender.SendMail(model.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordPost(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = -1, errorMessage = "Invalid model" });
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Ok();
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(new { code = model.Code, errorMessage = "" });
        }

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {

            if (userId == null || code == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Code and email should be supplied for confirmation" });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { code = -2, errorMessage = "Unable to confirm email" });
            }
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutPost()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Ok();
        }

        [HttpPost("EditInformation")]
        [Produces("application/json")]
        public async Task<IActionResult> EditInformationPost(EditInformationModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            return Ok();
        }

        [HttpPost("ChangePassword")]
        [Produces("application/json")]
        public async Task<IActionResult> ChangePasswordPost(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Redirect("./");
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");

            return Ok();
        }



        [HttpPost("ConfirmUser")]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> ConfirmUser(RegistrationRequestsModel model)
        {
            if (model == null)
            {
                return BadRequest(new { code = -1, errorMessage = "Please supply parameters" });
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                return Redirect("");
            }

            if (model.Decision == true)
            {
                user.Verified = true;
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest(new { code = 2, errorMessage = "Unable to update user" });
                }

                return Ok();
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest(new { code = 3, errorMessage = "Unable to delete user" });
                }

                return Ok();
            }

        }

        [HttpPost("DownloadPersonalData")]
        public async Task<IActionResult> DownloadPersonalDataPost()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(User));

            // Only include personal data for download
            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(IdentityUser).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(personalData)), "text/json");
        }

        //Used for testing ajax request to api (swal)
        [HttpGet("Ok")]
        [Produces("application/json")]
        public IActionResult OkMethodForTesting()
        {
            return Ok();
        }

        [HttpPost("DeletePersonalData")]
        public async Task<IActionResult> DeletePersonalDataPost(DeletePersonalDataModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                ModelState.AddModelError(string.Empty, "Incorrect password.");
                return BadRequest(new { code = -1, errorMessage = "Incorrect password" });
            }

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Ok();
        }
    }
}
