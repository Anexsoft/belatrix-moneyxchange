using Belatrix.Models.Domain;
using Belatrix.Task.Service.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Belatrix.Task.Service
{
    public class UserCreateEventHandler :
        IRequestHandler<UserCreateCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCreateEventHandler(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(UserCreateCommand notification, CancellationToken cancellationToken)
        {
            var entry = new ApplicationUser
            {
                Name = notification.Name,
                LastName = notification.LastName,
                Email = notification.Email,
                UserName = notification.Email
            };

            return await _userManager.CreateAsync(entry, notification.Password);
        }
    }
}
