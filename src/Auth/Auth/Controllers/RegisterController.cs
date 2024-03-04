using Auth.Core.Extensions;
using Auth.Core.Models;
using Auth.Core.Requests;
using Auth.Core.Responses;
using Auth.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        private readonly AccountsDbContext _accountsDbContext;
        private readonly TaskManagerDbContext _taskManagerDbContext;

        public RegisterController(AccountsDbContext accountsDbContext, TaskManagerDbContext taskManagerDbContext)
        {
            _accountsDbContext = accountsDbContext;
            _taskManagerDbContext = taskManagerDbContext;
        }

        [AllowAnonymous()]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUser request)
        {
            var isLoginAlreadyInUse = await _accountsDbContext.Accounts.AnyAsync(_ => _.Login == request.Login);

            if (isLoginAlreadyInUse)
                return BadRequest(new RegisterResponse
                {
                    Response = "Account with same login already exist!",
                    IsRegistered = false,
                    StatusCode = 500
                });

            var user = new User
            {
                Login = request.Login,
                Email = request.Email,
                Name = request.Name
            };

            var account = new Account
            {
                Login = request.Login,
                PasswordHash = request.Password.HashPassword(),
                UserId = user.id
            };

            _taskManagerDbContext.Users.Add(user);
            _accountsDbContext.Accounts.Add(account);

            await _taskManagerDbContext.SaveChangesAsync();
            await _accountsDbContext.SaveChangesAsync();

            return Ok(new RegisterResponse
            {
                Response = "Account has registered",
                IsRegistered = true,
                StatusCode = 200
            });
        }
    }
}
