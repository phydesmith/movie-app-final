using System;
using Microsoft.Extensions.DependencyInjection;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Dao;
using MovieApp.Services;


namespace MovieLibraryOO;

/// <summary>
///     Used for registration of new interfaces
/// </summary>
public class Startup
{
    public IServiceProvider ConfigureServices()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddTransient<IMainService, MainService>();
        services.AddSingleton<IRepository, Repository>();
        services.AddDbContextFactory<MovieContext>();


        return services.BuildServiceProvider();
    }

}