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
    public class KhachHangController : Controller
    {
        SachOnlineEntities db =new SachOnlineEntities();
        // GET: Admin/KhachHang
        public ActionResult Index(int ? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.KHACHHANGs.ToList().OrderBy( n => n.MaKH).ToPagedList(iPageNum, iPageSize));
        }

        [HttpGet]
        public ActionResult Create()
        { 
            return View();
        }

        /*[HttpPost]
        [ValidateInput(false)]
        public ActionResult Create1(KHACHHANG kh, FormCollection f)
        {
            var check = db.KHACHHANGs.FirstOrDefault(n => n.TaiKhoan == kh.TaiKhoan);
            if (check != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập đã tồn tại!!!!!";
                return View();
            }

            if (ModelState.IsValid)
            {
                kh.HoTen = f["sTenKH"];
                kh.TaiKhoan = f["sTaiKhoan"];
                kh.MatKhau = f["SMatKhau"];
                kh.Email = f["sEmail"];
                kh.DiaChi = f["sDiaChi"];
                kh.DienThoai = f["sDienThoai"];
                kh.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }*/

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KHACHHANG kh, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                kh.HoTen = f["sTenKH"];
                kh.TaiKhoan = f["sTaiKhoan"];
                kh.MatKhau = f["SMatKhau"];
                kh.Email = f["sEmail"];
                kh.DiaChi = f["sDiaChi"];
                kh.DienThoai = f["sDienThoai"];
                kh.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();           
        }

        public ActionResult Details(int id)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            db.KHACHHANGs.Remove(kh);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }      

            return View(kh);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(KHACHHANG kh, FormCollection f)
        {
            int maKH = int.Parse(f["iMaKH"]);
            kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == maKH);

            if (ModelState.IsValid)
            {
                kh.HoTen = f["sTenKH"];
                kh.TaiKhoan = f["sTaiKhoan"];
                kh.MatKhau = f["SMatKhau"];
                kh.Email = f["sEmail"];
                kh.DiaChi = f["sDiaChi"];
                kh.DienThoai = f["sDienThoai"];
                kh.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kh);
        }
    }
}