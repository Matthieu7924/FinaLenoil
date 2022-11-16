using _06_Entity.Models;

namespace _06_Entity.DAO
{
    public interface IProductDAO
    {
        Task<List<Product>> GetAll();
        Task<Product?> GetById(int id);
        Task Create(Product product);
        Task Update(Product product);
        Task Delete(int id);
    }
}
