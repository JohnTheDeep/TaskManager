using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.DTOs.Requests.User;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IMediator _mediator;
        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.HasErrors) return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginUserReqeust request)
        {
            var response = await _mediator.Send(request);

            if (response.HasErrors) return BadRequest(response);

            HttpContext.Session.SetString("UserId", response.Data.UserId);

            return Ok(response);
        }
    }
}
