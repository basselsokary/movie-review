using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReview.Data.Services.Interfaces;
using MovieReview.Data.Static;
using MovieReview.Mappers;
using MovieReview.Models.DTOs;

namespace MovieReview.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        this.reviewService = reviewService;
    }

    [HttpGet("{id:int}")]
    //[Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Get(int id)
    {
        var review = await reviewService.GetByIdAsync(id, r => r.Movie, r => r.User);
        if (review == null)
            return NotFound();

        return Ok(review.ToReadDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
    {
        var review = await reviewService.AddAsync(dto.ToEntity());

        return CreatedAtAction(nameof(Get), new { id = review.Id }, review.ToReadDto());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ReviewUpdateDto dto)
    {
        if (id != dto.Id)
            return BadRequest();

        var review = await reviewService.GetByIdAsync(id);
        if (review == null)
            return NotFound();

        review = await reviewService.UpdateAsync(dto.ToEntity(review));
        if (review == null)
            return NotFound();

        await reviewService.SaveAsync();

        return Ok(review.ToReadDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try {
            var review = await reviewService.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            await reviewService.DeleteAsync(id);
            await reviewService.SaveAsync();

            return NoContent();
        } catch {
            return BadRequest();
        }
    }
}