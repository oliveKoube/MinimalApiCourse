using Application;
using Application.Abstractions;
using Carter;
using DataAccess;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Abstractions;

namespace MinimalApi.Extensions;

public static class MinimalApiExtensions
{
  public static void RegisterServices(this WebApplicationBuilder builder)
  {
      builder.Services.AddDbContext<SocialDbContext>(opt => opt.UseSqlServer(
                  builder.Configuration.GetConnectionString("Database")));
      builder.Services.AddScoped<IPostRepository,PostRepository>();
      builder.Services.AddApplication();
      builder.Services.AddCarter();
  }

  //Reflection
  public static void RegisterEndPointDefinitions(this WebApplication app)
  {
      /*scan assembly
      recupère les types
      filtre des types pour garder les implémentations de IEndPointDefinitions
      On crée des instances des elements récupérer
      Et on précise leur type car la reflection ne sait pas de quel type les résultats récupérés sont
      Et donc chaque implémentation de IEndPointDefinitions possède forcement la méthode RegisterEndpoints*/
      var endpointDefinitions = typeof(Program).Assembly 
          .GetTypes() 
          .Where(t => t.IsAssignableTo(typeof(IEndPointDefinitions)) 
                      && !t.IsAbstract && !t.IsInterface) 
          .Select(Activator.CreateInstance)
          .Cast<IEndPointDefinitions>();

      foreach (var endpointDefinition in endpointDefinitions)
      {
          endpointDefinition.RegisterEndpoints(app);
      }
  }
}