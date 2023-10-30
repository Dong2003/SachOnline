using PagedList;
using SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace SachOnline.Controllers
{
    public class SachOnlineController : Controller
    {
        private SachOnlineEntities data = new SachOnlineEntities();
        // GET: SachOnline
        public ActionResult Index()
        {
            var listSachMoi = LaySachMoi(6);

            return View(listSachMoi);
        }
        public ActionResult ChuDePartial()
        {
            var listChuDe = from cd in data.CHUDEs select cd; 
            return PartialView(listChuDe);
        }
        public ActionResult SachBanNhieuPartial()
        {
            var listSachBanNhieu = LaySachMoi(6);
            return PartialView(listSachBanNhieu);
        }
        public ActionResult FooterPartial()
        {
            return PartialView();
        }
        public ActionResult SlidePartial()
        {
            return PartialView();
        }
        public ActionResult NavPartial()
        {
            return PartialView();
        }
        public ActionResult SachNXBPartial()
        {
            var listNXB  = from cd in data.NHAXUATBANs select cd;
            return PartialView(listNXB);
        }

        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogoutPartial");
        }

        private List<SACH> LaySachMoi(int count )
        {
            return data.SACHes.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }

        public ActionResult SachTheoChuDe(int iMaCD, int ? page)
        {
            ViewBag.MaCD = iMaCD;

            int iSize = 3;

            int iPageNum = (page ?? 1);
           
            var sach = (from s in data.SACHes where s.MaCD == iMaCD select s).OrderBy(s => s.MaCD);
            return View(sach.ToPagedList(iPageNum, iSize));
        }

        public ActionResult SachTheoNXB(int iMaNXB, int ? page)
        {
            ViewBag.MaNXB = iMaNXB;
            int iSize = 3;
            int iPageNum = (page ?? 1);

            var sach = (from s in data.SACHes where s.MaNXB == iMaNXB select s).OrderBy(s => s.MaNXB);
            return View(sach.ToPagedList(iPageNum, iSize));
        }

        public ActionResult ChiTietSach(int id)
        {
            var sach = from s in data.SACHes where s.MaSach == id select s;
            return View(sach.Single());
        }
    }
}