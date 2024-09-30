
using SurveyBasket.Api.Controllers;
using SurveyBasket.Api.Persistence;
using System.Reflection.Metadata.Ecma335;
using System.Threading;


namespace SurveyBasket.Api.Services;


public class PollServices(ApplicationDbContext dbContext) : IPollServices
{
   
    
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken) => await _dbContext.Polls.AsNoTracking().ToListAsync(cancellationToken);



    public async Task<Poll?> GetAsync(int id, CancellationToken cancellationToken) =>  await _dbContext.Polls.FindAsync(id, cancellationToken);

    public async Task<Poll> AddAsync(Poll poll, CancellationToken cancellationToken)
    {
         await _dbContext.Polls.AddAsync(poll, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return poll;
    }


    public async  Task<bool> UpdateAsync(int id, Poll poll, CancellationToken cancellationToken)
    {
        var CurrentPoll =  await GetAsync(id, cancellationToken);
        if(CurrentPoll is null)
           return false;
        CurrentPoll.Title = poll.Title;
         CurrentPoll.Summary = poll.Summary;
        CurrentPoll.StartAt = poll.StartAt;
        CurrentPoll.EndAt = poll.EndAt;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;

    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var Poll = await GetAsync(id, cancellationToken);
        if (Poll == null)
            return false;


        _dbContext.Remove(Poll);

      await  _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

   public async Task<bool> TogglePublishStatueAsync(int id,  CancellationToken cancellationToken = default)
    {
        var Poll = await GetAsync(id, cancellationToken);
        if (Poll is null)
            return false;
        Poll.IsPublished = !Poll.IsPublished;
       
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;


    }
}
