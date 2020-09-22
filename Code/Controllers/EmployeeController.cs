using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19Jul2020_1.Models;
namespace _19Jul2020_1.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeController()
        {

        }

        // GET: Employee
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Employee emp)
        {
            EntityFrameworkEntities context = new EntityFrameworkEntities();
            Employee e = context.Employees.Where(x => x.Email == emp.Email && x.Password == emp.Password).FirstOrDefault();
            if (e == null)
            {
                ViewBag.Error = "Wrong Passsword";
                return View();
            }
            else
            {
                Session["IsLogin"] = true;
                Session["Email"] = e.Email;
                Session["IsAdmin"] = e.IsAdmin;
                if ( (bool)Session["IsAdmin"])
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Myprofile");
                }
            }
        }

        public ActionResult Index()
        {
            bool IsLogin = Convert.ToBoolean(Session["IsLogin"]);
            bool IsAdmin = Convert.ToBoolean(Session["IsAdmin"]);
            if (IsLogin)
            {
                if (IsAdmin)
                {

                    EntityFrameworkEntities context = new EntityFrameworkEntities();
                    List<Employee> emp = context.Employees.ToList();
                    return View(emp);
                }
                else
                {
                    return RedirectToAction("Myprofile");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Create()
        {
            bool IsLogin = Convert.ToBoolean(Session["IsLogin"]);
            bool IsAdmin = Convert.ToBoolean(Session["IsAdmin"]);
            if (IsLogin)
            {
                if (IsAdmin)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Myprofile");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult Create(Employee emp)
        {


            EntityFrameworkEntities context = new EntityFrameworkEntities();
            context.Employees.Add(emp);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            bool IsLogin = Convert.ToBoolean(Session["IsLogin"]);
            if (IsLogin)
            {

                EntityFrameworkEntities context = new EntityFrameworkEntities();
                Employee emp = context.Employees.Find(id);
                return View(emp);
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            EntityFrameworkEntities context = new EntityFrameworkEntities();
            Employee e = context.Employees.Find(emp.Id);
            e.Name = emp.Name;
            e.Salary = emp.Salary;
            e.Email = emp.Email;
            e.Password = emp.Password;
            e.IsAdmin = emp.IsAdmin;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            bool IsLogin = Convert.ToBoolean(Session["IsLogin"]);
            if (IsLogin)
            {

                EntityFrameworkEntities context = new EntityFrameworkEntities();
                Employee emp = context.Employees.Find(id);
                context.Employees.Remove(emp);
                context.SaveChanges();
                return Json("You have  successfully deleted One record", JsonRequestBehavior.AllowGet);
               
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
           // Session["IsLogin"] = false;
           // Session["Email"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult Myprofile()
        {
            string e =Convert.ToString( Session["Email"]);
            EntityFrameworkEntities context = new EntityFrameworkEntities();
            Employee emp = context.Employees.Where(x=>x.Email==e).FirstOrDefault();
            return View(emp);
        }

    }
}