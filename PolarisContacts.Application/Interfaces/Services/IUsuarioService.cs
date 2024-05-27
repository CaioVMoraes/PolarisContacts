using PolarisContacts.Domain;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUserByPasswordAsync(string login, string senha);
        Task<bool> CreateUserAsync(string login, string senha);
        Task<bool> ChangeUserPasswordAsync(string login, string oldPassword, string newPassword);
    }
}