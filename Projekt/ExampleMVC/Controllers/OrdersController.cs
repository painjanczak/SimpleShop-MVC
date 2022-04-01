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

namespace ExampleMVC.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrdersController(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            List<CartViewModel> cart = (List<CartViewModel>)Session["cart"];
            if (cart == null)
            {
                ViewBag.Price = 0;
            }
            else ViewBag.Price = cart.Sum(b => b.product.Price);
            var orders = await _orderRepository.GetOrdersAsync();
            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = await _orderRepository.GetOrderAsync(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {            
            List<CartViewModel> cart = (List<CartViewModel>)Session["cart"];
            if (!Request.IsAuthenticated)
            { 
                ViewBag.User = ""; 
            }
            else ViewBag.User = User.Identity.Name;

            if (cart == null)
            {
                ViewBag.Price = 0;
            }
            else 
            {                
                ViewBag.Price = (int)cart.Sum(b => b.product.Price);
            }
            var order = new Order();
            order.Name = ViewBag.Name;
            order.TotalPrice = ViewBag.Price;
            return View(order);
        }

        // POST: Orders/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,TotalPrice")] Order order)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            var result = await _orderRepository.SaveOrderAsync(order);
            if (!result)
                return View("Error");

            var orders = await _orderRepository.GetOrdersAsync();

            List<CartViewModel> cart = new List<CartViewModel>();
            Session["cart"] = cart;
 
            return PartialView("_orderListPartial", orders);
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await _orderRepository.GetOrderAsync(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.SaveOrderAsync(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await _orderRepository.GetOrderAsync(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await _orderRepository.GetOrderAsync(id);
            await _orderRepository.DeleteOrderAsync(order);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult OrderListPartial()
        {
            var orders = Task.Run(() => _orderRepository.GetOrdersAsync()).Result;
            return PartialView("_orderListPartial", orders);
        }
    }
}
