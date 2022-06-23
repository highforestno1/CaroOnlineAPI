using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CKLS.Identity.IdentitySettings;
using Lab01.Models;
using Lab01.Models.ViewModels;
using Lab01.RequestModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Lab01.Services.Impl
{
    public class UserServicesImpl : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;


        public UserServicesImpl(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<ApplicationRole> roleManager)
        {
            this._userManager = userManager;
            this._configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                throw new Exception("Username not exist");
            }
            
            var loginResponse = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!loginResponse)
            {
                throw new Exception("Username or Password incorrect");
            }
            
            var token = await GenerateTokenJWTByUser(user);

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
        }

        public async Task<bool> Registration(RegistrationUser request)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = request.Username,
                Address = request.Address,
                Name = request.Name,
                Email = "axzczxc@gmail.com"
            };

            var newUser = await _userManager.CreateAsync(user, request.Password);
            if (newUser.Succeeded)
            {
                return true;
            }

            return false;
        }

        private async Task<JwtSecurityToken> GenerateTokenJWTByUser(ApplicationUser user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (string role in userRoles)
            {
                var roleData = await _roleManager.FindByNameAsync(role);
            }
            authClaims.Add(new Claim(ClaimTypes.Role, "manyRole"));

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DefaultApplication.SecretKey));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(24),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }
        
        
        
    }
}