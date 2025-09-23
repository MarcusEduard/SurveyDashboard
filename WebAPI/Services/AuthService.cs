using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace WebAPI.Services;
public class AuthService : IAuthService
{
    private readonly IList<User> users =
    [
        new()
        {
            Username = "Guest",
            Email = "guest@gmail.com",
            FullName = "Guest User",
            Domain = "Guest.com",
            Password = "Guest1234",
            Role = "Guest",
            SecurityLevel = 2,
            Birthday = new DateTime(1988, 3, 14)
        },
        new()
        {
            Username = "Marcus",
            Email = "marcus@gmail.com",
            FullName = "Marcus Eduard",
            Domain = "via.dk",
            Password = "Energy1234",
            Role = "Energy",
            SecurityLevel = 2,
            Birthday = new DateTime(1988, 3, 14)
        },
        new()
        {
            Username = "Tech",
            Email = "tech@gmail.com",
            FullName = "Tech User",
            Domain = "Tech.com",
            Password = "Tech1234",
            Role = "Tech",
            SecurityLevel = 2,
            Birthday = new DateTime(1988, 3, 14)
        },
        new()
        {
            Username = "Health",
            Email = "health@gmail.com",
            FullName = "Health User",
            Domain = "Health.com",
            Password = "Health1234",
            Role = "Health",
            SecurityLevel = 2,
            Birthday = new DateTime(1988, 3, 14)
        },
        new()
        {
            Username = "Jasmin",
            Email = "316993@via.dk",
            FullName = "Jasmin Tazli",
            Domain = "admin.com",
            Password = "Fedeko",
            Role = "Admin",
            SecurityLevel = 4,
            Birthday = new DateTime(1988, 3, 14)
        }
    ];

    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)) ?? throw new Exception("User not found");

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

    public Task RegisterUser(User user)
    {

        if (string.IsNullOrEmpty(user.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }

        users.Add(user);

        return Task.CompletedTask;
    }
}
