using AutoMapper;
using HR.LeaveManagment.MVC.Contracts;
using HR.LeaveManagment.MVC.Models;
using HR.LeaveManagment.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IAuthenticationService = HR.LeaveManagment.MVC.Contracts.IAuthenticationService;

namespace HR.LeaveManagment.MVC.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private JwtSecurityTokenHandler _tokenHandler;
        private IMapper _mapper;
        public AuthenticationService(IClient client, ILocalStorageServices localStorageServices,IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(client, localStorageServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenHandler = new JwtSecurityTokenHandler();
            _mapper = mapper;
        }


        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthRequest authRequest = new () { Email = email, Password = password };
                var authresponse = await _client.LoginAsync(authRequest);
                if(authresponse.Token != string.Empty)
                {
                    var tokenContent = _tokenHandler.ReadJwtToken(authresponse.Token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme));
                    var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,user);
                    _localStorage.SetStorageValue("token",authresponse.Token);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }

        public async Task logout()
        {
            _localStorage.ClearStorage(new List<string> { "Token" });
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> Register(RegisterVM register)
        {
            RegistrationRequest registrationRequest = _mapper.Map<RegistrationRequest>(register);
            var response = await _client.RegisterAsync(registrationRequest);
            if(!string.IsNullOrEmpty(response.UserId))
            {
                await Authenticate(register.Email, register.Password);
                return true;
            }
            return false;
        }
    }
}
