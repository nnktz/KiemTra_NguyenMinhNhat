using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_NguyenMinhNhat.Models;

namespace KiemTra_NguyenMinhNhat.Controllers
{
    public class SinhVienController : Controller
    {
        DBModelContextDataContext data = new DBModelContextDataContext();
        // GET: SinhVien
        public ActionResult Index()
        {
            var sinhvien = from sv in data.SinhViens select sv;
            return View(sinhvien);
        }

        //---Details---
        public ActionResult Details(string id)
        {
            var sinhvien = data.SinhViens.Where(x => x.MaSV == id).First();
            return View(sinhvien);
        }

        //---Create---
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SinhVien s)
        {
            var hoten = collection["hoten"];
            var gioitinh = collection["gioitinh"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["ngaysinh"]);
            var Hinh = collection["Hinh"];
            var manganh = collection["manganh"];

            if (string.IsNullOrEmpty(hoten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.HoTen = hoten.ToString();
                s.GioiTinh = gioitinh.ToString();
                s.NgaySinh = DateTime.Parse(ngaysinh);
                s.Hinh = Hinh.ToString();
                s.MaNganh = manganh.ToString();
                data.SinhViens.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        //---Edit---
        public ActionResult Edit(string id)
        {
            var sinhVien = data.SinhViens.First(x => x.MaSV == id);
            return View(sinhVien);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var masv = data.SinhViens.First(x => x.MaSV == id);
            var hoten = collection["hoten"];
            var gioitinh = collection["gioitinh"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["ngaysinh"]);
            var Hinh = collection["Hinh"];
            var manganh = collection["manganh"];
            masv.MaSV = id;
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                masv.HoTen = hoten.ToString();
                masv.GioiTinh = gioitinh.ToString();
                masv.NgaySinh = DateTime.Parse(ngaysinh);
                masv.Hinh = Hinh.ToString();
                masv.MaNganh = manganh.ToString();
                UpdateModel(masv);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }

        //---Delete---
        public ActionResult Delete(string id)
        {
            var sinhvien = data.SinhViens.First(x => x.MaSV == id);
            return View(sinhvien);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var sinhvien = data.SinhViens.Where(x => x.MaSV == id).First();
            data.SinhViens.DeleteOnSubmit(sinhvien);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //---Get link image---
        public String ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
    }
}