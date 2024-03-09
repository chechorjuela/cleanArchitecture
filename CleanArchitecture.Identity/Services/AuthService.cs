using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new Exception($"El usuario con el correo {request.Email} no existe");

            var response = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!response.Succeeded)
                throw new Exception("Las credenciales son incorrectas");


            var token = new JwtSecurityTokenHandler().WriteToken(await GenerateNewToken(user));
            return new AuthResponse
            {
                Id = user.Id,
                Token = token,
                Email = user.Email,
                Username = user.UserName
            };
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existUser = await _userManager.FindByNameAsync(request.Username);
            if(existUser != null)
                throw new Exception($"User: {request.Username} already exists.");

            var existEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existEmail != null)
                throw new Exception($"Email: {request.Email} already exists.");

            var user = new ApplicationUser {
                Email = request.Email,
                Firstname = request.Firstname, 
                Lastname = request.Lastname,
                UserName = request.Username,
                EmailConfirmed = true,
            };
            var token = new JwtSecurityTokenHandler().WriteToken(await GenerateNewToken(user));

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user, "User");
                return new RegistrationResponse {
                    Email = user.Email,
                    Token = token,
                    UserId = user.Id,
                    Username = user.Firstname,
                };
            }
            throw new Exception($"Error: {result.Errors}");
        }

        private async Task<JwtSecurityToken> GenerateNewToken(ApplicationUser user) {
            
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.UId, user.Id),
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;

        }
    }
}
