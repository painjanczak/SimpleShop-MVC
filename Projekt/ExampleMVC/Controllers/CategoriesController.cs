using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Example.Entities;
using Example.Entities.Models;
using Example.DataAccessLayer.Repositories.Abstract;

namespace ExampleMVC.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoriesController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        // GET: Categories
        public async Task<ActionResult> Index()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = await _categoryRepository.GetCategoryAsync(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var result = await _categoryRepository.SaveCategoryAsync(category);
            if (!result)
                return View("Error");

            var categories = await _categoryRepository.GetCategoriesAsync();
            return PartialView("_categoryListPartial", categories);         
        }

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await _categoryRepository.GetCategoryAsync(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.SaveCategoryAsync(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await _categoryRepository.GetCategoryAsync(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Category category = await _categoryRepository.GetCategoryAsync(id);
            await _categoryRepository.DeleteCategoryAsync(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CategoryListPartial()
        {
            var categories = Task.Run(() =>  _categoryRepository.GetCategoriesAsync()).Result;
            return PartialView("_categoryListPartial", categories);
        }
        public ActionResult Product()
        {
            return View("~/Views/Products/Indeex.cshtml");
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> CategoryProducts(int? id)
        {
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Category category = await _categoryRepository.GetCategoryAsync(id.Value);
                var products = await _productRepository.GetProductsAsync();
                products = products.Where(p => p.CategoryId == id).ToList<Product>();
                /*
                foreach (var item in products)
                {

                    if (item.CategoryId == id)
                    {
                        products.Add(item);
                    }
                }
                */
                if (category == null)
                {
                    return HttpNotFound();
                }

                return View(products);
            }
        }

    }
}

//[Bind(Include = "Id,Name")] Category category