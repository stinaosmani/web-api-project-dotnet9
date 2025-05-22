namespace backend.src.Application.Service.Auth
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string username, string role);
    }

}
