using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Data.Interfaces;
using UnitOfWork.Domain.Models.Security;

namespace UnitOfWork.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly ILogger<UserController> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _unitOfWork.User.All();
        return Ok(users);
    }

    [HttpGet("GetItem/{id}")]
    public async Task<IActionResult> GetItem(Guid id)
    {
        var item = await _unitOfWork.User.GetById(id);

        if(item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(User user)
    {
        if(ModelState.IsValid)
        {
                user.Id = Guid.NewGuid();

            await _unitOfWork.User.Add(user);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("GetItem", new {user.Id}, user);
        }

        return new JsonResult("Somethign went wrong") {StatusCode = 500};
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(Guid id, User user)
    {
        if(id != user.Id)
            return BadRequest();

        await _unitOfWork.User.Update(user);
        await _unitOfWork.CompleteAsync();

        // Following up the REST standart on update we need to return NoContent.
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(Guid id)
    {
        var item = await _unitOfWork.User.GetById(id);

        if(item == null)
            return BadRequest();

        await _unitOfWork.User.Delete(id);
        await _unitOfWork.CompleteAsync();

        return Ok(item);
    }
}