
namespace SurveyBasket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]


public class PollsController(IPollServices pollServices) : ControllerBase
{
    private readonly IPollServices _pollServices = pollServices;


    #region Get All Polls

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var Polls = await _pollServices.GetAllAsync(cancellationToken);
        var MappedPolls = Polls.Adapt<IEnumerable<PollResponse>>();


        return Ok(MappedPolls);
    }
    #endregion



    #region Get Poll By Id
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var CurrentPoll = await _pollServices.GetAsync(id, cancellationToken);
        var response = CurrentPoll.Adapt<PollResponse>();



        return CurrentPoll == null ? NotFound() : Ok(response);
    }

    #endregion



    #region Update Poll by Id
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PollRequest request, CancellationToken cancellationToken)
    {
        var IsUpdated = await _pollServices.UpdateAsync(id, request.Adapt<Poll>(), cancellationToken);
        if (!IsUpdated)
            return NotFound();
        return NoContent();
    }
    #endregion


    #region Delete poll by Id
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var IsDeleted = await _pollServices.DeleteAsync(id, cancellationToken);
        if (!IsDeleted)
            return NotFound();
        return NoContent();


    }
    #endregion


    #region Add or Create New Poll
    [HttpPost("")]
    public async Task<IActionResult> Add(PollRequest request, CancellationToken cancellationToken)
    {
        var MappedRequest = request.Adapt<Poll>();

        var newPoll = await _pollServices.AddAsync(MappedRequest, cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);

    }
    #endregion

    #region Toggle Publish statue

    [HttpPut("{id}/togglePublish")]
    public async Task<IActionResult> TogglePublish([FromRoute] int id, CancellationToken cancellationToken)
    {
        var IsUpdated = await _pollServices.TogglePublishStatueAsync(id, cancellationToken);
        if (!IsUpdated)
            return NotFound();
        return NoContent();


    } 
    #endregion
}
