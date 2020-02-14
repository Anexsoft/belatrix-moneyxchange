using Belatrix.Task.Service.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Belatrix.Task.Service.Commands
{
    public class UserLoginCommand : IRequest<IdentityAccess>
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
