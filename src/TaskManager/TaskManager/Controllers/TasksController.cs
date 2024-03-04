using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.DTOs.Requests.Tasks;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TasksController : Controller
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить списки задач пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("[action]")]
        public async Task<IActionResult> GetMyTaskList([FromBody] GetTasksListRequest request)
        {
            request.UserId = HttpContext.Session.GetString("UserId");

            var response = await _mediator.Send(request);

            if (response.HasErrors)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Создать список задач
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTaskList([FromBody] CreateTaskListRequest request)
        {
            request.UserId = HttpContext.Session.GetString("UserId");

            var response = await _mediator.Send(request);

            if (response.HasErrors)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Удаление списка задач
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteTaskList([FromBody] DeleteTaskListRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.HasErrors)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Редактирование списка задач
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateTaskList([FromBody] UpdateTaskListRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.HasErrors)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Удалить задачу из списка
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteTaskFromList([FromBody] DeleteTaskFromListRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.HasErrors)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Добавить новую задачу в список
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("[action]")]
        public async Task<IActionResult> AttachTaskToList([FromBody] AttachTaskToListRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.HasErrors)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Обновить задачу
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.HasErrors)
                return BadRequest(response);

            return Ok(response);
        }


        /// <summary>
        /// Получить инфу о таске, комменты, история статусов
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTaskDetils(string taskId)
        {
            var request = new GetTaskDetailsRequest
            {
                TaskId = taskId
            };

            var response = await _mediator.Send(request);

            if (response.HasErrors)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Слияние списков задач
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> MergeTaskList([FromBody] MergeTaskListRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.HasErrors)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
