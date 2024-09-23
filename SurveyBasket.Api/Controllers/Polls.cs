using Microsoft.AspNetCore.Http;
using SurveyBasket.Api.Models;


namespace SurveyBasket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]


public class Polls(IPollServices pollServices) : ControllerBase
{
    private readonly IPollServices _pollServices = pollServices;


    [HttpGet("")]
    public IActionResult GetAll() => Ok(_pollServices);

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
       var CurrentPoll= _pollServices.Get(id);
        return CurrentPoll == null ? NotFound() : Ok(CurrentPoll);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id,Poll poll)
    {
        var IsUpdated = _pollServices.Update(id, poll);
        if (!IsUpdated)
            return NotFound();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        var IsDeleted = _pollServices.Delete(id);
        if (!IsDeleted)
            return NotFound();
        return NoContent();



    }

    [HttpPost("")]
    public IActionResult Add(Poll request) { 
        
        var newPoll = _pollServices.Add(request);

        return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);

    }



}
