

using SurveyBasket.Api.Models;


namespace SurveyBasket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]


public class PollsController(IPollServices pollServices) : ControllerBase
{
    private readonly IPollServices _pollServices = pollServices;


    [HttpGet("")]
    public IActionResult GetAll()
    { 
       var Polls = _pollServices.GetAll();
       var MappedPolls= Polls.Adapt<PollResponse>();


        return Ok(MappedPolls);
    }



    [HttpGet("{id}")]
    public IActionResult Get([FromRoute]int id)
    {
       var CurrentPoll= _pollServices.Get(id);
        var response = CurrentPoll.Adapt<PollResponse>();



        return CurrentPoll == null ? NotFound() : Ok(response);
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
    public IActionResult Add(PollRequest request) { 
        var MappedRequest = request.Adapt<Poll>();
        
        var newPoll = _pollServices.Add(MappedRequest);

        return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);

    }



}
