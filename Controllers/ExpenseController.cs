using PerkyRabbit.Models;
using PerkyRabbit.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerkyRabbit.Controllers
{
    public class ExpenseController : Controller
    {
        public ActionResult Index()
        {
            var db = new PerkyContext();
            var catagories = (from catagory in db.Catagories
                              where catagory.Status.Equals(true)
                              select catagory).ToList();
            return View(catagories);
        }

        [HttpGet]
        public ActionResult AddCatagory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCatagory(Catagory catagory)
        {
            if(ModelState.IsValid)
            {
                var db = new PerkyContext();
                var catagories = db.Catagories;
                bool isValid = false;
                foreach(var i in catagories)
                {
                    if(i.Name.ToLower() == catagory.Name.ToLower())
                    {
                        isValid = true;
                    }
                }
                if(isValid)
                {
                    TempData["ErrMsg"] = "This Catagory already exist!";
                    return View(catagory);
                }
                else
                {
                    db.Catagories.Add(catagory);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Expense");
                }
            }
            return View(catagory);
        }

        [HttpGet]
        public ActionResult EditCatagory(int id)
        {
            var db = new PerkyContext();
            var catagory = (from c in db.Catagories
                            where c.Id == id
                            select c).SingleOrDefault();

            return View(catagory);
        }
        
        [HttpPost]
        public ActionResult EditCatagory(Catagory catagoryEdit)
        {
            var db = new PerkyContext();
            var catagory = (from c in db.Catagories
                                where c.Id == catagoryEdit.Id
                                select c).SingleOrDefault();

            catagory.Name = catagoryEdit.Name;
            db.SaveChanges();
            return RedirectToAction("Index", "Expense");
        }

        public ActionResult Bills(int id)
        {
            var db = new PerkyContext();
            var catagory = (from c in db.Catagories
                            where c.Id == id
                            select c.Name).SingleOrDefault();

            ViewBag.Bill = catagory;
            
            var bills = (from expense in db.Expenses
                         where expense.Catagory_Id == id
                         select expense).ToList();

            return View(bills);
        }
    }
}