using Library_API.AppUserAccesor;
using Library_API.Repository;
using Library_API.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence;

namespace Library_API.Extensions
{
    public static class ApplicationServiceExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {


            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "Library_API", Version = "v1" });
                options.EnableAnnotations();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. ",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });

            var connection = "Server=KELVIS\\DEVOPS;Database=dbLibrary;User Id=devops;Password=Windows2020;TrustServerCertificate=True;MultipleActiveResultSets=True;";
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));

            //Get data from Security Claims
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccesor, UserAccesor>();

            services.AddScoped<TokenGenerator, TokenGenerator>();


            
            services.AddAutoMapper(typeof(MappingConfig).Assembly);


            return services;
        }
    }
}
