using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace SachOnline.Areas.Admin.Controllers
{
    public class ChuDeController : Controller
    {
    SachOnlineEntities db = new SachOnlineEntities();
        // GET: Admin/ChuDe
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.CHUDEs.ToList().OrderBy(n => n.MaCD).ToPagedList(iPageNum, iPageSize));
        }

        [HttpGet]        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CHUDE chude, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                chude.TenChuDe = f["sTenChuDe"];
                db.CHUDEs.Add(chude);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            db.CHUDEs.Remove(chude);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(CHUDE chude, FormCollection f)
        {
            int maChude = int.Parse(f["iMaChuDe"]);
            chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == maChude);
            //sach = db.SACHes.SingleOrDefault(n => n.MaSach == int.Parse(f["iMaSach"]));

            if (ModelState.IsValid)
            {
                chude.TenChuDe = f["sTenChuDe"];
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(chude);
        }
    }
}