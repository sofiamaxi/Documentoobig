using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Documentoobig.Models;

namespace Documentoobig.Controllers
{
    public class OrdersController : Controller
    {
        private Post db = new Post();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Client).Include(o => o.From).Include(o => o.FromDep).Include(o => o.Receiver).Include(o => o.Staff).Include(o => o.To).Include(o => o.ToDep);
            return View(orders.ToList());
        }
        public ActionResult CreateDocument(int id)
        {
            Orders orders = db.Orders.Find(id);

            var wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.Visible = false;
            var wordDocument = wordApp.Documents.Open(@"R:\Kursach\Documentoobig\Documentoobig\Content\maket.doc");
            string pathname = @"R:\Kursach\Documentoobig\Documentoobig\Content";
            string filename = "Document" + id + DateTime.Now.Millisecond;

            ReplaseWordStub("{Nomer}", orders.Id.ToString(), wordDocument);
            ReplaseWordStub("{Date}", DateTime.Now.ToShortDateString(), wordDocument);
            ReplaseWordStub("{CompanyName}", db.Companies.Find(orders.Staff.CompanyId).Name, wordDocument);
            ReplaseWordStub("{CompanyDirector}", db.Companies.Find(orders.Staff.CompanyId).DirectorFName +" "+ db.Companies.Find(orders.Staff.CompanyId).DirectorLName + " " + db.Companies.Find(orders.Staff.CompanyId).DirectorPName  , wordDocument);
            ReplaseWordStub("{ClientPIB}", orders.Client.Description, wordDocument);

            ReplaseWordStub("{Receiver}", orders.Receiver.Name, wordDocument);
            ReplaseWordStub("{Volume}", (orders.Volume / 100000).ToString() +" м.куб" , wordDocument);
            ReplaseWordStub("{Weight}", orders.Weight.ToString() +" кг" , wordDocument);
            ReplaseWordStub("{OrderPrice}", orders.Cost.ToString() + "грн", wordDocument);
            ReplaseWordStub("{Price}", orders.Price.ToString() + "грн", wordDocument);
            ReplaseWordStub("{ClientPhone}", orders.Client.Phone.ToString(), wordDocument); 
            wordDocument.SaveAs(filename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument);
            wordDocument.Close(false);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wordDocument);

            byte[] fileBytes = System.IO.File.ReadAllBytes(@"C:\Users\V2\Documents\" + filename + ".doc");

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename + ".doc");
        }

        void ReplaseWordStub(string stubToReplace, string text, Microsoft.Office.Interop.Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "Id", "Description");
            ViewBag.FromCityID = new SelectList(db.Cities, "Id", "Name");
            ViewBag.FromDepID = new SelectList(db.Departments, "Id", "Description");
            ViewBag.ReceiverID = new SelectList(db.Receivers, "Id", "Name");
            ViewBag.StaffID = new SelectList(db.Staffs, "Id", "Description");
            ViewBag.ToCityID = new SelectList(db.Cities, "Id", "Name");
            ViewBag.ToDepID = new SelectList(db.Departments, "Id", "Description");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FromCityID,FromDepID,ToCityID,ToDepID,ClientID,StaffID,ReceiverID,Height,Width,Length,Weight,Cost")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "Id", "Description", orders.ClientID);
            ViewBag.FromCityID = new SelectList(db.Cities, "Id", "Name", orders.FromCityID);
            ViewBag.FromDepID = new SelectList(db.Departments, "Id", "Description", orders.FromDepID);
            ViewBag.ReceiverID = new SelectList(db.Receivers, "Id", "Name", orders.ReceiverID);
            ViewBag.StaffID = new SelectList(db.Staffs, "Id", "Description", orders.StaffID);
            ViewBag.ToCityID = new SelectList(db.Cities, "Id", "Name", orders.ToCityID);
            ViewBag.ToDepID = new SelectList(db.Departments, "Id", "Description", orders.ToDepID);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "Id", "Description", orders.ClientID);
            ViewBag.FromCityID = new SelectList(db.Cities, "Id", "Name", orders.FromCityID);
            ViewBag.FromDepID = new SelectList(db.Departments, "Id", "Description", orders.FromDepID);
            ViewBag.ReceiverID = new SelectList(db.Receivers, "Id", "Name", orders.ReceiverID);
            ViewBag.StaffID = new SelectList(db.Staffs, "Id", "Description", orders.StaffID);
            ViewBag.ToCityID = new SelectList(db.Cities, "Id", "Name", orders.ToCityID);
            ViewBag.ToDepID = new SelectList(db.Departments, "Id", "Description", orders.ToDepID);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FromCityID,FromDepID,ToCityID,ToDepID,ClientID,StaffID,ReceiverID,Height,Width,Length,Weight,Cost")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "Id", "Description", orders.ClientID);
            ViewBag.FromCityID = new SelectList(db.Cities, "Id", "Name", orders.FromCityID);
            ViewBag.FromDepID = new SelectList(db.Departments, "Id", "Description", orders.FromDepID);
            ViewBag.ReceiverID = new SelectList(db.Receivers, "Id", "Name", orders.ReceiverID);
            ViewBag.StaffID = new SelectList(db.Staffs, "Id", "Description", orders.StaffID);
            ViewBag.ToCityID = new SelectList(db.Cities, "Id", "Name", orders.ToCityID);
            ViewBag.ToDepID = new SelectList(db.Departments, "Id", "Description", orders.ToDepID);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
