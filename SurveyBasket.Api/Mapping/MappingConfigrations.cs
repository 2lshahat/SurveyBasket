using Mapster;
using SurveyBasket.Api.Contracts.Requests;
using SurveyBasket.Api.Contracts.Responses;

namespace SurveyBasket.Api.Mapping;

public class MappingConfigrations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PollRequest, PollResponse>().TwoWays();
        config.NewConfig<Poll, PollResponse>().Map(dest => dest.Summary, src => src.Summary).TwoWays();
                
        
    }
}
