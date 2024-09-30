using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using SurveyBasket.Api.Persistence;
using System.Reflection;

namespace SurveyBasket.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services
        , IConfiguration configuration)
    {

        services.AddControllers();
        services.AddSwagger();
        services.AddMapsterConf(); 
        services.AddFluentValidation();
        services.AddDataBase(configuration);


        




        services.AddScoped<IPollServices, PollServices>();

     
        return services;
    }
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
    public static IServiceCollection AddMapsterConf(this IServiceCollection services)
    {
        services.AddMapster();
        //Add Mapster Config
        var MappingConfig = TypeAdapterConfig.GlobalSettings;
        MappingConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(MappingConfig));



        return services;
    }
    public static IServiceCollection AddFluentValidation(this IServiceCollection services) 
    {
        services.AddFluentValidationAutoValidation()
                 .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
            } 

    public static IServiceCollection AddDataBase(this IServiceCollection services , IConfiguration configuration)
    {
        var ConnectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection String 'DefaultConnection' not found ");


        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));


        return services;
    }


}
