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
using ExampleMVC.Models;
using Microsoft.AspNet.Identity;

namespace ExampleMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoriesRepository;
        private readonly IOrderRepository _orderRepository;
        public ProductsController(IProductRepository productRepository, ICategoryRepository categoriesRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _categoriesRepository = categoriesRepository;
            _orderRepository = orderRepository;
        }

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var products = await _productRepository.GetProductsAsync();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _productRepository.GetProductAsync(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public async Task<ActionResult> Create()
        {
            var categories = await _categoriesRepository.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            //ViewBag.CategoryId = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View();
        }
        public async Task<ActionResult> Add(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                List<CartViewModel> cart = (List<CartViewModel>)Session["cart"];
                if(cart != null) ViewBag.Price = cart.Sum(b => b.product.Price);
                return View("Cart");
            }
            var product = await _productRepository.GetProductAsync(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (Session["cart"] == null)
            {
                List<CartViewModel> cart = new List<CartViewModel>();
                cart.Add(new CartViewModel(product, 1));
                Session["cart"] = cart;
                ViewBag.Price = cart.Sum(b => b.product.Price);
            }
            else 
            {
                List<CartViewModel> cart = (List<CartViewModel>)Session["cart"];
                cart.Add(new CartViewModel(product, 1));
                Session["cart"] = cart;
                ViewBag.Price = cart.Sum(b => b.product.Price);
            }    
            return View("Cart");            
        }

        public async Task<ActionResult> DeleteFromCart(int? id)
        {
            if (id == null)
            {
                ViewBag.Price = 0;
                return View("Cart");
            }
            
            if (Session["cart"] == null)
            {
                List<CartViewModel> cart = new List<CartViewModel>();
                ViewBag.Price = 0;
                Session["cart"] = cart;
            }
            else
            {
                List<CartViewModel> cart = (List<CartViewModel>)Session["cart"];
                Product product = await _productRepository.GetProductAsync(id.Value);
                cart.RemoveAt(cart.FindIndex(item => item.product.Id == id));
                Session["cart"] = cart;
                ViewBag.Price = cart.Sum(b => b.product.Price);
            }
            return View("Cart");
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Price,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.SaveProductAsync(product);
                return RedirectToAction("Index");
            }
            var categories = await _categoriesRepository.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _productRepository.GetProductAsync(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }


            var categories = await _categoriesRepository.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", product.CategoryId);
            //ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Price,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.SaveProductAsync(product);
                return RedirectToAction("Index");
            }
            var categories = await _categoriesRepository.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", product.CategoryId);
            //ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _productRepository.GetProductAsync(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await _productRepository.GetProductAsync(id);
            await _productRepository.DeleteProductAsync(product);
            return RedirectToAction("Index");
        }        
    }
}
