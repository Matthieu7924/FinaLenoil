using _06_Entity.Models;

namespace _06_Entity.ViewModel
{
    public class ProductsPageViewModel
    {
        public string Filter { get; set; } = String.Empty;

        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }

        public int NumberOfPages { get; set; }

        public IEnumerable<Product>? Products { get; set; }
    }
}
