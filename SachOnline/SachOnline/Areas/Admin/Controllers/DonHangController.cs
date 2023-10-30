using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;
using SachOnline.Models;

namespace SachOnline.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        SachOnlineEntities db = new SachOnlineEntities();
        // GET: Admin/DonHang
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.DONDATHANGs.ToList().OrderBy(n => n.MaDonHang).ToPagedList(iPageNum, iPageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH" ,"HoTen");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DONDATHANG ddh, FormCollection f)
        {
            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH");
            if (ModelState.IsValid)
            {
                ddh.DaThanhToan = Boolean.Parse(f["sDaThanhToan"]);
                ddh.TinhTrangGiaoHang = int.Parse(f["sTinhTrangGiaoHang"]);
                ddh.NgayDat = Convert.ToDateTime(f["dNgayDat"]);
                ddh.NgayGiao = Convert.ToDateTime(f["dNgayGiao"]);
                ddh.MaKH = int.Parse(f["MaKH"]);
                db.DONDATHANGs.Add(ddh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 422;
                return null;

            }
            return View(ddh);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 422;
                return null;

            }
            return View(ddh);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            db.DONDATHANGs.Remove(ddh);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 422;
                return null;

            }

            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen", ddh.MaKH);
            return View(ddh);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(DONDATHANG ddh, FormCollection f)
        {
            int maDDH = int.Parse(f["iMaDH"]);
            ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == maDDH);
            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen");

            if (ModelState.IsValid)
            {
                ddh.DaThanhToan = Boolean.Parse(f["sDaThanhToan"]);
                ddh.TinhTrangGiaoHang = int.Parse(f["sTinhTrangGiaoHang"]);
                ddh.NgayDat = Convert.ToDateTime(f["dNgayDat"]);
                ddh.NgayGiao = Convert.ToDateTime(f["dNgayGiao"]);
                ddh.MaKH = int.Parse(f["MaKH"]);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ddh);
        }
    }
}