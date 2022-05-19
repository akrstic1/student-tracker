using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using StudentTracker.Web.Repositories;
using StudentTracker.Web.Settings;

namespace StudentTracker.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRestApiServices(this IServiceCollection services)
        {
            services
                .AddControllers(options =>
                    options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer())));

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services
                .AddScoped<IProfessorRepository, ProfessorRepository>()
                .AddScoped<IClassRepository, ClassRepository>()
                .AddScoped<IStudentRepository, StudentRepository>();

            return services;
        }
    }
}