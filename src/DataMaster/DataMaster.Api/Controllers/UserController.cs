using DataMaster.Db;
using DataMaster.Models;
using DataMaster.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DataMaster.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : Controller
    {
        private readonly DataMasterDbContext _dbContext;

        public UserController(DataMasterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <response code="400">RFC 7807: https://learn.microsoft.com/en-us/aspnet/core/web-api/#default-badrequest-response</response>
        /// <response code="409">Логин занят</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(UserCreateRequest model)
        {
            if (await _dbContext.Users.AnyAsync(_ => _.Login == model.Login))
                return StatusCode(StatusCodes.Status409Conflict, "Login is already in use");

            var entity = new User
            {
                Login = model.Login,
                Name = model.Name,
                Email = model.Email,
                PasswordHash = HashPassword(model.Password)
            };

            _dbContext.Users.Add(entity);

            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity.Id);
        }

        /// <summary>
        /// Пользователь
        /// </summary>
        /// <response code="400">RFC 7807: https://learn.microsoft.com/en-us/aspnet/core/web-api/#default-badrequest-response</response>
        /// <response code="404">сущность не найдена</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(_ => _.Id == id);

            if (user is null) return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Получить пользователя по логину и паролю
        /// </summary>
        /// <response code="400">Пользователь с таким логином или паролем не найден</response>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(UserLoginRequest model)
        {
            var entity = await _dbContext.Users
                .SingleOrDefaultAsync(_ => _.Login == model.Login);

            if (entity == null || entity.PasswordHash != HashPassword(model.Password))
                return BadRequest();

            return Ok(entity);
        }

        private static string HashPassword(string password)
        {
            var md5 = MD5.Create();

            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(bytes);

            var sb = new StringBuilder();

            foreach (var h in hash)
                sb.Append(h.ToString("X2"));

            return sb.ToString();
        }
    }
}
