using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_NguyenMinhNhat.Models;

namespace KiemTra_NguyenMinhNhat.Controllers
{
    public class NguoiDungController : Controller
    {
        DBModelContextDataContext data = new DBModelContextDataContext();
        // GET: NguoiDung
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var mssv = collection["mssv"];
            if (String.IsNullOrEmpty(mssv))
                ViewData["Loi"] = "Vui lòng nhập mã số sinh viên";
            else
            {
                var sv = data.SinhViens.SingleOrDefault(n => n.MaSV == mssv);
                if (sv != null)
                {
                    Session["Taikhoan"] = sv;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ThongBao = "Sinh viên này không tồn tại";
                }
            }
            return View();
        }
    }
}