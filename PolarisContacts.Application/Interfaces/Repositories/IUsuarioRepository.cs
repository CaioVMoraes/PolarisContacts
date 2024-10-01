using PolarisContacts.Domain;
using System.Threading.Tasks;

namespace PolarisContacts.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUserByPasswordAsync(string login, string senha);
        Task<bool> CreateUserAsync(Usuario usuario);
        Task<bool> ChangeUserPasswordAsync(string login, string oldPassword, string newPassword);
    }
}