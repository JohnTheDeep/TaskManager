using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.DTOs.Requests.User;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProfileController : Controller
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Редактирование профиля пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            request.UserId = HttpContext.Session.GetString("UserId") ?? string.Empty;

            var response = await _mediator.Send(request);

            if (response.HasErrors)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
