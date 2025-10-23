using SmartLibrary.Models.DTOs;
using SmartLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Jose;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;

namespace SmartLibrary.Models.Services
{
    public class AuthService:IAuthService 
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTDto _jwt;
        private readonly IMapper _mapp;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWTDto> jwt,IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _mapp = mapper;
        }

        
        public async Task<AuthDto> RegisterAsync(RegisterDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthDto { Message = "Email is already registered!" };

            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new AuthDto { Message = "Username is already registered!" };

            var user = _mapp.Map<ApplicationUser>(model);
            //var user = new ApplicationUser
            //{
            //    UserName = model.Username,
            //    Email = model.Email,
            //    FirstName = model.FirstName,
            //    LastName = model.LastName,
            //    PhoneNumber = model.phoneNumber
            //};

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthDto { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthDto
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }


        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }



     
        public async Task<AuthDto> LoginAsync(LoginDto model)
        {
            var authDto = new AuthDto();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authDto.Message = "Email or Password is incorrect!";
                return authDto;
            }
            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authDto.IsAuthenticated = true;
            authDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authDto.Email = user.Email;
            authDto.Username = user.UserName;
            authDto.ExpiresOn = jwtSecurityToken.ValidTo;
            authDto.Roles = rolesList.ToList();

            return authDto;

        }


        public  async Task<string> AddRoleAsync(AddRoleDto model)
        {
            var user = _userManager.FindByIdAsync(model.UserId).Result;
            if (user == null)
                return  await Task.FromResult("User not found");
            if (!_roleManager.RoleExistsAsync(model.Role).Result)
                return await Task.FromResult("Role not found");
            if (_userManager.IsInRoleAsync(user, model.Role).Result)
                return  await Task.FromResult("User already assigned to this role");

            // Get current roles
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Remove all current roles (if you want to allow only one role per user)
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
                return "Failed to remove old roles";

            var result = _userManager.AddToRoleAsync(user, model.Role).Result;
            return result.Succeeded ? await Task.FromResult(string.Empty) : await Task.FromResult("Something went wrong");
        }

    }
}
