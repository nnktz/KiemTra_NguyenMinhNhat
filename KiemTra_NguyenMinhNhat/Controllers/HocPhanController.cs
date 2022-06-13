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
        public List<HocPhan> LayHocPhan()
        {
            List<HocPhan> listHocPhan = Session["HocPhan"] as List<HocPhan>;
            if (listHocPhan == null)
            {
                listHocPhan = new List<HocPhan>();
                Session["HocPhan"] = listHocPhan;
            }
            return listHocPhan;
        }

        /*public ActionResult DangKyHP(string id, string strURL)
        {
            List<HocPhan> listHocPhan = LayHocPhan();
            HocPhan hp = listHocPhan.Find(n => n.MaHP == id);
            if (hp == null)
            {
                hp = new HocPhan(id);
                listHocPhan.Add(hp);
                return Redirect(strURL);
            }
            else
            {
                ViewBag.ThongBao = "Học phần này đã đăng ký";
                return Redirect(strURL);
            }
        }*/

        private int TongSoLuongHocPhan()
        {
            int tsl = 0;
            List<HocPhan> listHocPhan = Session["HocPhan"] as List<HocPhan>;
            if (listHocPhan != null)
            {
                tsl = listHocPhan.Count;
            }
            return tsl;
        }

        public ActionResult XoaHocPhan(string id)
        {
            List<HocPhan> listHocPhan = LayHocPhan();
            HocPhan hp = listHocPhan.SingleOrDefault(n => n.MaHP == id);
            if (hp != null)
            {
                listHocPhan.RemoveAll(n => n.MaHP == id);
                return RedirectToAction("HocPhan");
            }
            return RedirectToAction("HocPhan");
        }

        public ActionResult XoaTatCaDangKyHP()
        {
            List<HocPhan> listHocPhan = LayHocPhan();
            listHocPhan.Clear();
            return RedirectToAction("HocPhan");
        }

        public ActionResult HocPhan()
        {
            List<HocPhan> listHocPhan = LayHocPhan();
            ViewBag.TongSoLuongHocPhan = TongSoLuongHocPhan();
            return View(listHocPhan);
        }

        public ActionResult HocPhanPartial()
        {
            ViewBag.TongSoLuongHocPhan = TongSoLuongHocPhan();
            return PartialView();
        }

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
            List<HocPhan> listHocPhan = LayHocPhan();
            ViewBag.TongSoLuongSanPham = TongSoLuongHocPhan();
            return View(listHocPhan);
        }
       /* public ActionResult DangKy(FormCollection collection)
        {
            DangKy dk = new DangKy();
            SinhVien sv = (SinhVien)Session["TaiKhoan"];
            HocPhan s = new HocPhan();

            List<HocPhan> hocPhans = LayHocPhan();

            dk.NgayDK = DateTime.Now;
            dk.MaSV = sv.MaSV;

            data.HocPhans.InsertOnSubmit(dk);
            data.SubmitChanges();
            foreach (var item in hocPhans)
            {
                ChiTietDangKy ctdk = new ChiTietDangKy();
                ctdk.MaDK = dk.MaDK;
                ctdk.MaHP = item.MaHP;
                data.SubmitChanges();

                data.ChiTietDangKies.InsertOnSubmit(ctdk);
            }
            data.SubmitChanges();
            Session["HocPhan"] = null;
            return RedirectToAction("XacNhanDangKy", "HocPhan");
        }*/
        public ActionResult XacNhanDangKy()
        {
            return View();
        }
    }
}