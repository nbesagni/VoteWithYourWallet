using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using VoteWithYourWallet.Models;

namespace VoteWithYourWallet.Controllers
{
    public class CausesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Causes
        public ActionResult Index()
        {
            
            var causes = db.Causes.Include(c => c.ApplicationUser);
            return View(causes.ToList());
        }

        // GET: Causes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cause cause = db.Causes.Find(id);
            if (cause == null)
            {
                return HttpNotFound();
            }


            // Check if user has signed cause before
            var userID = User.Identity.GetUserId();
            if (db.Signatures.Where(u => u.ApplicationUserID == userID && u.CauseID == id).Any())
            {
                ViewBag.AlreadySigned = "True";
            }
            else
            {
                ViewBag.AlreadySigned = "False";
            }




            return View(cause);
        }

        // GET: Causes/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Causes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cause cause, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string path;
                string dbPath;
                if (file != null) {
                    //Generate random string code by User Mora, Source: https://stackoverflow.com/questions/17874086/generate-random-alphanumeric-string-into-password-field
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    var stringChars = new char[8];
                    var random = new Random();
                    
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }

                    var finalString = new String(stringChars);
                    // Image upload amend code by Ankur Mistry https://www.c-sharpcorner.com/article/upload-files-in-Asp-Net-mvc/
                    path = Path.Combine(Server.MapPath("~/Content/Images"), finalString + Path.GetExtension(file.FileName));
                    dbPath = "~/Content/Images/" + finalString + Path.GetExtension(file.FileName);
                    file.SaveAs(path);
                    
                }
                else
                {
                    dbPath = "~/Content/Images/causePhoto.jpg";

                }
                
                db.Causes.Add(new Cause
                {

                    CauseID = cause.CauseID,
                    Title = cause.Title,
                    ApplicationUserID = cause.ApplicationUserID,
                    ShortDescription = cause.ShortDescription,
                    LongDescription = cause.LongDescription,
                    Target = cause.Target,
                    Image = dbPath
                });
                //db.Causes.Add(cause);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "FirstName", cause.ApplicationUserID);
            return View(cause);
        }

        // GET: Causes/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cause cause = db.Causes.Find(id);
            if (cause == null)
            {
                return HttpNotFound();
            }

            // Check if user has access rights
            var userID = User.Identity.GetUserId();
            if (db.Causes.Where(u => u.ApplicationUserID == userID).Any() || User.IsInRole("Admin"))
            {
                ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "FirstName", cause.ApplicationUserID);
                return View(cause);
            }
            
            return RedirectToAction("Index");
                
        }

        // POST: Causes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CauseID,ApplicationUserID,Title,ShortDescription,LongDescription,Image,Target")] Cause cause)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cause).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "FirstName", cause.ApplicationUserID);
            return View(cause);
        }

        // GET: Causes/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cause cause = db.Causes.Find(id);
            if (cause == null)
            {
                return HttpNotFound();
            }
            // Check if user has access rights
            var userID = User.Identity.GetUserId();
            if (db.Causes.Where(u => u.ApplicationUserID == userID).Any() || User.IsInRole("Admin"))
            {
                ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "FirstName", cause.ApplicationUserID);
                return View(cause);
            }

            return RedirectToAction("Index");
        }

        // POST: Causes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cause cause = db.Causes.Find(id);
            db.Causes.Remove(cause);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Causes/Details/5
        public ActionResult Signees(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cause cause = db.Causes.Find(id);
            if (cause == null)
            {
                return HttpNotFound();
            }



            // Check if user has signed cause before
            var userID = User.Identity.GetUserId();
            if (db.Signatures.Where(u => u.ApplicationUserID == userID && u.CauseID == id).Any())
            {
                ViewBag.AlreadySigned = "True";
            }
            else
            {
                ViewBag.AlreadySigned = "False";
            }




            return View(cause);
        }


        // GET: Causes/_SignatureSmallList
        public ActionResult _SignatureSmallList()
        {
            return PartialView();
        }

        //// GET: Causes/_SignatureSmallListJson
        //public JsonResult _SignatureSmallListJson(int? id)
        //{
        //    var SignatureList = db.Signatures.Where(c => c.CauseID == id).OrderByDescending(s => s.DateTimeSigned).Take(5);


        //    return Json(SignatureList, JsonRequestBehavior.AllowGet);
        //}


        // GET: Causes/_SignCause
        public ActionResult _SignCause()
        {
            return PartialView();
        }

        // POST: Causes/_SignCause
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _SignCause([Bind(Include = "SignatureID,CauseID,ApplicationUserID,DateTimeSigned,Message")] Signature signature)
        {
            if (ModelState.IsValid)
            {
                db.Signatures.Add(signature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView();
        }

        [HttpGet]
        public ActionResult _SignatureCount(string CauseID)
        {

            return PartialView();
        }


        static List<Signature> signatures = new List<Signature>();
        public ActionResult GetSignatures()
        {
            return View();
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

