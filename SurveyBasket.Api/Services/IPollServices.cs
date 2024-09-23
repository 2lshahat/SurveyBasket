using SurveyBasket.Api.Models;

namespace SurveyBasket.Api.Services;

public interface IPollServices
{
    IEnumerable<Poll> GetAll();
    Poll? Get(int id);

    bool Update(int id ,Poll poll);
     
    bool Delete(int id );
    Poll Add(Poll poll);

}
