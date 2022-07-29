using Ecomm.DAL;
using Ecomm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm.Controllers
{
    public class RegistrationController : Controller
    {
        RegistrationDAL rd = new RegistrationDAL();


        // GET: RegistrationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegistrationController/Create
        [HttpPost]

        public ActionResult Create(Registration reg)
        {

            try
            {
                int result = rd.AddCustomer(reg);
                if (result == 1)
                {
                    return RedirectToAction("SignIn","Registration");
                }
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: RegistrationController/Create
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        // POST: RegistrationController/Create
        public IActionResult SignIn(Registration reg)

        {

            Registration user = rd.UserLogin(reg);

            if (user.UPassword == reg.UPassword)
            {
           
                    HttpContext.Session.SetString("username", user.UEmail.ToString());
                    HttpContext.Session.SetString("userid", user.UId.ToString());
                    if (user.URoleId == Roles.Customer)
                    {
                        return RedirectToAction("ViewProduct", "Product");
                    }
                    else if (user.URoleId == Roles.Admin)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                    else
                        return View();
                
               
            }
            else
                return View();



            
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }


    }
}
