using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;
using SachOnline.Models;
using System.Data.Entity.Validation;

namespace SachOnline.Areas.Admin.Controllers
{
    public class NhaXuatBanController : Controller
    {
        SachOnlineEntities db = new SachOnlineEntities();
        // GET: Admin/NhaXuatBan
        public ActionResult Index(int ? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.NHAXUATBANs.ToList().OrderBy( n => n.MaNXB).ToPagedList(iPageNum, iPageSize));
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NHAXUATBAN nxb, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                nxb.TenNXB = f["sTenNXB"];
                nxb.DiaChi = f["sDiaChi"];
                nxb.DienThoai = f["sDienThoai"];
                db.NHAXUATBANs.Add(nxb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            db.NHAXUATBANs.Remove(nxb);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(NHAXUATBAN nxb, FormCollection f)
        {
            int maNxb = int.Parse(f["iMaNXB"]);
            nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == maNxb );
            //sach = db.SACHes.SingleOrDefault(n => n.MaSach == int.Parse(f["iMaSach"]));

            if (ModelState.IsValid)
            {
                nxb.TenNXB = f["sTenNXB"];
                nxb.DiaChi = f["sDiaChi"];
                nxb.DienThoai = f["sDienThoai"];
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(nxb);
        }
    }
}