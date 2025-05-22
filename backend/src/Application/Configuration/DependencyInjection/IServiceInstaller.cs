namespace backend.src.Application.Configuration.DependencyInjection
{
    public interface IServiceInstaller
    {
        void Install(IServiceCollection services, IConfiguration configuration);
    }
}
