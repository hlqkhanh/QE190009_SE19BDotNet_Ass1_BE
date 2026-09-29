using Microsoft.AspNetCore.Mvc;
using TaskTrack.Service.DTOs;
using TaskTrack.Service.Interfaces;

namespace TaskTrack.API.Controllers;

[ApiController, Route("api/tags")]
public sealed class TagsController(ITagService service) : ControllerBase
{
    [HttpGet] public async Task<ActionResult<IReadOnlyList<TagDto>>> GetAll(CancellationToken ct) => Ok(await service.GetAllAsync(ct));

    [HttpPost]
    public async Task<ActionResult<TagDto>> Create(TagRequest request, CancellationToken ct)
    {
        var created = await service.CreateAsync(request, ct);
        return Created($"/api/tags/{created.TagId}", created);
    }

    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, TagRequest request, CancellationToken ct) { await service.UpdateAsync(id, request, ct); return NoContent(); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id, CancellationToken ct) { await service.DeleteAsync(id, ct); return NoContent(); }
}
