using UserService.Model;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        Task<User> GetSingle(int id);
        Task Save();
    }
}
