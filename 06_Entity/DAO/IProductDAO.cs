using _06_Entity.Models;
using _06_Entity.ViewModel;

namespace _06_Entity.DAO
{
    public interface IProductDAO
    {
        // Task<List<Product>> GetAll(string? description = null);

        Task<ProductsPageViewModel> GetAll(ProductsPageViewModel? input = null);

        Task<Product?> GetById(int id);
        Task Create(Product product);
        Task Update(Product product);
        Task Delete(int id);
    }
}
