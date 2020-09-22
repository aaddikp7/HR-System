using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19Jul2020_1.Models;
namespace _19Jul2020_1.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        public ActionResult In()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PostIn()
        {
            Attendance a = new Attendance();
            string e = Convert.ToString(Session["Email"]);
            EntityFrameworkEntities context = new EntityFrameworkEntities();

            Employee emp = context.Employees.Where(x => x.Email == e).FirstOrDefault();
            a.Date = DateTime.Now;
            a.EmplyeeId = emp.Id;
            a.PunchIn = DateTime.Now;
            context.Attendances.Add(a);
            context.SaveChanges();

          //  ViewBag.PunchInMessage = "You have punch in successfully"

            return  Json("You have punch in successfully");
        }

        public ActionResult Out()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostOut()
        {
            Attendance a = new Attendance();
            string e = Convert.ToString(Session["Email"]);
            EntityFrameworkEntities context = new EntityFrameworkEntities();

            Employee emp = context.Employees.Where(x => x.Email == e).FirstOrDefault();
            List<Attendance> dbs = context.Attendances.Where(x => x.EmplyeeId == emp.Id).OrderBy(x=>x.Date).ToList();
            Attendance db = dbs.Where(x =>  x.Date.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy")).LastOrDefault();

            if (db != null)
            {

                db.PunchOut = DateTime.Now;

                context.SaveChanges();
                // ViewBag.PunchInMessage = "You have punch out successfully";
                return Json("You have punch out successfully");
            }
            else
            {
                // ViewBag.PunchInMessage = "No record for today punch in";
                return Json("No record for taday punch in");

            }

            
        }
    }
}