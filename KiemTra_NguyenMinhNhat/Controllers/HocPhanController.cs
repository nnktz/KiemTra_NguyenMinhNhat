using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_NguyenMinhNhat.Models;

namespace KiemTra_NguyenMinhNhat.Controllers
{
    public class HocPhanController : Controller
    {
        DBModelContextDataContext data = new DBModelContextDataContext();
        // GET: HocPhan

        public ActionResult Index()
        {
            var hocphan = from hp in data.HocPhans select hp;
            return View(hocphan);
        }
        public List<hocphan> LayHocPhan()
        {
            List<hocphan> listHocPhan = Session["HocPhan"] as List<hocphan>;
            if (listHocPhan == null)
            {
                listHocPhan = new List<hocphan>();
                Session["HocPhan"] = listHocPhan;
            }
            return listHocPhan;
        }

        public ActionResult DangKyHP(string id, string strURL)
        {
            List<hocphan> listHocPhan = LayHocPhan();
            hocphan hp = listHocPhan.Find(n => n.mahp == id);
            if (hp == null)
            {
                hp = new hocphan(id);
                listHocPhan.Add(hp);
                return Redirect(strURL);
            }
            else
            {
                return Redirect(strURL);
            }
        }

        private int TongSoLuongHocPhan()
        {
            int tsl = 0;
            List<hocphan> listHocPhan = Session["HocPhan"] as List<hocphan>;
            if (listHocPhan != null)
            {
                tsl = listHocPhan.Count;
            }
            return tsl;
        }

        public ActionResult XoaHocPhan(string id)
        {
            List<hocphan> listHocPhan = LayHocPhan();
            hocphan hp = listHocPhan.SingleOrDefault(n => n.mahp == id);
            if (hp != null)
            {
                listHocPhan.RemoveAll(n => n.mahp == id);
                return RedirectToAction("HocPhan");
            }
            return RedirectToAction("HocPhan");
        }

        public ActionResult XoaTatCaDangKyHP()
        {
            List<hocphan> listHocPhan = LayHocPhan();
            listHocPhan.Clear();
            return RedirectToAction("HocPhan");
        }

        public ActionResult HocPhan()
        {
            List<hocphan> listHocPhan = LayHocPhan();
            ViewBag.TongSoLuongHocPhan = TongSoLuongHocPhan();
            return View(listHocPhan);
        }

        public ActionResult HocPhanPartial()
        {
            ViewBag.TongSoLuongHocPhan = TongSoLuongHocPhan();
            return PartialView();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["HocPhan"] == null)
            {
                return RedirectToAction("Index", "HocPhan");
            }
            List<hocphan> listHocPhan = LayHocPhan();
            ViewBag.TongSoLuongSanPham = TongSoLuongHocPhan();
            return View(listHocPhan);
        }

        public ActionResult DangKy(FormCollection collection)
        {
            DangKy dk = new DangKy();
            SinhVien sv = (SinhVien)Session["TaiKhoan"];

            List<hocphan> hocPhans = LayHocPhan();

            dk.NgayDK = DateTime.Now;
            dk.MaSV = sv.MaSV;

            data.DangKies.InsertOnSubmit(dk);
            data.SubmitChanges();
            foreach (var item in hocPhans)
            {
                ChiTietDangKy ctdk = new ChiTietDangKy();
                ctdk.MaDK = dk.MaDK;
                ctdk.MaHP = item.mahp;
                data.SubmitChanges();

                data.ChiTietDangKies.InsertOnSubmit(ctdk);
            }
            data.SubmitChanges();
            Session["HocPhan"] = null;
            return RedirectToAction("XacNhanDangKy", "HocPhan");
        }

        public ActionResult XacNhanDangKy()
        {
            return View();
        }
    }
}