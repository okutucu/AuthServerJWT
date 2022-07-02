using AuthServer.Core.Models;

namespace AuthServer.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public void SpeacialMethodForProduct();
    }
}
