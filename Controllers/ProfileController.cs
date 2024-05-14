using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Data.Interfaces;
using UnitOfWork.Domain.Models.Security;

namespace UnitOfWork.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController(ILogger<ProfileController> logger, IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly ILogger<ProfileController> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var profiles = await _unitOfWork.Profile.All();
        return Ok(profiles);
    }

    [HttpGet("GetItem/{id}")]
    public async Task<IActionResult> GetItem(Guid id)
    {
        var item = await _unitOfWork.Profile.GetById(id);

        if(item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(Profile profile)
    {
        if(ModelState.IsValid)
        {
                profile.Id = Guid.NewGuid();

            await _unitOfWork.Profile.Add(profile);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("GetItem", new {profile.Id}, profile);
        }

        return new JsonResult("Somethign went wrong") {StatusCode = 500};
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(Guid id, Profile profile)
    {
        if(id != profile.Id)
            return BadRequest();

        await _unitOfWork.Profile.Update(profile);
        await _unitOfWork.CompleteAsync();

        // Following up the REST standart on update we need to return NoContent.
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(Guid id)
    {
        var item = await _unitOfWork.Profile.GetById(id);

        if(item == null)
            return BadRequest();

        await _unitOfWork.Profile.Delete(id);
        await _unitOfWork.CompleteAsync();

        return Ok(item);
    }
}
