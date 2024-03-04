using MediatR;
using TaskManager.Core.DTOs.Responses.Tasks;
using TaskManager.Core.DTOs.Responses;
using System.Text.Json.Serialization;

namespace TaskManager.Core.DTOs.Requests.Tasks
{
    public class CreateTaskListRequest : IRequest<BaseResponseDto<CreateTasksListResponse>>
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; } = null!;
    }
}
