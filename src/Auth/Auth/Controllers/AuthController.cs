using Auth.Core.Requests;
using Auth.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Auth.Core.Extensions;
using System.Security.Claims;
using Auth.Core.Models;
using Auth.Core.Responsesl;

namespace Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AccountsDbContext _accountsDbContext;

        public AuthController(IConfiguration configuration, AccountsDbContext accountsDbContext)
        {
            _configuration = configuration;
            _accountsDbContext = accountsDbContext;
        }

        [AllowAnonymous()]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUser loginModel)
        {
            var existAccount = _accountsDbContext.Accounts.SingleOrDefault(el =>
                el.Login == loginModel.Login && el.PasswordHash == loginModel.Password.HashPassword());

            if (existAccount is null)
                return NotFound(new LoginResponse
                {
                    StatusCode = 500,
                    Response = "Account not found"
                });

            var token = await Generate(existAccount);

            return Ok(new LoginResponse
            {
                UserId = existAccount.UserId.ToString(),
                Token = token,
                Response = "access granted",
                StatusCode = 200,
            });
        }

        [NonAction]
        private async Task<string> Generate(Account existAccount)
        {
            var rsa = RSA.Create();

            string key = await System.IO.File.ReadAllTextAsync(_configuration["SecurityOptions:PrivateKeyFilePath"]!);

            rsa.FromXmlString(key);

            var credentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

            var claims = new List<Claim>()
            {
                new Claim("UserId", existAccount.UserId.ToString()),
            };

            var token = new JwtSecurityToken(
                _configuration["SecurityOptions:Issuer"],
                _configuration["SecurityOptions:Audience"],
                claims,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
