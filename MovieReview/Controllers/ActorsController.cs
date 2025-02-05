using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReview.Data.Services.Interfaces;
using MovieReview.Data.Static;
using MovieReview.Mappers;
using MovieReview.Models.DTOs;
using MovieReview.Models.Entities;

namespace MovieReview.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActorsController : ControllerBase
{
    private readonly IActorService actorService;

    public ActorsController(IActorService actorService) => this.actorService = actorService;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var actors = await actorService.GetAllAsync(a => a.MovieActors);
        if (actors == null)
            return NotFound();

        return Ok(actors.Select(a => a.ToReadDto()).ToList());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var actor = await actorService.GetActorIncluded(id);
        if (actor == null)
            return NotFound();

        return Ok(actor.ToReadDto());
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Create([FromBody] ActorCreateDto actorDto)
    {
        var actor = await actorService.CreateActorAsync(actorDto);

        return CreatedAtAction(nameof(Get), new { id = actor.Id }, actor.ToReadDto());
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Update(int id, [FromBody] ActorUpdateDto actorDto)
    {
        if (id != actorDto.Id)
            return BadRequest();

        var actor = await actorService.UpdateActorAsync(actorDto);
        if (actor == null)
            return NotFound();

        return Ok(actor.ToReadDto());
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        try {
            var actor = await actorService.GetByIdAsync(id);
            if (actor == null)
                return NotFound();

            await actorService.DeleteAsync(id);
            await actorService.SaveAsync();

            return NoContent();
        } catch {
            return BadRequest();
        }
    }
}
