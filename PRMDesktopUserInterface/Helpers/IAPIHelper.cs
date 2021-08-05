using PRMDesktopUserInterface.Models;
using System.Threading.Tasks;

namespace PRMDesktopUserInterface.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}