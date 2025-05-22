using Application.Configuration.Interceptors;
using Application.Users;
using backend.src.Application.Data;
using backend.src.Application.Service.Auth;
using backend.src.Application.Service.Posts;
using backend.src.Application.Service.Users;

namespace backend.src.Application.Configuration.DependencyInjection
{
    public class ApplicationServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();

            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
            services.AddSingleton<DbOperationTimingInterceptor>();
        }
    }
}
