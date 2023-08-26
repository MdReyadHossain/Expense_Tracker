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

            var catagories = (from c in db.Catagories
                              where c.Id != catagoryEdit.Id
                              select c).ToList();

            bool isValid = false;
            foreach (var i in catagories)
            {
                if (i.Name.ToLower() == catagoryEdit.Name.ToLower())
                {
                    isValid = true;
                }
            }
            if (isValid)
            {
                TempData["ErrMsg"] = "This Catagory already exist!";
                return RedirectToAction("EditCatagory", "Expense", catagoryEdit.Id);
            }
            else
            {
                catagory.Name = catagoryEdit.Name;
                db.SaveChanges();
                return RedirectToAction("Index", "Expense");
            }
        }

        public ActionResult DeletedCatagory()
        {
            var db = new PerkyContext();
            var catagories = (from catagory in db.Catagories
                            where catagory.Status.Equals(false)
                            select catagory).ToList();

            return View(catagories);
        }

        public ActionResult DeleteCatagory(int id)
        {
            var db = new PerkyContext();
            var catagory = (from c in db.Catagories
                              where c.Id == id
                              select c).SingleOrDefault();

            catagory.Status = false;
            db.SaveChanges();
            return RedirectToAction("Index", "Expense");
        }

        public ActionResult RecycleCatagory(int id)
        {
            var db = new PerkyContext();
            var catagory = (from c in db.Catagories
                            where c.Id == id
                            select c).SingleOrDefault();

            catagory.Status = true;
            db.SaveChanges();
            return RedirectToAction("DeletedCatagory", "Expense");
        }

        public ActionResult Bills(int id)
        {
            var db = new PerkyContext();
            var catagory = (from c in db.Catagories
                            where c.Id == id
                            select c.Name).SingleOrDefault();

            ViewBag.Bill = catagory;
            ViewBag.BillId = id;
            
            var bills = (from expense in db.Expenses
                         where expense.Catagory_Id == id
                         select expense).ToList();

            return View(bills);
        }

        [HttpGet]
        public ActionResult AddBill(int id, string name)
        {
            ViewBag.Id = id;
            ViewBag.Name = name;

            return View();
        }

        [HttpPost]
        public ActionResult AddBill(Expense expense)
        {
            if (ModelState.IsValid)
            {
                var db = new PerkyContext();

                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index", "Expense");
            }
            return View(expense);
        }

        [HttpGet]
        public ActionResult EditBill(int id, string name, int catagoryId)
        {
            ViewBag.Name = name;
            var db = new PerkyContext();
            var expense = (from exp in db.Expenses
                           where exp.Id == id
                           select exp).SingleOrDefault();
            return View(expense);
        }

        [HttpPost]
        public ActionResult EditBill(Expense expenseEdit)
        {
            if (ModelState.IsValid)
            {
                var db = new PerkyContext();
                var expense = (from exp in db.Expenses
                                where exp.Id == expenseEdit.Id
                                select exp).SingleOrDefault();

                expense.FirstDate = expenseEdit.FirstDate;
                expense.EndDate = expenseEdit.EndDate;
                expense.Expenditure = expenseEdit.Expenditure;
                db.SaveChanges();
                return RedirectToAction("Index", "Expense");

            }
            return View(expenseEdit);
        }

        public ActionResult DeleteBill(int id)
        {
            var db = new PerkyContext();
            var expense = (from exp in db.Expenses
                           where exp.Id == id
                           select exp).SingleOrDefault();
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Index", "Expense");
        }
    }
}