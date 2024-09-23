
using SurveyBasket.Api.Controllers;
using SurveyBasket.Api.Models;

namespace SurveyBasket.Api.Services;


public class PollServices : IPollServices
{

    private static readonly List<Poll> _pollList = [
        new Poll{
            Id = 1,
            Title = "poll 1",
             Description = "this is description of poll"

        }

        ];



    public IEnumerable<Poll> GetAll() =>  _pollList ;


    public bool Delete(int id)
    {
        var Poll = Get(id);
        if (Poll == null)
            return false;
       
        _pollList.Remove(Poll);

        return true;
    }

    public Poll? Get(int id) => _pollList.FirstOrDefault(x => x.Id == id);



    public bool Update(int id, Poll poll)
    {
        var CurrentPoll = Get(id);
        if (CurrentPoll == null)
            return false;

    
        CurrentPoll.Title = poll.Title;
        CurrentPoll.Description = poll.Description; 
        return true;
        
    }

    public Poll Add(Poll poll)
    {
        poll.Id = _pollList.Count + 1;
       _pollList.Add(poll);
        return poll;
    }
}
