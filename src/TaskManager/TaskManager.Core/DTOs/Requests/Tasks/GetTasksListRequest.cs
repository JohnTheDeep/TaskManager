using MediatR;
using System.Text.Json.Serialization;
using TaskManager.Core.DTOs.Requests.Common;
using TaskManager.Core.DTOs.Responses;
using TaskManager.Core.DTOs.Responses.Tasks;

namespace TaskManager.Core.DTOs.Requests.Tasks;

public record GetTasksListRequest : IRequest<BaseResponseDto<GetTasksListResponse>>
{
    /// <summary>
    /// Наименование поля по которому необходимо фильтровать
    /// </summary>
    public string? PropertyFilter { get; set; }

    /// <summary>
    /// Значение поля по которому необходимо фильтровать
    /// </summary>
    public string? PropertyValue { get; set; }

    /// <summary>
    /// Параметры поиски
    /// </summary>
    public SearchOptions? SearchOption { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
}
