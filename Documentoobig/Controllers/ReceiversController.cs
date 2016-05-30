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
    public class ReceiversController : Controller
    {
        private Post db = new Post();

        // GET: Receivers
        public ActionResult Index()
        {
            return View(db.Receivers.ToList());
        }

        // GET: Receivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receiver receiver = db.Receivers.Find(id);
            if (receiver == null)
            {
                return HttpNotFound();
            }
            return View(receiver);
        }

        // GET: Receivers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Receivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Receiver receiver)
        {
            if (ModelState.IsValid)
            {
                db.Receivers.Add(receiver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(receiver);
        }

        // GET: Receivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receiver receiver = db.Receivers.Find(id);
            if (receiver == null)
            {
                return HttpNotFound();
            }
            return View(receiver);
        }

        // POST: Receivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Receiver receiver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receiver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(receiver);
        }

        // GET: Receivers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receiver receiver = db.Receivers.Find(id);
            if (receiver == null)
            {
                return HttpNotFound();
            }
            return View(receiver);
        }

        // POST: Receivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receiver receiver = db.Receivers.Find(id);
            db.Receivers.Remove(receiver);
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
