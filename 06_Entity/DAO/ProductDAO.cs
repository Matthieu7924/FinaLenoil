using _06_Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace _06_Entity.DAO
{
    public class ProductDAO : IProductDAO
    {
        private readonly ApplicationDbContext _db;

        public ProductDAO(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Product product)
        {
            _db.Add(product);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);

            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetAll()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            // return await _db.Products.FirstOrDefaultAsync(m => m.Id == id);
            return await _db.Products.FindAsync(id); // plus efficace
        }

        public async Task Update(Product product)
        {
            _db.Update(product);
            await _db.SaveChangesAsync();
        }
    }
}
