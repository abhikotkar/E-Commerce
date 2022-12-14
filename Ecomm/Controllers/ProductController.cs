using Ecomm.DAL;
using Ecomm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm.Controllers
{
    public class ProductController : Controller
    {
        ProductDAL db = new ProductDAL();
        CartDAL cd = new CartDAL();
        OrderDAL od = new OrderDAL();
        // GET: ProductController
        
        public ActionResult Index()
        {
            var model = db.GetProducts();
            return View(model);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var model = db.GetProductById(id);
            return View(model);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {

            try
            {
                int result = db.AddProduct(product);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = db.GetProductById(id);
            return View(model);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            try
            {
                int result = db.UpdateProduct(product);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = db.GetProductById(id);
            return View(model);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = db.DeleteProduct(id);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

       
       
        public IActionResult ViewProduct()
        {
            var model = db.GetProducts();
            return View(model);
        }

        public IActionResult AddProductToCart(int id)
        {
            string userid =HttpContext.Session.GetString("userid");
            Cart cart = new Cart();
            cart.PId = id;
            cart.UId =Convert.ToInt32( userid);
            int res = cd.AddToCart(cart);
            if (res == 1)
            {
                return RedirectToAction("ViewCart");
            }
            else
            {
                return View();
            }
        }       
        [HttpGet]
        public IActionResult ViewCart()
        {
            string userid = HttpContext.Session.GetString("userid");
            var model = cd.ViewProductsFromCart(userid);
            return View(model);
        }

        [HttpGet]
        public IActionResult RemoveFromCart(int cid)
        {
            int res = cd.RemoveFromCart(cid);
            if (res == 1)
            {
                return RedirectToAction("ViewCart");
            }
            else
            {
                return View();
            }
        }

        public IActionResult AddCartToOrder(int id)
        {
            string userid = HttpContext.Session.GetString("userid");
            Order or = new Order();
            or.PId = id;
            or.UId = Convert.ToInt32(userid);
            int res = od.PlaceOrder(or);
            if (res == 1)
            {
                return RedirectToAction("ViewOrder");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult ViewOrder()
        {
            string userid = HttpContext.Session.GetString("userid");
            var model = od.ViewProductForOrder(userid);
            return View(model);
        }

        [HttpGet]
        public IActionResult RemoveFromOrder(int oid)
        {
            int res = od.RemoveFromOrders(oid);
            if (res == 1)
            {
                return RedirectToAction("ViewOrder");
            }
            else
            {
                return View();
            }
        }

       
    }
}
