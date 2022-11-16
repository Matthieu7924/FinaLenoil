using _06_Entity.DAO;
using _06_Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _06_Entity.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductDAO _dao;

        public ProductsController(IProductDAO dao)
        {
            _dao = dao;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _dao.GetAll());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                // return NotFound();
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

            var product = await _dao.GetById((int)id);

            if (product == null)
            {
                // return NotFound();
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Prix")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _dao.Create(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _dao.GetById((int)id);

            if (product == null)
            {
                // return NotFound();
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Prix")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _dao.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _dao.GetById((int)id);

            if (product == null)
            {
                // return NotFound();
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) // On ne peut pas avoir 2 Méthodes "Delete" avec la même signature, alors on change le Nom de la méthode et on inversement celui de l'action associée
        {
            var product = await _dao.GetById((int)id);

            if (product != null)
            {
                await _dao.Delete(product.Id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
