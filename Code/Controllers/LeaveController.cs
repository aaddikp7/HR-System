using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19Jul2020_1.Models;

namespace _19Jul2020_1.Controllers
{
    public class LeaveController : Controller
    {
        // GET: Leave
        public ActionResult Apply()
        {
            bool IsLogin = Convert.ToBoolean(Session["IsLogin"]);
            if (IsLogin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult Apply(Leave l)
        {
            
            string e = Convert.ToString(Session["Email"]);
            EntityFrameworkEntities context = new EntityFrameworkEntities();

            Employee emp = context.Employees.Where(x => x.Email == e).FirstOrDefault();
           
            l.EmployeeId = emp.Id;
            l.CreateOn = DateTime.Now;
            l.CreatedBy = emp.Id;
            l.ModifiedOn = DateTime.Now;
                context.Leaves.Add(l);
            context.SaveChanges();
            return View();
        }
        public ActionResult Aproved()
        {
            bool IsLogin = Convert.ToBoolean(Session["IsLogin"]);
            bool IsAdmin = Convert.ToBoolean(Session["IsAdmin"]);
            if (IsLogin)
            {
                if (IsAdmin)
                {
                    EntityFrameworkEntities context = new EntityFrameworkEntities();
                    List<Leave> l = context.Leaves.ToList();
                    return View(l);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Aproveacept(int id)
        {
            EntityFrameworkEntities context = new EntityFrameworkEntities();
            Leave l = context.Leaves.Find(id);
            l.Aproved = "1";
            context.SaveChanges();
            return RedirectToAction("Aproved");
                
                
        }
    }
}