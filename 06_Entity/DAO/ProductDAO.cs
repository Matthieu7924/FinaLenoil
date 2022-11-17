using _06_Entity.Models;
using _06_Entity.ViewModel;
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

        public async Task<List<Product>> GetAll(string? description = null)
        {
            /* if (description is null)
             {
                 // return await _db.Products.Where(x => x.Description.Contains(String.Empty)).ToListAsync();
                 return await _db.Products.ToListAsync();
             }*/

            int? pageNumber = null;
            int? pageSize = null;

            return await _db.Products
                .Where(x => x.Description.Contains(description ?? String.Empty))
                .Skip((pageNumber ?? 0) * (pageSize ?? 10))
                .Take(pageSize ?? _db.Products.Count())
                .ToListAsync();
        }

        public async Task<ProductsPageViewModel> GetAll(ProductsPageViewModel? input)
        {
            ProductsPageViewModel ppvm = input ?? new(); // ppvm = input si input not null sinon ppvm = mew ProductsPageViewModel()

            string description = ppvm.Filter ?? String.Empty;
            int currentPage = ppvm.CurrentPage ?? 0;
            int pageSize = ppvm.PageSize ?? _db.Products.Count();

            ppvm.Products = await _db.Products
                .Where(x => x.Description.Contains(description))
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return ppvm;
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
