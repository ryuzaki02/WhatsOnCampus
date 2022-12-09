using WhatsOnCampus.Model;
namespace WhatsOnCampus.Services
{
	public interface IFirebase
	{
		Task<bool> AddUpdateAsync(User model);
        Task<bool> DeleteAsync(String key);
        Task<List<User>> GetAllAsync();
    }
}

