using Microsoft.AspNetCore.Mvc;
using TaskManagement.Service.DTOs;
using TaskManagement.Service.Interfaces;

namespace TaskManagement.API.Controllers;

[ApiController, Route("api/tasks")]
public sealed class TasksController(ITaskService service) : ControllerBase
{
    [HttpGet] public async Task<ActionResult<IReadOnlyList<TaskListDto>>> GetAll(CancellationToken ct) => Ok(await service.GetAllAsync(ct));
    [HttpGet("{id:int}")] public async Task<ActionResult<TaskDetailDto>> GetById(int id, CancellationToken ct) => Ok(await service.GetByIdAsync(id, ct));
    [HttpGet("project/{projectId:int}")] public async Task<ActionResult<IReadOnlyList<TaskListDto>>> GetByProject(int projectId, CancellationToken ct) => Ok(await service.GetByProjectAsync(projectId, ct));
    [HttpGet("search")] public async Task<ActionResult<IReadOnlyList<TaskListDto>>> Search([FromQuery] string? title, [FromQuery] short? status, [FromQuery] short? priority, [FromQuery] int? projectId, [FromQuery] int? tagId, CancellationToken ct) => Ok(await service.SearchAsync(title, status, priority, projectId, tagId, ct));

    [HttpPost]
    public async Task<ActionResult<TaskDetailDto>> Create(TaskRequest request, CancellationToken ct)
    {
        var created = await service.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.TaskId }, created);
    }

    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, TaskRequest request, CancellationToken ct) { await service.UpdateAsync(id, request, ct); return NoContent(); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id, CancellationToken ct) { await service.DeleteAsync(id, ct); return NoContent(); }
}
