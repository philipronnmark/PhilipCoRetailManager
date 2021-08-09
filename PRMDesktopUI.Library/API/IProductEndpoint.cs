using PRMDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRMDesktopUI.Library.API
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}