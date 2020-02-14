using Belatrix.Models.Domain;
using Belatrix.Persistence;
using Belatrix.Task.Service.Commands;
using Belatrix.Task.Service.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Belatrix.Task.Service
{
    public class UserLoginEventHandler :
        IRequestHandler<UserLoginCommand, IdentityAccess>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<UserLoginEventHandler> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserLoginEventHandler(
            SignInManager<ApplicationUser> signInManager,
            ILogger<UserLoginEventHandler> logger,
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
            _logger = logger;
        }

        public async Task<IdentityAccess> Handle(UserLoginCommand notification, CancellationToken cancellationToken)
        {
            var result = new IdentityAccess();

            var user = await _context.Users.SingleAsync(x => x.Email == notification.Email);
            var response = await _signInManager.CheckPasswordSignInAsync(user, notification.Password, false);

            if (response.Succeeded)
            {
                _logger.LogInformation($"Successful authentication by {user.Email}");

                result.Succeeded = true;
                GenerateToken(user, result);
            }
            else 
            {
                _logger.LogWarning($"Unsuccessful authentication by {user.Email}");
            }

            return result;
        }

        private void GenerateToken(ApplicationUser user, IdentityAccess identity)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            identity.AccessToken = tokenHandler.WriteToken(createdToken);
        }
    }
}
